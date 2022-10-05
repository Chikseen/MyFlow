public class NumberHandler
{
    UserHandler userHandler = new UserHandler();

    public List<Entry> getCounter(User user, int id)
    {
        if (userHandler.hasUserAccessforCounter(user, id))
        {
            String getIdSql = @$" SELECT numbers.id, value, date, numbers.created FROM numbers
                                INNER JOIN counter
                                ON counter.user_id = '{user.authid}'
                                AND counter.id = numbers.counter_id
                                AND numbers.counter_id = {id}";
            List<List<String>> data = DatabaseService.query(getIdSql);

            List<Entry> allEntry = new List<Entry>();
            for (int i = 0; i < data.Count; i++)
            {
                allEntry.Add(new Entry(Int32.Parse(data[i][0]), Int32.Parse(data[i][1]), DateTime.Parse(data[i][2]), DateTime.Parse(data[i][3])));
            }
            return allEntry;
        }
        else return null!;
    }

    public Entry saveNumbers(User user, CreateEntry entry)
    {
        if (userHandler.hasUserAccessforCounter(user, entry.id ?? default(int)))
        {
            String sql = $"INSERT INTO numbers(counter_id, value, date) VALUES ('{entry.id}', '{entry.value}', '{entry.date}');";
            List<List<String>> data = DatabaseService.query(sql);

            return new Entry(entry.id ?? default(int), entry.value ?? default(int), DateTime.Parse(entry.date!), DateTime.Now); // Send time with timezone -> fix later
        }
        else return null!;
    }
}
