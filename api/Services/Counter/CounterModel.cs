
public class Counter
{
    public int id { get; set; }
    public String? authid { get; set; }
    public String? name { get; set; }
    public DateTime created { get; set; }

    public Counter(int id, String? authid, String? name, DateTime created)
    {
        this.id = id;
        this.authid = authid;
        this.name = name;
        this.created = created;
    }
}

public class CreateCounter
{
    public String? name { get; set; }
}