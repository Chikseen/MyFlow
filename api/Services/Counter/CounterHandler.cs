public class CounterHandler
{
    public Counter createNewCounter(User user, CreateCounter createCounter)
    {
        String sql = $"INSERT INTO counter(user_id, name) VALUES ('{user.authid}', '{createCounter.name}');";
        DatabaseService.query(sql);
        String getIdSql = @$" SELECT *
                                FROM counter 
                                WHERE user_id = '{user.authid}' 
                                    AND name = '{createCounter.name}'
                                ORDER BY id DESC;;";
        List<List<String>> data = DatabaseService.query(getIdSql);

        Counter counter = new Counter(Int32.Parse(data[0][0]), data[0][1], data[0][2], DateTime.Parse(data[0][3]));
        return counter;
    }

    public List<Counter> getAllCounter(User user)
    {
        String getIdSql = @$" SELECT *
                                FROM counter 
                                WHERE user_id = '{user.authid}'
                                ORDER BY id ASC;";
        List<List<String>> data = DatabaseService.query(getIdSql);

        List<Counter> allCounter = new List<Counter>();
        for (int i = 0; i < data.Count; i++)
        {
            allCounter.Add(new Counter(Int32.Parse(data[i][0]), data[i][1], data[i][2], DateTime.Parse(data[i][3])));
        }

        return allCounter;
    }
}