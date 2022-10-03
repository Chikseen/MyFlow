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

    [HttpGet("{id}")]
    public async Task<ActionResult> getCounter(int id)
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        User userData = await user.checkUser(cookies);
        if (userData != null)
            return Ok(counterHandler.getCounter(userData, id));
        else
            return Unauthorized();
    }

    [HttpGet()]
    public async Task<ActionResult> getAllCounter()
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        User userData = await user.checkUser(cookies);
        if (userData != null)
            return Ok(counterHandler.getAllCounter(userData));
        else
            return Unauthorized();
    }

    [HttpPost()]
    public async Task<ActionResult<Counter>> createNewCounter(CreateCounter createCounter)
    {
        UserCookies cookies = new UserCookies(HttpContext.Request.Cookies["access_token"]!, HttpContext.Request.Cookies["auth_provider"]!);
        User userData = await user.checkUser(cookies);
        if (userData != null)
        {
            return Ok(counterHandler.createNewCounter(userData, createCounter));
        }
        else
            return Unauthorized();
    }
}
