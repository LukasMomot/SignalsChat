// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Debug Client for SignalsChat is starting...");
var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5213/chatHub")
    .Build();
    
connection.On<string>("ReceiveDebugMessage", message => Console.WriteLine($"{DateTime.Now.ToLongDateString()}: {message}"));

await connection.StartAsync();

Console.ReadLine();
