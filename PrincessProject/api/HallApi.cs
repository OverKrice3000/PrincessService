using System.Text;
using System.Text.Json.Nodes;
using System.Web;
using PrincessProject.Data.model;
using PrincessProject.utils;

namespace PrincessProject.api;

public static class HallApi
{
    public static async Task ResetHall(int attemptId)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.HallApiBase}/{attemptId}/reset");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        builder.Query = query.ToString();

        await client.PostAsync(builder.ToString(), null);
    }

    public static async Task NextContender(int attemptId)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.HallApiBase}/{attemptId}/next");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        builder.Query = query.ToString();

        await client.PostAsync(builder.ToString(), null);
    }

    public static async Task<int> SelectContender(int attemptId)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.HallApiBase}/{attemptId}/select");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        builder.Query = query.ToString();

        var content = await client.PostAsync(builder.ToString(), null);
        var json = JsonNode.Parse(await content.Content.ReadAsStringAsync());

        if (json?["rank"] is null)
        {
            throw new ApplicationException("Bad http response");
        }

        return int.Parse(json!["rank"]!.ToString());
    }

    public static async Task<VisitingContender> CompareContenders(int attemptId, VisitingContender first,
        VisitingContender second)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.FriendApiBase}/{attemptId}/compare");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        query["first"] = first.FullName;
        query["second"] = second.FullName;
        builder.Query = query.ToString();

        var jsonRaw = new JsonObject()
        {
            ["first"] = first.FullName,
            ["second"] = second.FullName
        }.ToString();
        var content = await client.PostAsync(builder.ToString(),
            new StringContent(jsonRaw, Encoding.UTF8, "application/json"));

        var json = JsonNode.Parse(await content.Content.ReadAsStringAsync());

        if (json?["name"] is null)
        {
            throw new ApplicationException("Bad http response");
        }

        return Util.VisitingContenderFromFullName(json!["name"]!.ToString());
    }
}