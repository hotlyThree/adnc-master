using Adnc.Shared.WebApi;
using Fstorch.AfterSales.Application.SingletonHub;
using StackExchange.Redis;

var startAssembly = System.Reflection.Assembly.GetExecutingAssembly();
var startAssemblyName = startAssembly.GetName().Name ?? string.Empty;
var lastName = startAssemblyName.Split('.').Last();
var serviceInfo = ServiceInfo.CreateInstance(startAssembly, migrationsAssemblyName: startAssemblyName);
var builder = WebApplication
    .CreateBuilder(args);
var config = builder
    .ConfigureAdncDefault(serviceInfo);
config.Services.AddHttpClient();
//config.Services.AddOfficeAccountSubScribe();

var configuration = builder.Configuration["Redis:Dbconfig:ConnectionString"];
// ���� Redis��ΪSignalR��̨
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var cfg = ConfigurationOptions.Parse(configuration);
    return ConnectionMultiplexer.Connect(cfg);
});
/*var redisOptions = builder.Configuration.GetSection("Redis:Dbconfig:ConnectionString").Get<RedisOptions>();
config.Services.AddSingleton(redisOptions);*/

/*
var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(builder.Configuration["Redis:Dbconfig:ConnectionString"]);
var db = connectionMultiplexer.GetDatabase();
await db.StringSetAsync("testkey", "testvalue");
var value = await db.StringGetAsync("testkey");
if (value == "testvalue")
{
    Console.WriteLine("Connected to Redis successfully.");
}
else
{
    Console.WriteLine("Failed to connect to Redis.");
}*/

// ע�� NotificationServiceFromSignalR
builder.Services.AddSingleton<NotificationServiceFromSignalR>();

// ���� SignalR ʹ�� Redis ��Ϊ���
builder.Services.AddSignalR().AddStackExchangeRedis(configuration);


var app = config.Build();
/*
app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var clientId = "";
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var buffer = new byte[1024 * 4]; // ������������С����Ӧ���ܵ���Ϣ��С
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var receivedText = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var message = JsonConvert.DeserializeObject<Dictionary<string, string>>(receivedText);
                clientId = message["clientId"];

                if (!string.IsNullOrEmpty(clientId))
                {
                    CustomerWebSocketManager.AddOrUpdateSocket(clientId, webSocket);
                    var connectionConfirmationMessage = Encoding.UTF8.GetBytes("Connection confirmed");
                    await webSocket.SendAsync(new ArraySegment<byte>(connectionConfirmationMessage), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "Client ID is required", CancellationToken.None);
                }
            }
            else
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "Expected text message", CancellationToken.None);
            }

            // �����߼�
            while (webSocket.State == WebSocketState.Open)
            {
                // ��ʱ������������ÿ5��һ��
                await Task.Delay(TimeSpan.FromSeconds(5));

                var pingMessage = Encoding.UTF8.GetBytes("ok");
                await webSocket.SendAsync(new ArraySegment<byte>(pingMessage), WebSocketMessageType.Text, true, CancellationToken.None);

                // �������״̬
                if (webSocket.State == WebSocketState.Open)
                {
                    Console.WriteLine("����״̬: ��");
                }
                else if (webSocket.State == WebSocketState.CloseReceived || webSocket.State == WebSocketState.CloseSent)
                {
                    Console.WriteLine("����״̬: �ѹرջ����ڹر�");
                    CustomerWebSocketManager.TryRemoveSocket(clientId);
                    break;
                }
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
});
*/


//app.UseRouting();
//app.UseAuthorization();

app.UseAdnc();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AfterSalesHub>("/fshub");
});
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
