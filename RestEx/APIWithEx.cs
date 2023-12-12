using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestEx
{
    public class APIWithEx
    {
        string baseUrl = "https://reqres.in/api/";
        //checking for error msg
        /* public void GetSingleUser()
         {
             var client = new RestClient(baseUrl);
             var req = new RestRequest("users/5", Method.Get);
             var res = client.Execute(req);

             if(!res.IsSuccessful)
             {
                 try
                 {
                     var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>(res.Content);
                     if (errorDetails != null)
                     {
                         Console.WriteLine($"API Error: {errorDetails.Error}");
                     }
                 }
                 catch (JsonException)
                 {
                     Console.WriteLine("Failed to deserialize error reponse.");
                 }
             }
             else
             {
                 Console.WriteLine("Successful Response:");
                 Console.WriteLine(res.Content);
             }    
         }*/
        //Json  content check for null
        public void GetSingleUser()
        {
            var client = new RestClient(baseUrl);
            var req = new RestRequest("users/23", Method.Get);
            var res = client.Execute(req);

            if (!res.IsSuccessful)
            {
                if (IsJson(res.Content))
                {
                    try
                    {
                        var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>(res.Content);
                        if (errorDetails != null)
                        {
                            Console.WriteLine($"API Error: {errorDetails.Error}");
                        }
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine("Failed to deserialize error reponse.");
                    }
                }
                else
                {
                    Console.WriteLine($"Non-Json error response: {res.Content}");
                }
            }
            static bool IsJson(string content)
            {
                try
                {
                    JToken.Parse(content);
                    return true;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
        }
    }
}
