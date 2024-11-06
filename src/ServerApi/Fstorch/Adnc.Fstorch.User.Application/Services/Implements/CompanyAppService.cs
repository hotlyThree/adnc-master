using Microsoft.AspNetCore.Mvc;

namespace Adnc.Fstorch.User.Application.Services.Implements
{
    public class CompanyAppService : AbstractAppService, ICompanyAppService
    {
        private readonly IEfRepository<Da_companyinfo> _companyinfoRepository;
        private readonly IEfRepository<Da_userinfo> _userinfoRepository;
        private readonly CacheService _cacheService;
        public CompanyAppService(IEfRepository<Da_companyinfo> companyinfoRepository, CacheService cacheService, IEfRepository<Da_userinfo> userinfoRepository)
        {
            _companyinfoRepository = companyinfoRepository;
            _cacheService = cacheService;
            _userinfoRepository = userinfoRepository;
        }

        public async Task<AppSrvResult<long>> CreateAsync(CompanyCreation input)
        {
            input.TrimStringFields();
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            if (comDtoList != null && comDtoList.Exists(x => x.Name.Equals(input.Name)))
                return Problem(HttpStatusCode.BadRequest, "该公司已经存在，不能重复创建");
            var company = Mapper.Map<Da_companyinfo>(input);
            company.Reg_time = DateTime.Now;
            company.Id = IdGenerater.GetNextId();
            company.Insider = input.Creater.ToString();
            await _companyinfoRepository.InsertAsync(company);
            return company.Id;
        }

        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _companyinfoRepository.DeleteAsync(id);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<CompanyDto>> GetInfoAsync(long id)
        {
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            if (comDtoList == null)
                return Problem(HttpStatusCode.NotFound, "您还未创建任何公司信息");
            var company = comDtoList.FirstOrDefault(x => x.Id == id);
            if (company == null)
                return Problem(HttpStatusCode.NotFound, "公司信息不存在");
            return company;
        }

        public async Task<AppSrvResult<List<CompanyDto>>> GetListAsync(string insider)
        {
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            if (comDtoList == null)
                return Problem(HttpStatusCode.NotFound, "还未创建任何公司信息");
            comDtoList = comDtoList.Where(x => x.Insider.Contains(insider)).ToList();
            return comDtoList;
        }

        public async Task<AppSrvResult> UpdateAsync(long id, CompanyUpdation input)
        {
            input.TrimStringFields();
            var company = Mapper.Map<Da_companyinfo>(input);
            company.Id = id;
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            if (comDtoList == null)
                return Problem(HttpStatusCode.BadRequest, "更新的公司信息不存在");
            var comDto = comDtoList.FirstOrDefault(x => x.Id == id);
            if (comDto == null)
                return Problem(HttpStatusCode.BadRequest, "更新的公司信息不存在");
            var updatingProps = UpdatingProps<Da_companyinfo>
                (x => x.Name, x => x.Scope, x => x.Status, x => x.Valid_time, x => x.Address, x => x.Phone, x => x.Url, x => x.Managerphone, x => x.Logo, x => x.Country, x => x.Creater);
            await _companyinfoRepository.UpdateAsync(company, updatingProps);
            if(!comDto.Insider.Contains(input.Creater.ToString()) && input.Creater != 0)
            {

            }
            return AppSrvResult();
        }

        /*public async Task<AppSrvResult<string[]>> ConvertPdfToImages(IFormFile pdfFile)
        {
            List<string> images = new List<string>();
            // 在此处可以对IFormFile进行操作，如保存或转换等
            try
            {
                if (pdfFile != null && pdfFile.Length > 0)
                {
                    // 将IFormFile保存为临时文件
                    var tempFile = Path.GetTempFileName();
                    using (var stream = new FileStream(tempFile, FileMode.Create))
                    {
                        await pdfFile.CopyToAsync(stream);
                        using (var rasterizer = new GhostscriptRasterizer())
                        {
                            // 设置路径为当前项目根目录下的"ConvertPdfToImages"文件夹
                            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ConvertPdfToImages");

                            // 检查文件夹是否存在，如果不存在则创建文件夹
                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }
                            rasterizer.Open(stream);

                            for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                            {
                                var pageImage = rasterizer.GetPage(200, pageNumber);
                                var filename = $"{IdGenerater.GetNextId()}_page_{pageNumber}.png";
                                var savePath = Path.Combine(dirPath, filename);
                                pageImage.Save(savePath, ImageFormat.Png);
                                images.Add($"ConvertPdfToImages/{filename}");
                            }

                            rasterizer.Close();
                            stream.Close();
                        }
                        File.Delete(tempFile);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return images.ToArray();
        }*/

