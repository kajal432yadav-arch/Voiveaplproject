namespace VoiceAPI.Models;

public class ChatRequest
{
    public string model { get; set; } = "sarvam-m";

    public List<Message> messages { get; set; } = new();
}

public class Message
{
    public string role { get; set; } = "";
    public string content { get; set; } = "";
}