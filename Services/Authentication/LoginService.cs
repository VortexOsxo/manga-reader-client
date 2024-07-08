using MangaReader.Config;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MangaReader.Services.Authentication
{
    internal class LoginService
    {
        static public string sessionId;

        static public async void Login(string username, string password, Action<HttpStatusCode>? callback = null)
        {
            string json = JsonConvert.SerializeObject(new { username, password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"{Server.url}/login", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic responseData = JsonConvert.DeserializeObject(responseBody);
                    sessionId = responseData.session_id;
                }
                callback?.Invoke(response.StatusCode);
            }
        }
    }
}
