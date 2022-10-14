using dotenv.net;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class OAuthContoller : ControllerBase
{
    private GithubAuth githubAuth;
    private GoogleAuth googleAuth;
    private UserHandler user;

    public OAuthContoller()
    {
        githubAuth = new GithubAuth();
        googleAuth = new GoogleAuth();
        user = new UserHandler();
    }

    [HttpGet("github")]
    public async Task<ActionResult> authGithub(String code)
    {
        DotEnv.Load();
        String redirect = Environment.GetEnvironmentVariable("REDIRECT_AFTER_LOGIN")!;

        HttpContext.Response.Cookies.Append("access_token", await githubAuth.userLogin(code));
        HttpContext.Response.Cookies.Append("auth_provider", "0");

        return RedirectPermanentPreserveMethod(redirect);
    }

    [HttpGet("google")]
    public async Task<ActionResult> authGoogle(String code)
    {
        DotEnv.Load();
        String redirect = Environment.GetEnvironmentVariable("REDIRECT_AFTER_LOGIN")!;

        HttpContext.Response.Cookies.Append("access_token", await googleAuth.userLogin(code));
        HttpContext.Response.Cookies.Append("auth_provider", "1");

        return RedirectPermanentPreserveMethod(redirect);
    }

    [HttpGet("checkuser")]
    public async Task<ActionResult> checkuser()
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        User userData = await user.checkUser(cookies);
        if (userData != null)
            return Ok(userData);
        else
            return Unauthorized();
    }
}
