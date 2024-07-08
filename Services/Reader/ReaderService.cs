using MangaReader.Model;
using MangaReader.Services.Reader;
using MangaReader.Services.ReaderServices;

namespace MangaReader.Services
{
    internal class ReaderService
    {
        public MangaPreview? manga;

        public PagesService pagesService;
        public ScrollerService scrollerService;

        public static ReaderService Instance { get { return instance; } }
        private static ReaderService instance = new ReaderService();
    
        private ReaderService()
        {
            pagesService = new PagesService();
            scrollerService = new ScrollerService(pagesService);
        }
    
        public void SetManga(MangaPreview manga)
        {
            this.manga = manga;
            pagesService.SetManga(manga);
            scrollerService.ResetScroll();
        }
    }
}
