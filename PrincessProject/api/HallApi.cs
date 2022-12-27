using System.Text;
using System.Text.Json.Nodes;
using System.Web;
using Microsoft.Extensions.Configuration;
using PrincessProject.Data.model;
using PrincessProject.utils;

namespace PrincessProject.api;

public static class HallApi
{
    private static readonly HttpClient Client = new HttpClient();
    private static string WebAppApiBase;
    private static string HallApiBase;
    private static string FriendApiBase;
    private static string SessionId;

    static HallApi()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        WebAppApiBase = config.GetSection("PrincessConfig")["HallApiBase"];
        SessionId = config.GetSection("PrincessConfig")["SessionId"];
        HallApiBase = WebAppApiBase + "/hall";
        FriendApiBase = WebAppApiBase + "/freind";
    }

    public static async Task ResetHall()
    {
        var builder = new UriBuilder($"{HallApiBase}/reset");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["sessionId"] = SessionId;
        builder.Query = query.ToString();

        var kek = await Client.PostAsync(builder.ToString(), null);
    }

    public static async Task NextContender(int attemptId)
    {
        var builder = new UriBuilder($"{HallApiBase}/{attemptId}/next");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["sessionId"] = SessionId;
        builder.Query = query.ToString();

        var kek = await Client.PostAsync(builder.ToString(), null);
    }

    public static async Task<int> SelectContender(int attemptId)
    {
        var builder = new UriBuilder($"{HallApiBase}/{attemptId}/select");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["sessionId"] = SessionId;
        builder.Query = query.ToString();

        var content = await Client.PostAsync(builder.ToString(), null);

        var rank = int.Parse(await content.Content.ReadAsStringAsync());

        return rank;
    }

    public static async Task<VisitingContender> CompareContenders(int attemptId, VisitingContender first,
        VisitingContender second)
    {
        var builder = new UriBuilder($"{FriendApiBase}/{attemptId}/compare");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["sessionId"] = SessionId;
        builder.Query = query.ToString();

        var jsonRaw = new JsonObject()
        {
            ["name1"] = first.FullName,
            ["name2"] = second.FullName
        }.ToString();
        var content = await Client.PostAsync(builder.ToString(),
            new StringContent(jsonRaw, Encoding.UTF8, "application/json"));

        var compared = await content.Content.ReadAsStringAsync();

        return Util.VisitingContenderFromFullName(compared);
    }
}