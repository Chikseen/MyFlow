using Microsoft.AspNetCore.Mvc;
using dotenv.net;

[ApiController]
[Route("auth")]
public class OAuthContoller : ControllerBase
{

    private String githubClientID;
    private String githubClientSecret;
    private readonly HttpClient client;

    public OAuthContoller()
    {
        DotEnv.Load();
        client = new HttpClient();
        githubClientID = Environment.GetEnvironmentVariable("GITHUB_CLIENT_ID")!;
        githubClientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENT_SECRET")!;
    }

    [HttpGet("github")]
    public async Task<ActionResult> AuthGithub(String code)
    {
        var values = new Dictionary<string, string>
        {
            { "client_id", githubClientID },
            { "client_secret", githubClientSecret },
            { "code", code }
        };
        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("https://github.com/login/oauth/access_token", content);
        var responseString = await response.Content.ReadAsStringAsync();
        return Ok(responseString);
    }
}
