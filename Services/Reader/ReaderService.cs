using MangaReader.Model;
using MangaReader.Services.ReaderServices;

namespace MangaReader.Services
{
    internal class ReaderService
    {
        public MangaPreview? manga;
        public PagesService pageServices;

        public static ReaderService Instance { get { return instance; } }
        private static ReaderService instance = new ReaderService();
    
        private ReaderService()
        {
            pageServices = new PagesService();
        }
    
        public void SetManga(MangaPreview manga)
        {
            this.manga = manga;
            pageServices.SetManga(manga);
        }
    }
}
