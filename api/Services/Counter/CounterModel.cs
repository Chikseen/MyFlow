
public class Counter
{
    public int id { get; set; }
    public String? authid { get; set; }
    public String? name { get; set; }
    public DateTime created { get; set; }
    public String unit { get; set; }
    public NumberOverview numbers { get; set; }

    public Counter(int id, String? authid, String? name, DateTime created, String unit, int numbersValue, DateTime? numbersDate)
    {
        this.id = id;
        this.authid = authid;
        this.name = name;
        this.created = created;
        this.unit = unit;
        this.numbers = new NumberOverview(numbersValue, numbersDate);
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
public class PutCounter
{
    public String? unit { get; set; }
    public String? name { get; set; }
}