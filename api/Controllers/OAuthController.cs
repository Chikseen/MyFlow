using dotenv.net;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class OAuthContoller : ControllerBase
{
    [HttpGet("github")]
    public async Task<ActionResult> authGithub(String code)
    {
        GithubAuth auth = new GithubAuth();

        DotEnv.Load();
        String redirect = Environment.GetEnvironmentVariable("REDIRECT_AFTER_LOGIN")!;

        HttpContext.Response.Cookies.Append("access_token", await auth.userLogin(code));
        HttpContext.Response.Cookies.Append("auth_provider", "0");

        return RedirectPermanentPreserveMethod(redirect);
    }

    [HttpGet("checkuser")]
    public async Task<ActionResult> checkuser()
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        UserHandler user = new UserHandler();
        User userData = await user.checkUser(cookies);
        if (userData != null)
            return Ok(userData);
        else
            return Unauthorized();
    }
}
