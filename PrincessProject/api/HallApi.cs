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

        var content = await client.PostAsync(builder.ToString(), null);

        Console.WriteLine(content);
    }

    public static async Task<VisitingContender> NextContender(int attemptId)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.HallApiBase}/{attemptId}/next");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        builder.Query = query.ToString();

        var content = await client.PostAsync(builder.ToString(), null);

        Console.WriteLine(content);

        return new VisitingContender("name", "surname");

        /*POST hall/[номер попытки]/next?session=[sessionId]
        возвращаем следующего претендента для заданной попытки
        ответ:
        {
            name: "Иван Иванович" ,
        }*/
    }

    public static async Task<int> SelectContender(int attemptId)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.HallApiBase}/{attemptId}/select");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        builder.Query = query.ToString();

        var content = await client.PostAsync(builder.ToString(), null);

        Console.WriteLine(content);

        return 0;

        /*POST hall/[номер попытки]/select?session=[sessionId]
        возвращаем уровень выбранного претендента
        {
            rank: 67
        }*/
    }

    public static async Task<VisitingContender> CompareContenders(int attemptId, VisitingContender first,
        VisitingContender second)
    {
        using var client = new HttpClient();
        var builder = new UriBuilder($"{Constants.FriendApiBase}/{attemptId}/compare");

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["session"] = Constants.SessionId.ToString();
        builder.Query = query.ToString();

        var content = await client.PostAsync(builder.ToString(), null);

        Console.WriteLine(content);

        return new VisitingContender("name", "surname");

        /*POST freind/[номер попытки]/compare?session=[sessionId]
        {
            name: "Иван Иванович",
            name: "Никита Данилович"
        }
        просим подругу сравнить двух претендентов
        ответ:
        {
            "Никита Данилович"
        }
        либо ошибку в случае если претенденты не знакомы с принцессой.*/
    }
}