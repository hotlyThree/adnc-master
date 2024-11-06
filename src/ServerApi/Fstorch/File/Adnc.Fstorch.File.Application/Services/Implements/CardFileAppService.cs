using System.Text.Json;

namespace Adnc.Fstorch.File.Application.Services.Implements
{
    public class CardFileAppService : AbstractAppService, ICardFileAppService
    {
        private readonly IEfRepository<Da_Files> _fileRepository;
        //private readonly MsgGrpc.MsgGrpcClient _msgGrpcClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRestClient _userRestClient;
        public CardFileAppService(IEfRepository<Da_Files> fileRepository,  IUnitOfWork unitOfWork, IUserRestClient userRestClient)
        {
            _fileRepository = fileRepository;
            //_msgGrpcClient = msgGrpcClient;
            _unitOfWork = unitOfWork;
            _userRestClient = userRestClient;
        }

        public async Task<AppSrvResult<string[]>> ChangeThumimg(ThumingUpdationDto input)
        {
            // 设置路径为当前项目根目录下的"ConvertPdfToImages"文件夹
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Thumimg");
            _unitOfWork.BeginTransaction();
            try
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                //var fileEntity = await _fileRepository.FetchAsync(x => x, x => x.Orderid == input.id.ToString());
                //替换   20240407 因更改为批量上传（九宫格），就不做替换图片处理，用户更换图片值需要自己删除指定图片后重新上传即可
                /*if(fileEntity != null)
                {
                    await _fileRepository.DeleteAsync(fileEntity.Id);
                    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{fileEntity.Filepath}");
                    if(System.IO.File.Exists(rootPath))
                        System.IO.File.Delete(rootPath);
                }*/
                long id = IdGenerater.GetNextId();
                string fileName = $"{input.id}_{id}_{input.file.FileName}";
                string filePath = Path.Combine(dirPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await input.file.CopyToAsync(stream);
                }

                var entity = new Da_Files()
                {
                    Id = id,
                    Filepath = $"Thumimg/{fileName}",
                    Orderid = input.id.ToString(),
                    Filetype = "文章",
                    Filesize = input.file.Length / 1024 == 0 ? 1 : input.file.Length / 1024,
                    Inserttime = DateTime.Now,
                    Uid = input.Releaseid == 0 ? "" : input.Releaseid.ToString(),
                    Cid = input.Cid == 0 ? "" : input.Cid.ToString(),
                    SystemName = "名片系统"
                };
                //由于IIS6.0不支持http/2 IIS10.0才引入http/2的支持，并且windows上部署必须是特定版本 比如windows server2016   并且就算IIS支持http/2，也不支持grpc
                /*var grpcResponse = await _msgGrpcClient.ChangeThumimgAsync(new ChangeThumimgRequest { Id = input.id, Path = entity.Filepath });
                if(grpcResponse.IsSuccessStatusCode)
                    await _fileRepository.InsertAsync(entity);*/
                List<string> thumimg = new ();
                if (input.Thumimg.IsNullOrWhiteSpace() || input.Thumimg.ToLower().Contains("null"))
                {
                    thumimg = new()
                    {
                        entity.Filepath
                    };
                }
                else
                {
                    thumimg = JsonSerializer.Deserialize<List<string>>(input.Thumimg);
                    thumimg.Add(entity.Filepath);
                }
                await _fileRepository.InsertAsync(entity);
                await _userRestClient.ChangeThumingAsync(new MessageUpdationRto(input.id, JsonSerializer.Serialize(thumimg)));
                await _unitOfWork.CommitAsync();
                return thumimg.ToArray();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<AppSrvResult<string[]>> CreateSolutionAsync(FileCreationDto input)
        {
            List<string> images = new List<string>();
            // 在此处可以对IFormFile进行操作，如保存或转换等
            try
            {
                _unitOfWork.BeginTransaction();

                // 设置路径为当前项目根目录下的"Solutions"文件夹
                string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Solutions");

                // 检查文件夹是否存在，如果不存在则创建文件夹
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                string fileName = $"{IdGenerater.GetNextId()}_{input.File.FileName}";
                string filePath = Path.Combine(dirPath, fileName);
                List<Da_Files> files = new List<Da_Files>();
                // 将IFormFile保存为临时文件
                //var tempFile = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.File.CopyToAsync(stream);
                    // 获取PDF标题。
                    string title = input.Filememo;
                    /*// 读取PDF文件 
                    stream.Position = 0;
                    //CopyToAsync方法在把文件复制完成后并不会自动地重置stream的Position属性。这意味着，尝试打开PDF文档的时候，stream的Position其实是在文件的末尾，而不是文件的开头  
                    // 读取PDF文档。
                    Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(stream);

                    // 获取PDF标题。
                    string title = input.Filememo.IsNullOrWhiteSpace() ? pdfDocument.Info.Title : input.Filememo;

                    string thumbnailImagePath = "";
                    int pageCount = pdfDocument.Pages.Count;
                    #region Aspose.Pdf   pdf转图片（试用版,仅支持获取前4页，若读取全部页需购买企业版）
                    *//*bool flag = true;
                    for (int i = 1; i <= pageCount; i++)
                    {
                        Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(300);
                        Aspose.Pdf.Devices.PngDevice pngDevice;
                        string imageName = "";
                        pngDevice = new Aspose.Pdf.Devices.PngDevice(1920, 1080, resolution);
                        imageName = $"{IdGenerater.GetNextId()}_page_{i}.png";
                        using (var imageStream = new MemoryStream())
                        {
                            // PDF页转为png图像。
                            pngDevice.Process(pdfDocument.Pages[i], imageStream);
                            // 将流位置复位。
                            imageStream.Position = 0;

                            // 使用ImageSharp加载图像。
                            using Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageStream);

                            string savePath = Path.Combine(dirPath, imageName);
                            // 保存图像。
                            await image.SaveAsPngAsync(savePath);

                            //写入da_file
                            var filesEntity = Mapper.Map<Da_Files>(input);
                            filesEntity.Id = IdGenerater.GetNextId();
                            filesEntity.Filepath = $"Solutions/{imageName}";
                            filesEntity.Filesize = input.File.Length / 1024 == 0 ? 1 : input.File.Length / 1024;
                            filesEntity.Inserttime = DateTime.Now;
                            filesEntity.SystemName = "名片系统";
                            await _fileRepository.InsertAsync(filesEntity);
                            images.Add($"Solutions/{imageName}");
                        }
                        if (i == 1 && flag)
                        {
                            // 如果是第一页,生成缩略图路径。
                            thumbnailImagePath = $"Solutions/{imageName}";
                            // 第一页额外生成一个缩略图 200*200 像素。
                            pngDevice = new Aspose.Pdf.Devices.PngDevice(200, 200, resolution);
                            imageName = $"{IdGenerater.GetNextId()}_thumb.png";
                            using (var imageStream = new MemoryStream())
                            {
                                // PDF页转为png图像。
                                pngDevice.Process(pdfDocument.Pages[i], imageStream);
                                // 将流位置复位。
                                imageStream.Position = 0;

                                // 使用ImageSharp加载图像。
                                using Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageStream);

                                string savePath = Path.Combine(dirPath, imageName);
                                // 保存图像。
                                await image.SaveAsPngAsync(savePath);

                                //写入da_file
                                var filesEntity = Mapper.Map<Da_Files>(input);
                                filesEntity.Id = IdGenerater.GetNextId();
                                filesEntity.Filepath = $"Solutions/{imageName}";
                                filesEntity.Filesize = input.File.Length / 1024 == 0 ? 1 : input.File.Length / 1024;
                                filesEntity.Inserttime = DateTime.Now;
                                filesEntity.SystemName = "名片系统";
                                await _fileRepository.InsertAsync(filesEntity);
                                images.Add($"Solutions/{imageName}");
                            }
                        }
                        //其余业务逻辑代码继续...
                    }*//*
                    #endregion


                    Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(300);
                    Aspose.Pdf.Devices.PngDevice pngDevice;
                    string imageName = $"{IdGenerater.GetNextId()}_thumb.png";
                    // 如果是第一页,生成缩略图路径。
                    thumbnailImagePath = $"Solutions/{imageName}";
                    // 第一页额外生成一个缩略图 200*200 像素。
                    pngDevice = new Aspose.Pdf.Devices.PngDevice(300, 300, resolution);
                    List<Da_Files> files = new List<Da_Files>();
                    using (var imageStream = new MemoryStream())
                    {
                        // PDF页转为png图像。
                        try
                        {
                            pngDevice.Process(pdfDocument.Pages[1], imageStream);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("pngDevice.Process  PDF页转png出错:" + ex.Message);
                        }
                        // 将流位置复位。
                        imageStream.Position = 0;
                        long sizePerPixel = 4;
                        try
                        {
                            // 使用ImageSharp加载图像。
                            using Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageStream);
                            // Rgba32中每个像素由4个字节组成（R, G, B, A）
                            string savePath = Path.Combine(dirPath, imageName);
                            // 保存图像。
                            await image.SaveAsPngAsync(savePath);
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("使用ImageSharp加载图像并保存图像出错:" + ex.Message);
                        }*/

                    var response = await _userRestClient.CreateMessageAsync(
                               new MessageCreationRto("B", input.Uid.IsNotNullOrWhiteSpace() ? (long)input.Uid.ToLong() : 0, 0, input.Cid.IsNotNullOrWhiteSpace() ? (long)input.Cid.ToLong() : 0, 0, title, $"Solutions/{fileName}", $"Solutions/58122f10af0bf_610.jpg"));

                        if (!response.IsSuccessStatusCode)
                            return Problem(HttpStatusCode.BadRequest, "创建文章时出错");
                        //写入da_file

                        var filesEntity = Mapper.Map<Da_Files>(input);
                        filesEntity.Id = IdGenerater.GetNextId();
                        filesEntity.Filepath = $"Solutions/58122f10af0bf_610.jpg";
                        filesEntity.Filesize = input.File.Length / 1024 == 0 ? 1 : input.File.Length / 1024;
                        filesEntity.Inserttime = DateTime.Now;
                        filesEntity.SystemName = "名片系统";
                        filesEntity.Orderid = response.Content.ToString();
                        filesEntity.Security = 1;
                        var filesEntityImg = Mapper.Map<Da_Files>(input);
                        filesEntityImg.Id = IdGenerater.GetNextId();
                        filesEntityImg.Filepath = $"Solutions/{fileName}";
                        filesEntityImg.Filesize = 0;
                        filesEntityImg.Inserttime = DateTime.Now;
                        filesEntityImg.SystemName = "名片系统";
                        filesEntityImg.Filetype = "缩略图";
                        filesEntity.Security = 1;
                        filesEntityImg.Orderid = response.Content.ToString();
                        files.Add(filesEntity);
                        files.Add(filesEntityImg);
                        images.Add($"Solutions/58122f10af0bf_610.jpg");
                    }
                    await _fileRepository.InsertRangeAsync(files);
                    /*
                    using (PdfDocument pdf = PdfDocument.Open(stream))
                    {
                        string title = pdf.GetInformation().Title;
                        string thumimgPath = "";
                        int pageCount = pdf.PageCount;

                        for (int i = 0; i <= pageCount; i++)
                        {
                            if (i == 0)
                            {
                                //第一页额外生成一个缩略图 200*200 像素
                                using var pageImageThumimg = pdf.Render(i, 200, 200, true);
                                using var memoryStreamThumimg = new MemoryStream();
                                pageImageThumimg.Save(memoryStreamThumimg, System.Drawing.Imaging.ImageFormat.Png);
                                memoryStreamThumimg.Position = 0;
                                using Image<Rgba32> thumimg = SixLabors.ImageSharp.Image.Load<Rgba32>(memoryStreamThumimg);
                                string thumimgName = $"{IdGenerater.GetNextId()}_thumimg.png";
                                string thumimgSavePath = Path.Combine(dirPath, thumimgName);
                                await thumimg.SaveAsPngAsync(thumimgSavePath);
                                thumimgPath = $"Solutions/{thumimgName}";
                            }
                            using var pageImage = pdf.Render(i, 1920, 1080, true);
                            using var memoryStream = new MemoryStream();
                            pageImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                            memoryStream.Position = 0;
                            using Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(memoryStream);
                            // 图像处理
                            string filename = $"{IdGenerater.GetNextId()}_page_{i}.png";
                            string savePath = Path.Combine(dirPath, filename);
                            await image.SaveAsPngAsync(savePath);  // Save the image
                            //写入da_file
                            var filesEntity = Mapper.Map<Da_Files>(input);
                            filesEntity.Id = IdGenerater.GetNextId();
                            filesEntity.Filepath = $"Solutions/{filename}";
                            filesEntity.Filesize = input.File.Length / 1024 == 0 ? 1 : input.File.Length / 1024;
                            filesEntity.Inserttime = DateTime.Now;
                            filesEntity.SystemName = "名片系统";
                            await _fileRepository.InsertAsync(filesEntity);
                            images.Add($"Solutions/{filename}");
                        }
                        //创建解决方案
                        if (thumimgPath.IsNullOrWhiteSpace())
                            return Problem(HttpStatusCode.BadRequest, "缩略图生成失败");
                        await _userRestClient.CreateMessageAsync(
                           new MessageCreationRto("B", input.Uid.IsNotNullOrWhiteSpace() ? (long)input.Uid.ToLong() : 0, 0, input.Cid.IsNotNullOrWhiteSpace() ? (long)input.Cid.ToLong() : 0, 0, title, string.Join(',', images), thumimgPath));
                    }
                    */
                    await _unitOfWork.CommitAsync();
                    //System.IO.File.Delete(tempFile);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }
            finally { _unitOfWork.Dispose(); }
            return images.ToArray();
        }

        public async Task<AppSrvResult<string>> SharedImgAsync(IFormFile File)
        {
            // 以某种方式生成一个唯一的文件名
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(File.FileName)}";
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Temp");
            if(!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            var filePath = Path.Combine(dirPath, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await File.CopyToAsync(stream);
            }
            // 返回URL供前端访问
            return $"Temp/{fileName}";
        }

        public async Task<AppSrvResult<string>> UploadPhotoAsync(PhotoUpdationDto input)
        {
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photo");
            long fileid = IdGenerater.GetNextId();
            string fileName = $"{input.Id}_{input.Type}_{input.File.FileName}";
            string result = string.Empty;
            string type = "头像";
            type = input.Type switch
            {
                "photo" => "头像",
                "wechat" => "微信视频号",
                "tiktok" => "抖音视频号",
                _ => "头像"
            };
            _unitOfWork.BeginTransaction();
            try
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                var fileEntity = await _fileRepository.FetchAsync(x => x, x => x.Orderid == input.Id.ToString() && x.Filetype.Equals(type));
                //替换旧图片
                if(fileEntity != null)
                {
                    await _fileRepository.DeleteAsync(fileEntity.Id);
                    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{fileEntity.Filepath}");
                    if (System.IO.File.Exists(rootPath))
                        System.IO.File.Delete(rootPath);
                }
                string filePath = Path.Combine(dirPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await input.File.CopyToAsync(stream);
                }
                var entity = new Da_Files()
                {
                    Id = fileid,
                    Filepath = $"Photo/{fileName}",
                    Orderid = input.Id.ToString(),
                    Filetype = type,
                    Filesize = input.File.Length / 1024 == 0 ? 1 : input.File.Length / 1024,
                    Inserttime = DateTime.Now,
                    Uid = input.Id.ToString(),
                    Cid = "",
                    Security = 1,
                    SystemName = "名片系统"
                };
                await _fileRepository.InsertAsync(entity);
                var userResult = await _userRestClient.ChangePhotoAsync(new ChangePhotoRto(input.Id, input.Type, entity.Filepath));

                await _unitOfWork.CommitAsync();
                result = entity.Filepath;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Problem(HttpStatusCode.BadRequest, $"上传{type}失败");
            }
            finally { _unitOfWork.Dispose(); }
            return result;
        }
    }
}
