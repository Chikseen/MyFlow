
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

public class Entry
{
    public int? value { get; set; }
    public DateTime? date { get; set; }
    public DateTime? createdDate { get; set; }

    public Entry(int value, DateTime date, DateTime createdDate)
    {
        this.value = value;
        this.date = date;
        this.createdDate = createdDate;
    }
}

public class CreateEntry
{
    public int? id { get; set; }
    public int? value { get; set; }
    public String? date { get; set; }
}