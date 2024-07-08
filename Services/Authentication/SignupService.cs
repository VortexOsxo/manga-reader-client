using MangaReader.Config;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MangaReader.Services.Authentication
{
    internal class SignupService
    {
        static async public void Signup(string username, string password, Action<HttpStatusCode>? callback = null)
        {
            string json = JsonConvert.SerializeObject(new { username, password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"{Server.url}/signup", content);
                callback?.Invoke(response.StatusCode);
            }
        }
    }
}
