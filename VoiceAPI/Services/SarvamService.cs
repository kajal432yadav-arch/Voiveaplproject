using Microsoft.Extensions.Options;
using VoiceAPI.Models;
using System.Text.Json;

namespace VoiceAPI.Services;

public class SarvamService : ISarvamService
{
    private readonly HttpClient _httpClient;
    private readonly SarvamOptions _options;

    public SarvamService(
        HttpClient httpClient,
        IOptions<SarvamOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> AskAsync(string prompt)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Add(
            "api-subscription-key",
            _options.ApiKey);

        var request = new ChatRequest
        {
            model = "sarvam-30b",
            messages =
            [
                new Message
                {
                    role = "user",
                    content = prompt
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/v1/chat/completions",
            request);

        var json = await response.Content.ReadAsStringAsync();

        // ✅ Parse and return only the assistant's message
        using var doc = JsonDocument.Parse(json);
        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return content ?? "No response received.";
    }
}