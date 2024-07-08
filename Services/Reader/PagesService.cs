using MangaReader.Model;

namespace MangaReader.Services.ReaderServices
{
    public class PagesService
    {
        public string CurrentPage
        {
            get { return mangaPreview != null ? MangasService.GetPageUrl(mangaPreview.Id, chapterIndex, pageIndex) : ""; }
        }

        public string NextPage
        {
            get { return mangaPreview != null ? MangasService.GetPageUrl(mangaPreview.Id, chapterIndex, pageIndex + 1): ""; }
        }

        public int ChapterIndex
        {
            get { return chapterIndex; }
        }

        public int PageIndex
        {
            get { return pageIndex; }
        }

        public event EventHandler? PagesChanged;
        public event EventHandler? ChapterChanged;

        private List<Chapter>? chapters;

        private MangaPreview? mangaPreview;

        private int pageIndex = 0;
        private int chapterIndex = 0;

        public void SetManga(MangaPreview manga)
        {
            mangaPreview = manga;

            MangasService.GetChapters(mangaPreview.Id).ContinueWith((precedent) =>
            {
                chapters = precedent.Result;
                ChapterChanged?.Invoke(this, EventArgs.Empty);
                PagesChanged?.Invoke(this, EventArgs.Empty);
            });
        }

        public void GoToNextPage()
        {
            GoToPage(pageIndex + 1);
        }

        public void GoToPreviousPage()
        {
            GoToPage(pageIndex - 1);
        }

        public void GoToPage(int pageId)
        {
            pageIndex = pageId;

            if (pageIndex >= GetPagesCount())
                GoToChapter(chapterIndex + 1);
            
            if (pageIndex < 0)
                GoToChapter(chapterIndex - 1);

            PagesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void GoToChapter(int chapterId)
        {
            chapterIndex = chapterId;
            GoToPage(0);

            ChapterChanged?.Invoke(this, EventArgs.Empty);
        }

        private int GetPagesCount()
        {
            if (chapters == null) return int.MaxValue;
            return (chapters.Count >= chapterIndex) ? int.MaxValue : int.Parse(chapters[chapterIndex].Pages);
        }
    }
}
