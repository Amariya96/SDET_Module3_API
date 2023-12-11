using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);

/*var getUserReq = new RestRequest("users/2", Method.Get);
var getUserRes = client.Execute(getUserReq);
Console.WriteLine("Get Response: \n" + getUserRes.Content);

var createUserReq = new RestRequest("users", Method.Post);
createUserReq.AddParameter("name", "John Doe");
createUserReq.AddParameter("job", "Software Engineer");

var createUserRes = client.Execute(createUserReq);
Console.WriteLine("POST Create User Response:");
Console.WriteLine(createUserRes.Content);

var updateUserReq = new RestRequest("users/2", Method.Put);
updateUserReq.AddParameter("name", "Updated John Doe");
updateUserReq.AddParameter("job", "Senior Software Engineer");

var updateUserRes = client.Execute(updateUserReq);
Console.WriteLine("PUT Update User Response:");
Console.WriteLine(updateUserRes.Content);

var deleteUserReq = new RestRequest("users/2", Method.Delete);

var deleteUserRes = client.Execute(deleteUserReq);
Console.WriteLine("DELETE User Response:");
Console.WriteLine(deleteUserRes.Content);
*/
GetAllUsers(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);
GetSingleUser(client);
static void GetAllUsers(RestClient client) 
{ 
var getUserReq = new RestRequest("users", Method.Get);
getUserReq.AddQueryParameter("page", "1"); //Adding query parameter

var getUserRes = client.Execute(getUserReq);
Console.WriteLine("Get Response: \n" + getUserRes.Content);
}

static void CreateUser(RestClient client)
{
    var createUserReq = new RestRequest("users", Method.Post);
    createUserReq.AddHeader("Content-Type", "application/json");//Adding header
    createUserReq.AddJsonBody(new { name = "John Doe", job = "Software Engineer" });

    var createUserRes = client.Execute(createUserReq);
    Console.WriteLine("POST Create User Response:");
    Console.WriteLine(createUserRes.Content);
}
static void UpdateUser(RestClient client)
{
    var updateUserReq = new RestRequest("users/2", Method.Put);
    updateUserReq.AddHeader("Content-Type", "application/json");//Adding header
    updateUserReq.AddJsonBody(new { name = "Updated John Doe", job = "Senior Software Engineer" });

    var updateUserRes = client.Execute(updateUserReq);
    Console.WriteLine("PUT Update User Response:");
    Console.WriteLine(updateUserRes.Content);
}
static void DeleteUser(RestClient client)
{
    var deleteUserReq = new RestRequest("users/2", Method.Delete);

    var deleteUserRes = client.Execute(deleteUserReq);
    Console.WriteLine("DELETE User Response:");
    Console.WriteLine(deleteUserRes.Content); 
}
static void GetSingleUser(RestClient client)
{
    var getUserReq = new RestRequest("users/2", Method.Get);
    var getUserRes = client.Execute(getUserReq);
    if (getUserRes.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse JSON response content
        JObject? userJson = JObject.Parse(getUserRes.Content);
        string? userName = userJson["data"]["first_name"].ToString();
        string? userLastName = userJson["data"]["last_name"].ToString();
        Console.WriteLine($"User Name: {userName} {userLastName}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserRes.ErrorMessage}");
    }
}



