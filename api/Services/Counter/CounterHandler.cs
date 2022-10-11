using dotenv.net;

public class CounterHandler
{
    public Counter createNewCounter(User user, CreateCounter createCounter)
    {
        String sql = @$"
            INSERT INTO counter(user_id, name) VALUES ('{user.authid}', '{createCounter.name}')
            RETURNING *;";
        List<List<String>> data = DatabaseService.query(sql);

        Counter counter = new Counter(Int32.Parse(data[0][0]), data[0][1], data[0][2], DateTime.Parse(data[0][3]), -1, null, "-");
        return counter;
    }

    public List<Counter> getAllCounter(User user)
    {
        String getCounter = @$"
            SELECT *
            FROM counter 
            WHERE user_id = '{user.authid}'
            ORDER BY id ASC;";
        List<List<String>> counter = DatabaseService.query(getCounter);

        List<Counter> allCounter = new List<Counter>();
        for (int i = 0; i < counter.Count; i++)
        {
            String getNumber = @$"SELECT counter_id, value, date, unit FROM numbers WHERE counter_id = '{counter[i][0]}' ORDER BY date DESC LIMIT 1;";
            List<List<String>> numbers = DatabaseService.query(getNumber);
            if (numbers.Count > 0)
            {
                DotEnv.Load();
                String os = Environment.GetEnvironmentVariable("OS")!;

                String[] datearr;
                DateTime date;
                if (os == "w")
                    datearr = numbers[0][2].Split(" ")[0].Split(".");
                else
                    datearr = numbers[0][2].Split(" ")[0].Split("/");

                date = new DateTime(Int32.Parse(datearr[2]), Int32.Parse(datearr[1]), Int32.Parse(datearr[0]));
                allCounter.Add(new Counter(Int32.Parse(counter[i][0]), counter[i][1], counter[i][2], DateTime.Parse(counter[i][3]), Int32.Parse(numbers[0][1]), date, numbers[0][3]));
            }
            else
                allCounter.Add(new Counter(Int32.Parse(counter[i][0]), counter[i][1], counter[i][2], DateTime.Parse(counter[i][3]), -1, null, "-"));
        }
        return allCounter;
    }

    public Boolean deleteCounter(User user, int id)
    {
        try
        {
            String getIdSql = @$"DELETE FROM counter WHERE id='{id}' AND user_id = '{user.authid}';";
            List<List<String>> data = DatabaseService.query(getIdSql);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}