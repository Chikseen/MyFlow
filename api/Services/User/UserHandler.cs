using Microsoft.AspNetCore.Mvc;

public class UserHandler : ControllerBase
{
    public void saveUser(String name, String id, String logoURL)
    {
        String sql = $"INSERT INTO alluser(name, authid, logoURL) VALUES ('{name}', '{id}','{logoURL}') ON CONFLICT DO NOTHING;";
        DatabaseService.query(sql);
        //return "addedImages";
    }

    public async Task<ActionResult> checkUser(UserCookies cookies)
    {
        if (((cookies.access_token != null) && (cookies.auth_provider != null)) && (cookies.access_token.Length > 0 && cookies.auth_provider.Length > 0))
        {
            switch (cookies.auth_provider)
            {
                case "0":
                    {
                        GithubAuth auth = new GithubAuth();
                        User user = await auth.getUserDataFromAT(cookies.access_token);
                        String sql = $"SELECT name, authid FROM alluser WHERE authid = '{user.authid}'";
                        List<List<String>> data = DatabaseService.query(sql);
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