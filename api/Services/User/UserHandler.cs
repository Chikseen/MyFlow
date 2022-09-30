public class UserHandler
{
    public void saveUser(String access_token)
    {
        String sql = $"INSERT INTO alluser(token) VALUES ('{access_token}');";
        DatabaseService.query(sql);
        Console.WriteLine("hi");
        //return "addedImages";
    }
}