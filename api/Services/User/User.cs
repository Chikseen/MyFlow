
public class User
{
    public String name { get; set; }
    public String authid { get; set; }

    public User(String name, String authid)
    {
        this.name = name;
        this.authid = authid;
    }
}