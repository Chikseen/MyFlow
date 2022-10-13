public class NumberHandler
{
    UserHandler userHandler = new UserHandler();

    public List<Entry> getNumber(User user, int id)
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
            String sql = $"INSERT INTO numbers(counter_id, value, date) VALUES ('{entry.id}', '{entry.value}', '{entry.date}') RETURNING id, value, date, created;";
            List<List<String>> data = DatabaseService.query(sql);

            return new Entry(Int32.Parse(data[0][0]), Int32.Parse(data[0][1]), DateTime.Parse(data[0][2]), DateTime.Parse(data[0][3]));
        }
        else return null!;
    }

    public Boolean deleteNumbers(DeleteNumbers deleteNumbers, int id)
    {
        if (userHandler.hasUserAccessforCounter(deleteNumbers.user!, id))
        {
            try
            {
                String sql = $"DELETE FROM numbers WHERE id='{deleteNumbers.id}' AND counter_id = '{id}';";
                DatabaseService.query(sql);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        return false;
    }
}
