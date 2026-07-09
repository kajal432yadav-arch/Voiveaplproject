namespace VoiceAPI.Services;

public interface ISarvamService
{
    Task<string> AskAsync(string prompt);
}