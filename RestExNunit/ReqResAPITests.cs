using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNunit
{
    [TestFixture]
    internal class ReqResAPITests
    {
        private RestClient client;
        private string baseUrl = "https://reqres.in/api/";
        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test, Order(0)]
        public void GetSingleUser()
        {
            var req = new RestRequest("users/2", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userdata = JsonConvert.DeserializeObject<UserDataResponse>(res.Content);
            UserData? user = userdata?.Data;

            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Email);
        }
        [Test, Order(1)]
        public void CreateUser()
        {
            var req = new RestRequest("users", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "John Doe", job = "Software Engineer" });
            var res = client.Execute(req);

            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var user = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(user);
          //  Assert.IsNotEmpty(user.Email);
        }
        [Test, Order(2)]
        public void UpdateUser()
        {
            var req = new RestRequest("users/2", Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "Updated John Doe", job = "Senior Software Engineer" });
            var res = client.Execute(req);

            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(user);
        }
        [Test, Order(3)]
        public void DeleteUser()
        {
            var req = new RestRequest("users/2", Method.Delete);
            var res = client.Execute(req);

            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));

        }
        [Test, Order(4)]
        public void GetNonExistingUser()
        {
            var req = new RestRequest("users/999", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));         
        }
    }
}
