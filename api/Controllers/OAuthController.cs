using Microsoft.AspNetCore.Mvc;
using dotenv.net;

[ApiController]
[Route("auth")]
public class OAuthContoller : ControllerBase
{

    private String githubClientID;
    private String githubClientSecret;
    private readonly HttpClient client;
    private UserHandler userHandler;

    public OAuthContoller()
    {
        DotEnv.Load();
        client = new HttpClient();
        githubClientID = Environment.GetEnvironmentVariable("GITHUB_CLIENT_ID")!;
        githubClientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENT_SECRET")!;
        userHandler = new UserHandler();
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
        var queryValues = responseString.Split('&').Select(q => q.Split('='))
                           .ToDictionary(k => k[0], v => v[1]);

        userHandler.saveUser(queryValues["access_token"]);

        return Ok(responseString);
    }

    [HttpGet("test")]
    public ActionResult test()
    {
        return Ok();
    }
}
