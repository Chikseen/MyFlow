using dotenv.net;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("numbers")]
public class CounterContoller : ControllerBase
{
    private UserHandler user;
    private CounterHandler counterHandler;
    public CounterContoller()
    {
        user = new UserHandler();
        counterHandler = new CounterHandler();
    }

    [HttpGet()]
    public async Task<ActionResult> getAllCounter()
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        User userData = await user.checkUser(cookies);
        if (userData != null)
            return Ok(user);
        else
            return Unauthorized();
    }

    [HttpPost()]
    public async Task<ActionResult> createNewCounter(CreateCounter createCounter)
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        User userData = await user.checkUser(cookies);

        if (userData != null)
        {
            counterHandler.createNewCounter(userData, createCounter);
            return Ok(user);
        }
        else
            return Unauthorized();
    }
}
