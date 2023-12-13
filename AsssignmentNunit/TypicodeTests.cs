using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNunit
{
    [TestFixture]
    internal class TypicodeTests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test, Order(0)]
        public void GetSingleUser()
        {
            var req = new RestRequest("posts/2", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(res.Content);

            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Body);
            Assert.IsNotEmpty(user.Title);
        }
        [Test, Order(1)]
        public void CreateUser()
        {
            var req = new RestRequest("posts", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { id = "808", title = "Hlo There!!" });
            var res = client.Execute(req);

            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var user = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(user);
        }
        [Test, Order(2)]
        public void UpdateUser()
        {
            var req = new RestRequest("posts/2", Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { id = "303", title = "Its me Kavi!!" });
            var res = client.Execute(req);

            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(res.Content);
            Assert.NotNull(user);
        }
        [Test, Order(3)]
        public void DeleteUser()
        {
            var req = new RestRequest("posts/2", Method.Delete);
            var res = client.Execute(req);

            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        [Test, Order(4)]
        public void GetAllUser()
        {
            var req = new RestRequest("posts", Method.Get);
            var res = client.Execute(req);


            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
    }
}
