using Microsoft.AspNetCore.SignalR;
using SignalsChat.State;

namespace SignalsChat;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        // log connection in the debug console
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        
        ChatState.Clients.Add(Context.ConnectionId);

        Clients.Client(Context.ConnectionId).SendAsync("ReceiveDebugMessage", $"Welcome to the chat, {Context.ConnectionId}!");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        ChatState.Clients.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
    
    public async Task SendMessage(string message)
    {
        var msg = new Message
        {
            Client = Context.ConnectionId,
            TimeStamp = DateTime.Now,
            Text = message
        };
        
        ChatState.Messages.Add(msg);
        
        await Clients.All.SendAsync("ReceiveMessage", msg);
    }
}