namespace SignalsChat.State;

public static class ChatState
{
    public static IList<string> Clients { get; } = new List<string>();

    public static IList<Message> Messages { get; } = new List<Message>();
}