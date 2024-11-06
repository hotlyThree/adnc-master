
using Adnc.Fstorch.File.Application.Cache;
using Adnc.Infra.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Adnc.Fstorch.File.Application.Services.Implements
{
    public class FileManagerAppService : AbstractAppService, IFileManagerAppService
    {
        private readonly IEfRepository<Da_Files> _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRestClient _userRestClient;
        private readonly CacheService _cacheService;
        private readonly ILogger<FileManagerAppService> _logger;
        public FileManagerAppService(IEfRepository<Da_Files> fileRepository, IUnitOfWork unitOfWork, IUserRestClient userRestClient, CacheService cacheService, ILogger<FileManagerAppService> logger)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            _userRestClient = userRestClient;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<AppSrvResult> DeletePublicFileAsync(long id)
        {
            var fileEntity = await _fileRepository.FetchAsync(x => x, x => x.Id == id);
            if (fileEntity == null)
                return Problem(HttpStatusCode.NotFound, "文件不存在或已删除");
            await _fileRepository.DeleteAsync(fileEntity.Id);
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{fileEntity.Filepath}");
            if (System.IO.File.Exists(rootPath))
                await Task.Run(() =>
                {
                    System.IO.File.Delete(rootPath);
                });
            return AppSrvResult();
        }

        public async Task<AppSrvResult> DeleteRangePublicFileAsync(IEnumerable<long> ids)
        {
            var fileEntities = await _fileRepository.Where(x => ids.Contains(x.Id)).ToListAsync();
            if(fileEntities.Count == 0)
                return Problem(HttpStatusCode.NotFound, "文件不存在或已删除");
            await _fileRepository.DeleteRangeAsync(x => ids.Contains(x.Id));
            await Task.Run(() =>
            {
                foreach (var fileEntity in fileEntities)
                {
                    var rootPath = Path.Combine(Directory.GetCurrentDirectory(), fileEntity.Filepath);
                    if (System.IO.File.Exists(rootPath))
                        System.IO.File.Delete(rootPath);
                }
            });
            return AppSrvResult();
        }

        public async Task<PageModelDto<FileDto>> PagedAsync(FileSearchPagedDto search)
        {
            search.TrimStringFields();
            if (search.Uid.IsNullOrWhiteSpace() && search.Cid.IsNullOrWhiteSpace())
                return new PageModelDto<FileDto>(search);
            var expression = ExpressionCreator
                .New<Da_Files>()
                .AndIf(search.Inserttime.IsNotNullOrWhiteSpace(), x => x.Inserttime >= DateTime.Parse(search.Inserttime))
                .AndIf(search.Uid.IsNotNullOrWhiteSpace(), x => x.Uid.Equals(search.Uid))
                .AndIf(search.Cid.IsNotNullOrWhiteSpace(), x => x.Cid.Equals(search.Cid))
                .AndIf(search.Filememo.IsNotNullOrWhiteSpace(), x => EF.Functions.Like(x.Filememo, $"%{search.Filememo}%"))
                .AndIf(search.Filetype.IsNotNullOrWhiteSpace(), x => x.Filetype.Equals(search.Filetype))
                .AndIf(search.Orderid.IsNotNullOrWhiteSpace(), x => x.Orderid.Equals(search.Orderid))
                .AndIf(search.Ordertype.IsNotNullOrWhiteSpace(), x => x.OrderType.Equals(search.Ordertype));
            var total = await _fileRepository.CountAsync(expression);
            if (total == 0)
                return new PageModelDto<FileDto>(search);
            var entities = await _fileRepository
                .Where(expression)
                .OrderByDescending(x => x.Inserttime)
                .Skip(search.SkipRows())
                .Take(search.PageSize)
                .ToListAsync();
            var data = Mapper.Map<List<FileDto>>(entities);
            var result = await _userRestClient.GetAccountList();
            if (result.IsSuccessStatusCode)
            {
                var accountRtos = result.Content;
                foreach(var fileDto in data)
                {
                    var account = accountRtos.FirstOrDefault(x => x.Id.ToString().Equals(fileDto.Uid));
                    var username = account == null ? "" : account.Username;
                    fileDto.Username = username;
                }
            }
            return new PageModelDto<FileDto>(search, data, total);
        }

        public async Task<AppSrvResult<long>> UpLoadPublicFileAsync(FilePublicCreationDto input)
        {
            if (input.Uid.IsNullOrWhiteSpace() && input.Cid.IsNullOrWhiteSpace())
                return Problem(HttpStatusCode.BadRequest, "请先注册账号");
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Static");
            if(!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            try
            {
                //var progress = 0; 进度
                _unitOfWork.BeginTransaction();
                var fileName = $"{IdGenerater.GetNextId()}_{input.File.FileName}";
                var entity = Mapper.Map<Da_Files>(input);
                entity.Id = IdGenerater.GetNextId();
                entity.Inserttime = DateTime.Now;
                entity.Filepath = $"Static/{fileName}";
                entity.Storagetype = "A"; //目前默认本地上传
                entity.Filesize = input.File.Length / 1024 == 0 ? 1 : input.File.Length / 1024;
                string filePath = Path.Combine(dirPath, fileName);
                using var stream = new FileStream(filePath, FileMode.CreateNew);
                await input.File.CopyToAsync(stream);
                /*
                 * 视频文件借助FFmpeg实现取视频第一帧并保存未图片
                 * string ffmpegExecutablesPath = Path.Combine(Directory.GetCurrentDirectory(), "FFmpeg");

                //视频第一帧保存为图片
                var imageFileName = $"{IdGenerater.GetNextId()}_{Path.GetFileNameWithoutExtension(input.File.FileName)}.jpeg";
                string imageFilePath = Path.Combine(dirPath, imageFileName);
                FFmpeg.SetExecutablesPath(ffmpegExecutablesPath);
                var videoInfo = await FFmpeg.GetMediaInfo(filePath);
                //获取第一帧
                var videoStream = videoInfo.VideoStreams.First();
                // 定义将要生成的截图的大小
                // 你可以根据需要调整这个值
                var screenshotSize = videoStream.SetSize(VideoSize.Hd1080);

                //创建获取第一帧的操作
                var conversion = FFmpeg.Conversions.New()
                               .AddStream(screenshotSize)
                               .SetOutput(imageFilePath)
                               .SetOverwriteOutput(true) // 有存在的文件时, 覆盖
                               .ExtractNthFrame(1, outputFileName => imageFileName); //提取第一帧

                //运行操作
                await conversion.Start();*/

                if (!entity.Filetype.Equals("图片"))
                    entity.Security = 1;
                else
                {
                    //图片安全检测

                    /*var checkBody = new MediaCheckRto($"https://card.fstorch.com/{entity.Filepath}", 2, (long)entity.Uid.ToLong());
                    var response = await _userRestClient.MediaCheckDto(checkBody);
                    var result = (Dictionary<string, object>)response.Content;
                    if (!response.IsSuccessStatusCode)
                    {
                        System.IO.File.Delete(filePath);
                        result.TryGetValue("errcode", out object errcode);
                        return Problem(HttpStatusCode.BadRequest, $"图片安全检测错误:({(int)errcode}){result["errmsg"]}");
                    }
                    result.TryGetValue("trace_id", out object trace_id);
                    _cacheService.SetMediaCheckIdToCacheAsync((string)trace_id, entity.Id);*/
                }

                await _fileRepository.InsertAsync(entity);
                await _unitOfWork.CommitAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, $"文件上传失败:{ex.Message}");
            }
            finally { _unitOfWork.Dispose(); }
        }

        public async Task<AppSrvResult> UploadMediaThumimgAsync(long id, IFormFile file)
        {
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Static");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            try
            {
                //var progress = 0; 进度
                _unitOfWork.BeginTransaction();
                var fileName = $"{IdGenerater.GetNextId()}_{file.FileName}";
                string filePath = Path.Combine(dirPath, fileName);
                using var stream = new FileStream(filePath, FileMode.CreateNew);
                await file.CopyToAsync(stream);
                await _fileRepository.UpdateAsync(new Da_Files { Id = id, MediaPath = $"Static/{fileName}"}, UpdatingProps<Da_Files>(x => x.MediaPath));
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, $"文件上传失败:{ex.Message}");
            }
            finally { _unitOfWork.Dispose(); }
            return AppSrvResult();
        }

        public Task<string> MediaCheckCallBackAsync(string signature, string timestamp, string nonce, string echostr)
        {
            //将token、timestamp、nonce三个参数进行字典序排序
            //将三个参数字符串拼接成一个字符串进行sha1加密
            //开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
            var before = $"4008394213{timestamp}{nonce}";
            var stringArr = new string[] { "4008394213", timestamp, nonce };
            Array.Sort(stringArr);
            var stringBuf = new StringBuilder();
            foreach(var str in stringArr)
                stringBuf.Append(str);
            var sign = InfraHelper.Encrypt.Sha1(stringBuf.ToString());
            sign = sign.ToLower();
            if (signature.Equals(sign))
                return Task.FromResult(echostr);
            _logger.LogInformation("参数>>>timestamp>>>{timestamp}<<<nonce>>>{nonce}<<<echostr>>>{echostr}",timestamp, nonce, echostr);
            _logger.LogInformation($"签名>>>{signature}");
            _logger.LogInformation($"验签>>>{sign}");
            _logger.LogInformation("验证错误");
            return Task.FromResult("signature check fail");
        }

        public async Task<string> MediaCheckCallBackPostAsync(JObject input)
        {
            var trace_id = input["trace_id"].ToString();
            if (trace_id.IsNullOrWhiteSpace())
                return "trace_id was whitespace";
            var fileid = await _cacheService.GetMediaCheckIdToCacheAsync(trace_id);
            var label = input["result"]["label"].ToString();
            if (!label.Equals("100"))
            {
                var filepath = await _fileRepository.FetchAsync(x => x.Filepath, x => x.Id == fileid);
                if (filepath.IsNotNullOrWhiteSpace())
                {
                    var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var rootPath = Path.Combine(dirPath, filepath);
                    System.IO.File.Delete(rootPath);
                    await _fileRepository.DeleteAsync(fileid);
                }
                _logger.LogInformation($"微信图片检测上传检测>>>{label}");
                return "success";
            }
            await _fileRepository.UpdateAsync(new Da_Files { Id = fileid, Security = 1 }, UpdatingProps<Da_Files>(x => x.Security));
            return "success";
        }
    }
}
