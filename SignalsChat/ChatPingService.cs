using Commons.Messages;
using Microsoft.AspNetCore.SignalR;
using SignalsChat.State;

namespace SignalsChat;

public class ChatPingService : BackgroundService
{
    public ChatPingService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    private readonly IHubContext<ChatHub> _hubContext;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("ChatPingService is starting...");
        while (!stoppingToken.IsCancellationRequested)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveDebugMessage", "Ping!");
            
            //await _hubContext.Clients.All.SendAsync("ReceiveDebugMessage", $"There are {_hubContext.Clients.All.CountAsync().Result} clients connected.");
            var currentClientsMsg = new CurrentClientsMessage()
            {
                TimeStamp = DateTime.Now,
                Clients = ChatState.Clients.ToArray()
            };
            
            await _hubContext.Clients.All.SendAsync("ReceiveCurrentClients", currentClientsMsg);
            await Task.Delay(5000, stoppingToken);
        }
    }
}