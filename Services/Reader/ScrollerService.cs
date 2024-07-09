using MangaReader.Services.ReaderServices;

namespace MangaReader.Services.Reader
{
    internal class ScrollerService
    {
        public double scrollPercentage = 0;

        private PagesService pagesService;

        public ScrollerService(PagesService pagesService)
        { 
            this.pagesService = pagesService;
            pagesService.ChapterChanged += OnChapterChanged;
        }

        public void Scroll(double scroll)
        {
            scrollPercentage += scroll;

            if (!pagesService.CanGoDown() && scrollPercentage > 0)
                scrollPercentage = 0;

            if (scrollPercentage <= -1) {
                scrollPercentage += 1;
                pagesService.GoToNextPage();
            }

            else if (scrollPercentage > 0)
            {
                scrollPercentage -= 1;
                pagesService.GoToPreviousPage();
            }
        }

        public void ResetScroll()
        {
            scrollPercentage = 0;
        }

        private void OnChapterChanged(object sender, EventArgs e)
        {
            ResetScroll();
        }
    }
}