        public async Task<object> GetListByNameAsync(long aid,string name)
        {
            var comDtoList = await _cacheService.GetAllCompanysFromCacheAsync();
            if (comDtoList == null)
                return Problem(HttpStatusCode.NotFound, "公司不存在");
            comDtoList = comDtoList.Where(x =>(x.Name.IsNotNullOrWhiteSpace() && x.Name.Contains(name))).ToList();
            List<object> list = new();
            foreach (var comDto in comDtoList)
            {
                var isInsider = comDto.Insider.Contains(aid.ToString());
                var obj = new
                {
                    cid = comDto.Id,
                    name = comDto.Name,
                    isInsider = isInsider
                };
                list.Add(obj);
            }
            return new JsonResult(list);
        }

        public async Task<object> GetWaitingTask(long aid)
        {
            List<string> ids = new() { aid.ToString() };
            var cmpDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            if (cmpDtos == null)
                return Problem(HttpStatusCode.NotFound, "公司不存在");
            cmpDtos = cmpDtos.Where(x => x.Creater == aid).ToList();
            if (cmpDtos.Count == 0)
                return Problem(HttpStatusCode.NotFound, "您还未创建公司");
            List<object> list = new();
            var allaccount = await _cacheService.GetAllAccountFromCacheAsync();
            if(allaccount == null)
                return Problem(HttpStatusCode.NotFound,"账号不存在");
            foreach (var cmpDto in cmpDtos)
            {
                if (cmpDto.Applicant.IsNotNullOrWhiteSpace())
                {
                    var applicant = cmpDto.Applicant.Split(',');
                    foreach(var id in applicant)
                    {
                        if (id.IsNullOrWhiteSpace())
                            continue;
                        var account = allaccount.FirstOrDefault(x => x.Id.ToString().Equals(id));
                        if (account == null)
                            continue;
                        var obj = new
                        {
                            id,
                            name = account.Username,
                            phone = account.Phone,
                            openid = account.Openid,
                            cid = cmpDto.Id,
                            cname = cmpDto.Name
                        };
                        list.Add(obj);
                    }
                }
            }
            return new JsonResult(list);
        }

        public async Task<AppSrvResult> ApplyCompany(long aid, long cid, long uid)
        {
            var cmpDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            if (cmpDtos == null)
                return Problem(HttpStatusCode.BadRequest, "未建立任何公司信息");
            var cmpDto = cmpDtos.FirstOrDefault(x => x.Id == cid);
            var company = Mapper.Map<Da_companyinfo>(cmpDto);
            if (company.Applicant.Contains(aid.ToString()))
                return Problem(HttpStatusCode.BadRequest, "请勿重复申请");
            if(company.Applicant.IsNotNullOrWhiteSpace())
                company.Applicant += "," + aid.ToString();
            else
                company.Applicant = aid.ToString();
            if (company.CardApplicant.IsNotNullOrWhiteSpace())
                company.CardApplicant += $",{aid}:{uid}";
            else
                company.CardApplicant = $"{aid}:{uid}";
            await _companyinfoRepository.UpdateAsync(company, UpdatingProps<Da_companyinfo>(x => x.Applicant, x => x.CardApplicant));
            return AppSrvResult();
        }

