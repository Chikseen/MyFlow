
public class Counter
{
    public int id { get; set; }
    public String? authid { get; set; }
    public String? name { get; set; }
    public DateTime created { get; set; }
    public NumberOverview numbers { get; set; }

    public Counter(int id, String? authid, String? name, DateTime created, int numbersValue, DateTime? numbersDate, String numbersUnit)
    {
        this.id = id;
        this.authid = authid;
        this.name = name;
        this.created = created;
        this.numbers = new NumberOverview(numbersValue, numbersDate, numbersUnit);
    }
}

public class CreateCounter
{
    public String? name { get; set; }
}

public class DeleteCounter
{
    public int id { get; set; }
}