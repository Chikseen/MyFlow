public class GithubAT
{
    public String? access_token { get; set; }
    public String? token_type { get; set; }
}

public class GithubData
{
    public String? id { get; set; }
    public String? login { get; set; }
    public String? avatar_url { get; set; }
}

public class GoogleAT
{
    public String? access_token { get; set; }
    public String? expires_in { get; set; }
    public String? scope { get; set; }
    public String? token_type { get; set; }
    public String? id_token { get; set; }
}

public class GoogleData
{
    public String? id { get; set; }
    public String? given_name { get; set; }
    public String? picture { get; set; }
}