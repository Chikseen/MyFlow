
public class UserHandler
{
    public void saveUser(String name, String id, String logoURL)
    {
        String sql = $"INSERT INTO alluser(name, authid, logoURL) VALUES ('{name}', '{id}','{logoURL}') ON CONFLICT DO NOTHING;";
        DatabaseService.query(sql);
        //return "addedImages";
    }

    public async Task<User> checkUser(UserCookies cookies)
    {
        if (((cookies.access_token != null) && (cookies.auth_provider != null)) && (cookies.access_token.Length > 0 && cookies.auth_provider.Length > 0))
        {
            switch (cookies.auth_provider)
            {
                case "0":
                    {
                        GithubAuth auth = new GithubAuth();
                        User user = await auth.getUserDataFromAT(cookies.access_token);
                        String sql = $"SELECT name, authid, logourl FROM alluser WHERE authid = '{user.authid}'";
                        List<List<String>> data = DatabaseService.query(sql);
                        return new User(data[0][0], data[0][1], data[0][2]);
                    }
                case "1":
                    {
                        GoogleAuth auth = new GoogleAuth();
                        User user = await auth.getUserDataFromAT(cookies.access_token);
                        String sql = $"SELECT name, authid, logourl FROM alluser WHERE authid = '{user.authid}'";
                        List<List<String>> data = DatabaseService.query(sql);
                        return new User(data[0][0], data[0][1], data[0][2]);
                    }
                default:
                    return null!;
            }
        }
        else
        {
            return null!;
        }
    }

    public Boolean hasUserAccessforCounter(User user, int id)
    {
        String sql = $"SELECT id, user_id FROM counter WHERE user_id = '{user.authid}' AND id = '{id}'";
        List<List<String>> data = DatabaseService.query(sql);
        if (data.Count > 0)
            return true;
        else
            return false;
    }
}