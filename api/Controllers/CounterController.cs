using dotenv.net;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("numbers")]
public class CounterContoller : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> getAllCounter()
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        UserHandler user = new UserHandler();
        User userData = await user.checkUser(cookies);
        if (userData.name != null && userData.authid != null)
            return Ok(user);
        else
            return Unauthorized();
    }

    [HttpPost()]
    public async Task<ActionResult> createNewCounter(CreateCounter createCounter)
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        UserHandler user = new UserHandler();
        User userData = await user.checkUser(cookies);

        CounterHandler counterHandler = new CounterHandler();

        if (userData != null)
        {
            counterHandler.createNewCounter(userData, createCounter);
            return Ok(user);
        }
        else
            return Unauthorized();
    }
}
