namespace MangaReader.Services
{
    internal class FavoritesService
    {
        static public async void AddFavorite(string mangaId)
        {
            await HttpService.Post("favorites", new { mangaId });
        }
    }
}
