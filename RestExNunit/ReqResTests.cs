using Newtonsoft.Json;
using RestExNunit.Utilities;
using RestSharp;
using Serilog;

namespace RestExNunit
{
    [TestFixture]
    public class ReqResTests: CoreCodes
    {      
     
        [Test, Order(1)]
        public void GetSingleUser()
        {
            test = extent.CreateTest("Get single user");
            Log.Information("Getsingleuser Test started");

            var req = new RestRequest("users/2", Method.Get);
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");

                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(res.Content);
                UserData? user = userdata?.Data;

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(2));
                Log.Information("User id matches with fetch");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Get single user test passed all asserts");

                test.Pass("Getsingleuser test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Getsingleuser test passed all asserts");
            }
        }
        [Test, Order(2)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Createuser Test started");

            var req = new RestRequest("users", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "John Doe", job = "Software Engineer" });
            var res = client.Execute(req);
            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API response: {res.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(res.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");

                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Create user test passed all asserts");

                test.Pass("Create user test passed all asserts");
            }
            catch(AssertionException)
            {
                test.Fail("Create user test failed");
            }
        }
        [Test, Order(3)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update user");
            Log.Information("Updateuser Test started");

            var req = new RestRequest("users/2", Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "Updated John Doe", job = "Senior Software Engineer" });
            var res = client.Execute(req);
            try { 
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {res.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(res.Content);
                Assert.NotNull(user);
                Log.Information("Updated User and returned");
                Log.Information("Updated user test passed all asserts");

                test.Pass("Updated user test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Updated user test failed");
            }
        }
        [Test, Order(4)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete user");
            Log.Information("Deleteuser Test started");

            var req = new RestRequest("users/2", Method.Delete);
            var res = client.Execute(req);
            try
            { 
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
            Log.Information("User Deleted");
            Log.Information("Deleted user test passed all asserts");

            test.Pass("Deleted user test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Deleted user test failed");
            }
        }
        [Test, Order(5)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("NonExisting user");
            Log.Information("NonExistinguser Test started");

            var req = new RestRequest("users/999", Method.Get);
            var res = client.Execute(req);

            try 
            { 
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("NonExisting user test passed all asserts");

                test.Pass("NonExisting user test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("NonExisting user test failed");
            }
        }
    }
}
