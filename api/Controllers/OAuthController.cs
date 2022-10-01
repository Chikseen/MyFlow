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
        UserCookies coockies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);

        if (((coockies.access_token != null) && (coockies.auth_provider != null)) && (coockies.access_token.Length > 0 && coockies.auth_provider.Length > 0))
        {
            switch (coockies.auth_provider)
            {
                case "0":
                    {
                        GithubAuth auth = new GithubAuth();
                        User user = await auth.getUserDataFromAT(coockies.access_token);
                        String sql = $"SELECT name, authid FROM alluser WHERE authid = '{user.authid}'";
                        List<List<String>> data = DatabaseService.query(sql);
                        Console.WriteLine(data[0][0].ToString());
                        return Ok(new User(data[0][0].ToString(), data[0][1].ToString()));
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
