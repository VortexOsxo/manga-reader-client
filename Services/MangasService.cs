using MangaReader.Config;
using MangaReader.Model;
using MangaReader.Services.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MangaReader.Services
{
    internal class MangasService
    {
        static public async Task<List<MangaPreview>> GetPreviews()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{Server.url}/mangas");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<MangaPreview> previews = JsonConvert.DeserializeObject<List<MangaPreview>>(jsonResponse)!;
                    return previews;
                }
                else
                {
                    throw new HttpRequestException($"Error fetching data: {response.StatusCode}");
                }
            }
        }

        static public async Task<List<Chapter>> GetChapters(string mangaId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{Server.url}/mangas/{mangaId}/chapters");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Chapter> chapters = JsonConvert.DeserializeObject<List<Chapter>>(jsonResponse)!;
                    return chapters;
                }
                else
                {
                    throw new HttpRequestException($"Error fetching data: {response.StatusCode}");
                }
            }
        }

        static public string GetPageUrl(string mangaId, int chapterId, int pageId)
        {
            return $"{Server.url}/mangas/{mangaId}/{chapterId}/{pageId}";
        }

        static public async void AddFavorite(string mangaId)
        {
            string json = JsonConvert.SerializeObject(new { mangaId });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Session-Id", LoginService.sessionId);
                HttpResponseMessage response = await client.PostAsync($"{Server.url}/favorites", content);
            }
        }
    }
}
