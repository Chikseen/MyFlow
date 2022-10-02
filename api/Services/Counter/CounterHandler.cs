public class CounterHandler
{
    public Boolean createNewCounter(User user, CreateCounter createCounter)
    {
        String sql = $"INSERT INTO counter(user_id, name) VALUES ('{user.authid}', '{createCounter.name}');";
        List<List<String>> data = DatabaseService.query(sql);
        return true;
    }
}