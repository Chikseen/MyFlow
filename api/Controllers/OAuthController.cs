using dotenv.net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class OAuthContoller : ControllerBase
{
    [HttpGet("github")]
    public async Task<ActionResult> authGithub(String code)
    {
        DotEnv.Load();
        String redirect = Environment.GetEnvironmentVariable("REDIRECT_AFTER_LOGIN")!;
        GithubAuth auth = new GithubAuth();
        HttpContext.Response.Cookies.Append("access_token", await auth.userLogin(code));
        HttpContext.Response.Cookies.Append("auth_provider", "0");
        return RedirectPermanentPreserveMethod(redirect);
    }

    [HttpGet("checkuser")]
    public async Task<ActionResult> checkuser()
    {
        String access_token = HttpContext.Request.Cookies["access_token"]!;
        String auth_provider = HttpContext.Request.Cookies["auth_provider"]!;

        Console.WriteLine("access_token", access_token);
        Console.WriteLine("auth_provider", auth_provider);

        if (((access_token != null) && (auth_provider != null)) && (access_token.Length > 0 && auth_provider.Length > 0))
        {
            switch (auth_provider)
            {
                case "0":
                    {
                        GithubAuth auth = new GithubAuth();
                        User user = await auth.getUserDataFromAT(access_token);
                        String sql = $"SELECT authid FROM alluser WHERE authid = '{user.authid}'";
                        List<List<String>> data = DatabaseService.query(sql);
                        Console.WriteLine(data[0][0]);
                        return Ok();
                    }
                default: return Unauthorized();
            }
        }
        else
        {
            return Unauthorized();
        }
    }
}
