namespace SignalsChat.State;

public record Message
{
    public string Client { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Text { get; set; }
}