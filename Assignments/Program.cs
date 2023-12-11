using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

GetAllUsers(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);
GetSingleUser(client);
static void GetAllUsers(RestClient client)
{
    var getUserReq = new RestRequest("posts", Method.Get);
    var getUserRes = client.Execute(getUserReq);
    Console.WriteLine("Get Response: \n" + getUserRes.Content);
}

static void CreateUser(RestClient client)
{
    var createUserReq = new RestRequest("posts", Method.Post);
    createUserReq.AddHeader("Content-Type", "application/json");//Adding header
    createUserReq.AddJsonBody(new { id = "1", title = "Hi There!!" });

    var createUserRes = client.Execute(createUserReq);
    Console.WriteLine("POST Create User Response:");
    Console.WriteLine(createUserRes.Content);
}
static void UpdateUser(RestClient client)
{
    var updateUserReq = new RestRequest("posts/2", Method.Put);
    updateUserReq.AddHeader("Content-Type", "application/json");//Adding header
    updateUserReq.AddJsonBody(new { name = "Updated 2", job = "Yeah Its me!!" });

    var updateUserRes = client.Execute(updateUserReq);
    Console.WriteLine("PUT Update User Response:");
    Console.WriteLine(updateUserRes.Content);
}
static void DeleteUser(RestClient client)
{
    var deleteUserReq = new RestRequest("posts/2", Method.Delete);

    var deleteUserRes = client.Execute(deleteUserReq);
    Console.WriteLine("DELETE User Response:");
    Console.WriteLine(deleteUserRes.Content);
}
static void GetSingleUser(RestClient client)
{
    var getUserReq = new RestRequest("posts/2", Method.Get);
    var getUserRes = client.Execute(getUserReq);
    if (getUserRes.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse JSON response content
        JObject? userJson = JObject.Parse(getUserRes.Content);
        string? userid = userJson["id"].ToString();
        string? usertitle = userJson["title"].ToString();
        Console.WriteLine($"User Details: {userid} {usertitle}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserRes.ErrorMessage}");
    }
}



