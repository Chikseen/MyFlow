using dotenv.net;
using Newtonsoft.Json;

public class GoogleAuth
{
    private String googleClientID;
    private String googleClientSecret;
    private String googleredirect;
    public GoogleAuth()
    {
        DotEnv.Load();
        googleClientID = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")!;
        googleClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")!;
        googleredirect = Environment.GetEnvironmentVariable("GOOGLE_REDIRECT")!;
    }


    public async Task<String> userLogin(String code)
    {
        HttpClient accessTokenClient = new HttpClient();
        var values = new Dictionary<string, string>
        {
            { "client_id", googleClientID },
            { "client_secret", googleClientSecret },
            { "code", code },
            { "grant_type", "authorization_code" },
            { "redirect_uri", googleredirect }
        };
        Console.WriteLine("code: " + code);
        var content = new FormUrlEncodedContent(values);
        var response = await accessTokenClient.PostAsync("https://oauth2.googleapis.com/token", content);
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("responseString: " + responseString);


        GoogleAT token = JsonConvert.DeserializeObject<GoogleAT>(responseString)!;
        if (token.access_token != "")
        {
            await getUserDataFromAT(token.access_token!);
            return token.access_token!;
        }
        else return "401";
    }

    public async Task<User> getUserDataFromAT(String access_token)
    {
        Console.WriteLine("access_token: " + access_token);
        UserHandler userHandler = new UserHandler();
        HttpClient userDataClient = new HttpClient();
        Console.WriteLine($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={access_token}");
        var userDataResponse = await userDataClient.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={access_token}");
        var userDataString = await userDataResponse.Content.ReadAsStringAsync();
        Console.WriteLine("userDataString: " + userDataString);
        GoogleData token = JsonConvert.DeserializeObject<GoogleData>(userDataString)!;

        userHandler.saveUser(token.given_name!, token.id!, token.picture!);
        return new User(token.given_name!, token.id!);
    }
}
