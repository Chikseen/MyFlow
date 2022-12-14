
public class User
{
    public String name { get; set; }
    public String authid { get; set; }
    public String pictureURI { get; set; }
    public User(String name, String authid, String pictureURI)
    {
        this.name = name;
        this.authid = authid;
        this.pictureURI = pictureURI;
    }
}

public class UserCookies
{
    public String access_token { get; set; }
    public String auth_provider { get; set; }

    public UserCookies(String access_token, String auth_provider)
    {
        this.access_token = access_token;
        this.auth_provider = auth_provider;
    }
}