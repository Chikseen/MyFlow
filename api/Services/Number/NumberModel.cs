public class Entry
{
    public int? counter_id { get; set; }
    public int? value { get; set; }
    public DateTime? date { get; set; }
    public DateTime? createdDate { get; set; }

    public Entry(int counter_id, int value, DateTime date, DateTime createdDate)
    {
        this.counter_id = counter_id;
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