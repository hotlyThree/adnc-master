//var webApiAssembly = System.Reflection.Assembly.GetExecutingAssembly();

var startAssembly = System.Reflection.Assembly.GetExecutingAssembly();
var startAssemblyName = startAssembly.GetName().Name ?? string.Empty;
var lastName = startAssemblyName.Split('.').Last();
var serviceInfo = ServiceInfo.CreateInstance(startAssembly, migrationsAssemblyName : startAssemblyName);
var builder = WebApplication
    .CreateBuilder(args);
var config = builder
    .ConfigureAdncDefault(serviceInfo);
config.Services.AddHttpClient();
//config.Services.AddOfficeAccountSubScribe();
var app = config.Build();

/*app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            CustomerWebSocketManager.ConnectedSockets.Add(webSocket);
            var connectionConfirmationMessage = Encoding.UTF8.GetBytes("heart");
            await webSocket.SendAsync(new ArraySegment<byte>(connectionConfirmationMessage), WebSocketMessageType.Text, true, CancellationToken.None);
            // 心跳逻辑
            while (webSocket.State == WebSocketState.Open)
            {
                // 定时发送心跳包，每5秒一次
                await Task.Delay(TimeSpan.FromSeconds(5));

                var pingMessage = Encoding.UTF8.GetBytes("Ping");
                await webSocket.SendAsync(new ArraySegment<byte>(pingMessage), WebSocketMessageType.Text, true, CancellationToken.None);

                // 检查连接状态
                if (webSocket.State == WebSocketState.Open)
                {
                    Console.WriteLine("连接状态: 打开");
                }
                else if (webSocket.State == WebSocketState.CloseReceived || webSocket.State == WebSocketState.CloseSent)
                {
                    Console.WriteLine("连接状态: 已关闭或正在关闭");
                }
                Thread.Sleep(5000);
            }
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});*/

app.UseAdnc();
// Add services to the container.

app.ChangeThreadPoolSettings()
    .UseRegistrationCenter();
app.MapGet("/", async context =>
{
    var content = serviceInfo.GetDefaultPageContent(app.Services);
    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync(content);
});
// Configure the HTTP request pipeline.

await app.RunAsync();
