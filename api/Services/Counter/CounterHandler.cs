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

    public List<Entry> getCounter(int id)
    {
        String getIdSql = @$" SELECT value, date, created
                                FROM numbers 
                                WHERE counter_id = '{id}';";
        List<List<String>> data = DatabaseService.query(getIdSql);

        List<Entry> allEntry = new List<Entry>();
        for (int i = 0; i < data.Count; i++)
        {
            allEntry.Add(new Entry(Int32.Parse(data[i][0]), DateTime.Parse(data[i][1]), DateTime.Parse(data[i][2])));
        }
        return allEntry;
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

    public Entry saveNumbers(User user, CreateEntry entry)
    {
        String sql = $"INSERT INTO numbers(counter_id, value, date) VALUES ('{entry.id}', '{entry.value}', '{entry.date}');";

        List<List<String>> data = DatabaseService.query(sql);
        /* 
                       List<Counter> allCounter = new List<Counter>();
                       for (int i = 0; i < data.Count; i++)
                       {
                           allCounter.Add(new Counter(Int32.Parse(data[i][0]), data[i][1], data[i][2], DateTime.Parse(data[i][3])));
                       }

                       return allCounter; */
        return null!;
    }
}