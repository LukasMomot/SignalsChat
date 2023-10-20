using Microsoft.AspNetCore.SignalR;

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
            await Task.Delay(5000, stoppingToken);
        }
    }
}