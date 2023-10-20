namespace Commons.Messages;

public record CurrentClientsMessage
{
    public DateTime TimeStamp { get; set; }
    
    public string[] Clients { get; set; }
}