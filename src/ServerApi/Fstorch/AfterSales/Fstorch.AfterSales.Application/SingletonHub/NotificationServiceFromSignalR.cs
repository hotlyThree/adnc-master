using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace Fstorch.AfterSales.Application.SingletonHub
{
    /// <summary>
    /// Redis订阅 / 发布服务
    /// </summary>
    public class NotificationServiceFromSignalR
    {
        private readonly IHubContext<AfterSalesHub> _hubContext;
        private readonly IConnectionMultiplexer _redisConnectionMultiplexer;
        private readonly Dictionary<string, (ISubscriber subscriber, RedisChannel channel)> _subscriptions = new Dictionary<string, (ISubscriber, RedisChannel)>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="redisConnectionMultiplexer"></param>
        public NotificationServiceFromSignalR(IHubContext<AfterSalesHub> hubContext, IConnectionMultiplexer redisConnectionMultiplexer)
        {
            _hubContext = hubContext;
            _redisConnectionMultiplexer = redisConnectionMultiplexer;
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="clientId"></param>
        public void Subscribe(string clientId)
        {
            var subscriber = _redisConnectionMultiplexer.GetSubscriber();
            var channel = "notifications";//频道可拓展为动态
            var handler = new Action<RedisChannel, RedisValue>((ch, value) =>
            {
                var notification = JsonConvert.DeserializeObject<Notification>(Encoding.UTF8.GetString(value));
                if (notification.ClientId == clientId)
                {
                    _hubContext.Clients.Client(clientId).SendAsync("ReceiveNotification", notification.Message).Wait();
                }
            });
            subscriber.Subscribe(channel, handler);
            _subscriptions[clientId] = (subscriber, channel);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientId"></param>
        public void Publish(string message, string clientId)
        {
            var notification = new Notification { Message = message, ClientId = clientId };
            var serializedMessage = JsonConvert.SerializeObject(notification);
            var bytes = Encoding.UTF8.GetBytes(serializedMessage);
            var publisher = _redisConnectionMultiplexer.GetSubscriber();
            publisher.Publish(channel: "notifications", message: bytes);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="clientId"></param>
        public void Unsubscribe(string clientId)
        {
            if (_subscriptions.TryGetValue(clientId, out var value))
            {
                value.subscriber.Unsubscribe(value.channel);
                _subscriptions.Remove(clientId);
            }
        }
    }

    /// <summary>
    /// 消息体
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }
    }
}
