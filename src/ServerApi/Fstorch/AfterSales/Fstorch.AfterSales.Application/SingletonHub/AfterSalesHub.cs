using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Fstorch.AfterSales.Application.SingletonHub
{
    public class AfterSalesHub : Hub
    {

        private readonly ILogger<AfterSalesHub> _logger;
        private readonly NotificationServiceFromSignalR _notificationService;
        private readonly ISignalRAppService _signalRAppService;

        public AfterSalesHub(ILogger<AfterSalesHub> logger, NotificationServiceFromSignalR notificationService, ISignalRAppService signalRAppService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _signalRAppService = signalRAppService;
        }

        /// <summary>
        /// 连接事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            try
            {
                var query = Context.GetHttpContext().Request.Query;
                var userid = query["uid"].FirstOrDefault();
                var companyid = query["cid"].FirstOrDefault();
                //绑定用户
                await _signalRAppService.BindSignalRUser((long)companyid.ToLong(), userid, Context.ConnectionId);
                await base.OnConnectedAsync();
                _notificationService.Subscribe(Context.ConnectionId);
                _notificationService.Publish(JsonConvert.SerializeObject(new { ClientId = Context.ConnectionId, CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userId = userid }), Context.ConnectionId);
                _logger.LogInformation("SignalR Client Connected");
            }
            catch(Exception ex)
            {
                _notificationService.Publish("连接参数不正确", Context.ConnectionId);
                _notificationService.Unsubscribe(Context.ConnectionId);
                _logger.LogError("SignalR Client Connect Fail:{message}", ex.Message);
            }
        }

        /// <summary>
        /// 断连事件
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception != null)
            {
                _logger.LogError(exception, "Client disconnected with error");
            }
            else
            {
                _logger.LogInformation("Client disconnected");
            }
            _notificationService.Unsubscribe(Context.ConnectionId);
            //解绑用户
            await _signalRAppService.UnBindSignalRUser(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }


        /// <summary>
        /// 广播
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task NotifyClients(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