        public async Task<AppSrvResult> Review(long aid, string type, long cid)
        {
            var cmpDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            if (cmpDtos == null)
                return Problem(HttpStatusCode.BadRequest, "未建立任何公司信息");
            var cmpDto = cmpDtos.FirstOrDefault(x => x.Id == cid);
            var company = Mapper.Map<Da_companyinfo>(cmpDto);
            if(company == null)
                return Problem(HttpStatusCode.BadRequest, "公司不存在");
            //回写名片ID
            string callbackCardId = "";
            if (company.Applicant.IsNotNullOrWhiteSpace() && company.Applicant.Contains(","))
            {
                var applicants = company.Applicant.Split(',').ToList();
                var removeStr = applicants.FirstOrDefault(x => x.Contains(aid.ToString()));
                if (removeStr.IsNotNullOrWhiteSpace())
                    applicants.Remove(removeStr);
                company.Applicant = string.Join(',', applicants);
            }
            else
                company.Applicant = "";
            if (company.CardApplicant.IsNotNullOrWhiteSpace() && company.CardApplicant.Contains(","))
            {
                var applicants = company.CardApplicant.Split(',').ToList();
                var removeStr = applicants.FirstOrDefault(x => x.Contains(aid.ToString()));
                //申请时的名片ID
                callbackCardId = removeStr.Split(":")[1];
                if (removeStr.IsNotNullOrWhiteSpace())
                    applicants.Remove(removeStr);
                company.CardApplicant = string.Join(',', applicants);
            }
            else if(company.CardApplicant.IsNotNullOrWhiteSpace())
            {
                //申请时的名片ID
                callbackCardId = company.CardApplicant.Split(":")[1];
                company.CardApplicant = "";
            }
            if (type.Equals("1"))
            {
                //同意
                //company.Applicant = company.Applicant.Contains(',') ? company.Applicant.Replace($",{aid}", "") : company.Applicant.Replace($"{aid}", "");
                company.Insider += $",{aid}";
                await _companyinfoRepository.UpdateAsync(company, UpdatingProps<Da_companyinfo>(x => x.Applicant, x => x.Insider, x => x.CardApplicant));
                //回写名片的公司信息
                if (callbackCardId.IsNotNullOrWhiteSpace())
                    await _userinfoRepository.UpdateAsync(new Da_userinfo { Id = (long)callbackCardId.ToLong(), Cid = company.Id }, UpdatingProps<Da_userinfo>(x => x.Cid));
            }
            else if(type.Equals("2"))
            {
                //拒绝
                //company.Applicant = company.Applicant.Replace($",{aid}", "");
                await _companyinfoRepository.UpdateAsync(company, UpdatingProps<Da_companyinfo>(x => x.Applicant, x => x.CardApplicant));
            }
            return AppSrvResult();
        }

        public async Task<AppSrvResult> Remove(long aid, long cid)
        {
            var cmpDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            if (cmpDtos == null)
                return Problem(HttpStatusCode.BadRequest, "未建立任何公司信息");
            var cmpDto = cmpDtos.FirstOrDefault(x => x.Id == cid);
            var company = Mapper.Map<Da_companyinfo>(cmpDto);
            if (company == null)
                return Problem(HttpStatusCode.BadRequest, "公司不存在");
            company.Insider = company.Insider.Replace($",{aid}", "");
            await _companyinfoRepository.UpdateAsync(company, UpdatingProps<Da_companyinfo>(x => x.Insider));
            var userinfoList = await _userinfoRepository.Where(x => x.Cid == company.Id, noTracking:false).ToListAsync();
            userinfoList.ForEach(x => x.Cid = 0);
            await _userinfoRepository.UpdateRangeAsync(userinfoList);
            return AppSrvResult();
        }

        public async Task<AppSrvResult<List<AccountDto>>> GetAccountListAsync(long cid)
        {
            var cmpDtos = await _cacheService.GetAllCompanysFromCacheAsync();
            if (cmpDtos == null)
                return Problem(HttpStatusCode.NotFound, "未建立任何公司信息");
            var cmpDto = cmpDtos.FirstOrDefault(x => x.Id ==  cid);
            if(cmpDto.Insider.IsNullOrWhiteSpace())
                return Problem(HttpStatusCode.NotFound, "指定公司不存在");
            string[] ids = cmpDto.Insider.Split(',');
            var allaccount = await _cacheService.GetAllAccountFromCacheAsync();
            if (allaccount == null)
                return Problem(HttpStatusCode.NotFound, "无任何账号信息,请先建立账号");
            long[] longIds = ids.Select(id => long.Parse(id)).ToArray();
            allaccount = allaccount.Where(x => longIds.Contains(x.Id)).ToList();
            return allaccount;
        }
    }
}
