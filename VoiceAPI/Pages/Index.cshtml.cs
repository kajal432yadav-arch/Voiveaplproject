using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VoiceAPI.Services;
using VoiceAPI.Services;

namespace VoiceAPI.Pages;

public class IndexModel : PageModel
{
    private readonly ISarvamService _sarvamService;

    public IndexModel(ISarvamService sarvamService)
    {
        _sarvamService = sarvamService;
    }

    [BindProperty]
    public string Prompt { get; set; } = "";

    public string ResponseText { get; set; } = "";

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        ResponseText = await _sarvamService.AskAsync(Prompt);
    }
}