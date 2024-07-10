using MangaReader.Config;
using MangaReader.Model;
using MangaReader.Services.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MangaReader.Services
{
    internal class HistoryService
    {
        static public async void AddToHistory(string mangaId, int chapterNumber, int pageNumber)
        {
            string json = JsonConvert.SerializeObject(new { mangaId, chapterNumber, pageNumber });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Session-Id", LoginService.sessionId);
                HttpResponseMessage response = await client.PostAsync($"{Server.url}/histories", content);
            }
        }

        static public async Task<List<MangaPreview>> GetHistory()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Session-Id", LoginService.sessionId);
                HttpResponseMessage response = await client.GetAsync($"{Server.url}/histories");

                if (!response.IsSuccessStatusCode) return [];

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MangaPreview>>(jsonResponse)!;
            }
        }

        static public async Task<Bookmark> GetLastReadPage(string mangaId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Session-Id", LoginService.sessionId);
                HttpResponseMessage response = await client.GetAsync($"{Server.url}/histories/{mangaId}");

                if (!response.IsSuccessStatusCode) return new Bookmark(0,0);

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Bookmark>(jsonResponse)!;
            }
        }
    }
}
