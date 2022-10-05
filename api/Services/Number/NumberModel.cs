public class Entry
{
    public int? id { get; set; }
    public int? value { get; set; }
    public DateTime? date { get; set; }
    public DateTime? createdDate { get; set; }

    public Entry(int id, int value, DateTime date, DateTime createdDate)
    {
        this.id = id;
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

public class DeleteNumbers
{
    public User? user { get; set; }
    public int id { get; set; }

    public DeleteNumbers(User user, int id)
    {
        this.user = user;
        this.id = id;
    }
}