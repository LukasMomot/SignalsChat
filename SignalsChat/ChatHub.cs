using Microsoft.AspNetCore.SignalR;

namespace SignalsChat;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        // log connection in the debug console
        Console.WriteLine($"Client connected: {Context.ConnectionId}");

        Clients.Client(Context.ConnectionId).SendAsync("ReceiveDebugMessage", $"Welcome to the chat, {Context.ConnectionId}!");
        return base.OnConnectedAsync();
    }
}