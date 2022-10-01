using dotenv.net;
using Newtonsoft.Json;

public class GithubAuth
{
    private String githubClientID;
    private String githubClientSecret;
    public GithubAuth()
    {
        DotEnv.Load();
        githubClientID = Environment.GetEnvironmentVariable("GITHUB_CLIENT_ID")!;
        githubClientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENT_SECRET")!;
    }


    public async Task<String> userLogin(String code)
    {
        HttpClient accessTokenClient = new HttpClient();

        var values = new Dictionary<string, string>
        {
            { "client_id", githubClientID },
            { "client_secret", githubClientSecret },
            { "code", code }
        };
        var content = new FormUrlEncodedContent(values);
        var response = await accessTokenClient.PostAsync("https://github.com/login/oauth/access_token", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var queryValues = responseString.Split('&').Select(q => q.Split('='))
                           .ToDictionary(k => k[0], v => v[1]);
        String access_token = queryValues["access_token"];

        await getUserDataFromAT(access_token);

        return access_token;
    }

    public async Task<User> getUserDataFromAT(String access_token)
    {
        UserHandler userHandler = new UserHandler();
        HttpClient userDataClient = new HttpClient();

        userDataClient.BaseAddress = new Uri("https://api.github.com");
        userDataClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
        userDataClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        userDataClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", access_token);

        var userDataResponse = await userDataClient.GetAsync("/user");
        var userDataString = await userDataResponse.Content.ReadAsStringAsync();

        Console.WriteLine(userDataString);
        dynamic json = JsonConvert.DeserializeObject<object>(userDataString)!;

        userHandler.saveUser(json["login"].ToString(), json["id"].ToString(), json["avatar_url"].ToString());

        return new User(json["login"].ToString(), json["id"].ToString());
    }
}