using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Adnc.Fstorch.Shared.Web.Socket
{
    public static class CustomerWebSocketManager
    {
        private static readonly ConcurrentDictionary<string, WebSocket> _connectedSockets = new ConcurrentDictionary<string, WebSocket>();

        /// <summary>
        /// 管理客户端连接
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="webSocket"></param>
        public static void AddOrUpdateSocket(string clientId, WebSocket webSocket)
        {
            _connectedSockets.AddOrUpdate(clientId, webSocket, (key, oldValue) => webSocket);
        }

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static bool TryRemoveSocket(string clientId)
        {
            return _connectedSockets.TryRemove(clientId, out _);
        }


        /// <summary>
        /// 客户端是否存在
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public static bool TryGetSocket(string clientId, out WebSocket webSocket)
        {
            return _connectedSockets.TryGetValue(clientId, out webSocket);
        }

        /// <summary>
        /// 指定客户端发送消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task SendMessageToClientAsync(string clientId, string message)
        {
            if (TryGetSocket(clientId, out var socket))
            {
                try
                {
                    await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    // 记录日志
                    Console.WriteLine($"Error sending message to {clientId}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 通知客户端
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task NotifyClientAsync(string userId, string message)
        {
            foreach (var pair in _connectedSockets)
            {
                if (pair.Key == userId)
                {
                    await SendMessageToClientAsync(userId, message);
                    return;
                }
            }
        }
    }
}