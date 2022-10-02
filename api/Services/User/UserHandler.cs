public class UserHandler
{
    public void saveUser(String name, String id, String logoURL)
    {
        String sql = $"INSERT INTO alluser(name, authid, logoURL) VALUES ('{name}', '{id}','{logoURL}') ON CONFLICT DO NOTHING;";
        DatabaseService.query(sql);
        Console.WriteLine("hi");
        //return "addedImages";
    }
}