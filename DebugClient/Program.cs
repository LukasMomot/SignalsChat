// See https://aka.ms/new-console-template for more information

using Commons.Messages;
using Microsoft.AspNetCore.SignalR.Client;

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

await connection.StartAsync();

Console.ReadLine();
