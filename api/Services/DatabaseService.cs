using Npgsql;
using dotenv.net;

public static class DatabaseService
{
    private static string name = "";
    private static string location = "";
    private static String postgresHost = "";
    private static String postgresPort = "";
    private static String postgresUser = "";
    private static String postgresPassword = "";
    private static String postgresDatabase = "";
    private static String conectionString = "";

    static DatabaseService()
    {
        DotEnv.Load();
        postgresHost = Environment.GetEnvironmentVariable("POSTGRES_HOST")!;
        postgresPort = Environment.GetEnvironmentVariable("POSTGRES_PORT")!;
        postgresUser = Environment.GetEnvironmentVariable("POSTGRES_USER")!;
        postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")!;
        postgresDatabase = Environment.GetEnvironmentVariable("POSTGRES_DATABASE")!;
        conectionString = $"Host={postgresHost};Port={postgresPort};Username={postgresUser};Password={postgresPassword};Database={postgresDatabase}";
        conectionString = $"Host=localhost;Port={postgresPort};Username={postgresUser};Password={postgresPassword};Database={postgresDatabase}";
    }

    public static List<List<String>> query(String sql)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conectionString))
        {
            var columns = new List<List<String>>();
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();

            Console.WriteLine(sql); //-- Debug
            NpgsqlCommand command = new NpgsqlCommand(sql, con);
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                var rows = new List<String>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString() == "System.String[]")
                    {
                        string[] myArray = dr.GetFieldValue<string[]>(i);
                        rows.Add(string.Join(",#,", myArray));
                    }
                    else if (dr[i].ToString() == "System.Int32[]")
                    {
                        int[] myArray = dr.GetFieldValue<int[]>(i);
                        rows.Add(string.Join(",#,", myArray));
                    }
                    else
                    {
                        rows.Add($"{dr[i]}");
                    }
                }
                columns.Add(rows);
            }
            con.Close();
            return columns;
        }
    }

    public static String dbInit()
    {
        Console.WriteLine("DB Init");
        using (NpgsqlConnection con = new NpgsqlConnection(conectionString))
        {
            // __________ USERTABLE __________
            try
            {
                con.Open();
                var sql = "CREATE TABLE IF NOT EXISTS alluser (authid VARCHAR(255) PRIMARY KEY, name VARCHAR(255) NOT NULL, logoURL VARCHAR(255), created TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP);";
                Console.WriteLine(sql);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader dr = command.ExecuteReader();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error While creating alluser Table");
                Console.WriteLine(e);
                con.Close();
                return "error_creating_alluser";
            }
            con.Close();

            // __________ COUNTERTABLE __________
            try
            {
                con.Open();
                var sql = "CREATE TABLE IF NOT EXISTS counter (id serial, user_id VARCHAR(255),name VARCHAR(255), created TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP, PRIMARY KEY (id), FOREIGN KEY (user_id) REFERENCES alluser(authid) ON DELETE CASCADE);";
                Console.WriteLine(sql);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader dr = command.ExecuteReader();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error While creating counter Table");
                Console.WriteLine(e);
                con.Close();
                return "error_creating_counter";
            }
            con.Close();
        }
        return "success";
    }
}