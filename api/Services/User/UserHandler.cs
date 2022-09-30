public class UserHandler
{
    public void saveUser(String name, String id, String logoURL)
    {
        String sql = $"INSERT INTO alluser(name, authid, logoURL) VALUES ('{name}', '{id}','{logoURL}');";
        DatabaseService.query(sql);
        Console.WriteLine("hi");
        //return "addedImages";
    }
}