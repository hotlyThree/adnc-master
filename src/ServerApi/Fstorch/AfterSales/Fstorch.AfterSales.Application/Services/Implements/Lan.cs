namespace Fstorch.AfterSales.Application.Services.Implements
{
    public static class LanguageMsgService
    {

        public static async Task<string> GetMsg(ILogger _logger, ISystemRestClient _systemRestClient, string func, string funcId)
        {
            string errMsg = "";
            try
            {
                var response = await _systemRestClient.FindLanguageAsync(func, funcId);
                if (response.IsSuccessStatusCode)
                    errMsg = response.Content;
            }
            catch (Exception ex)
            {
                _logger.LogError("获取其他语言提示信息时出错:{message}", ex.Message);
            }
            return errMsg;
        }
    }
}