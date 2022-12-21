using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;
using Microsoft.Extensions.Configuration;
using PrincessProject.Data.model;
using PrincessProject.Data.model.api;
using PrincessProject.utils;

namespace PrincessProject.api;

public static class HallApi
{
    private static readonly HttpClient Client = new HttpClient();
    private static string WebAppApiBase;
    private static string HallApiBase;
    private static string FriendApiBase;
    private static int SessionId;

    static HallApi()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        WebAppApiBase = config.GetSection("PrincessConfig")["HallApiBase"];
        SessionId = int.Parse(config.GetSection("PrincessConfig")["SessionId"]);
        HallApiBase = WebAppApiBase + "/hall";
        FriendApiBase = WebAppApiBase + "/friend";
    }

    public static async Task ResetHall(int attemptId)
    {
        var builder = new UriBuilder($"{HallApiBase}/{attemptId}/reset");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = SessionId.ToString();
        builder.Query = query.ToString();

        await Client.PostAsync(builder.ToString(), null);
    }

    public static async Task NextContender(int attemptId)
    {
        var builder = new UriBuilder($"{HallApiBase}/{attemptId}/next");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = SessionId.ToString();
        builder.Query = query.ToString();

        await Client.PostAsync(builder.ToString(), null);
    }

    public static async Task<int> SelectContender(int attemptId)
    {
        var builder = new UriBuilder($"{HallApiBase}/{attemptId}/select");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = SessionId.ToString();
        builder.Query = query.ToString();

        var content = await Client.PostAsync(builder.ToString(), null);
        var json = JsonNode.Parse(await content.Content.ReadAsStringAsync())
            .Deserialize<SelectContenderResponsePayload>();

        if (json?.rank is null)
        {
            throw new ApplicationException("Bad http response");
        }

        return int.Parse(json.rank);
    }

    public static async Task<VisitingContender> CompareContenders(int attemptId, VisitingContender first,
        VisitingContender second)
    {
        var builder = new UriBuilder($"{FriendApiBase}/{attemptId}/compare");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = SessionId.ToString();
        builder.Query = query.ToString();

        var jsonRaw = new JsonObject()
        {
            ["first"] = first.FullName,
            ["second"] = second.FullName
        }.ToString();
        var content = await Client.PostAsync(builder.ToString(),
            new StringContent(jsonRaw, Encoding.UTF8, "application/json"));

        var json = JsonNode.Parse(await content.Content.ReadAsStringAsync())
            .Deserialize<CompareContendersResponsePayload>();

        if (json?.name is null)
        {
            throw new ApplicationException("Bad http response");
        }

        return Util.VisitingContenderFromFullName(json.name);
    }
}