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
        public AutoScrollerService autoScrollerService;

        public static ReaderService Instance { get { return instance; } }

        public int mangaWidth { get; private set; }

        private static ReaderService instance = new ReaderService();
    
        private ReaderService()
        {
            mangaWidth = 800;
            pagesService = new PagesService();
            scrollerService = new ScrollerService(pagesService);
            autoScrollerService = new AutoScrollerService(scrollerService);
        }
    
        public void SetManga(MangaPreview manga)
        {
            Reset();
            this.manga = manga;

            pagesService.SetManga(manga);
        }

        public void Reset()
        {
            manga = null;

            autoScrollerService.StopAutoScroll();
            scrollerService.ResetScroll();
        }
    }
}
