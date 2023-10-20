// See https://aka.ms/new-console-template for more information

using Commons.Messages;
using Microsoft.AspNetCore.SignalR.Client;
using SignalsChat.State;

Console.WriteLine("Debug Client for SignalsChat is starting...");
var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5213/chatHub")
    .Build();
    
connection.On<string>("ReceiveDebugMessage", message => Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {message}"));
connection.On<CurrentClientsMessage>("ReceiveCurrentClients", message =>
{
    Console.WriteLine("Current clients:");
    Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {message.TimeStamp}");
    foreach (var client in message.Clients)
    {
        Console.WriteLine($"- {client}");
    }
});

connection.On<Message>("ReceiveMessage", message =>
{
    var user = message.Client == connection.ConnectionId ? "You" : message.Client;
    Console.ForegroundColor = user == "You" ? ConsoleColor.Green : ConsoleColor.White;

    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - {user}: {message.Text}");
});

await connection.StartAsync();

Console.WriteLine("Provide a message to send to the chat:");
var msg = "";
while (msg.ToLower() != "exit")
{
    msg = Console.ReadLine();
    await connection.SendAsync("SendMessage", msg);
} 

Console.ReadLine();
