using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class OAuthContoller : ControllerBase
{
    [HttpGet("github")]
    public async Task<ActionResult> AuthGithub(String code)
    {
        GithubAuth auth = new GithubAuth();
        return Ok(await auth.userLogin(code));
    }
}
