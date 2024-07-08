using MangaReader.Model;

namespace MangaReader.Services.ReaderServices
{
    public class PagesService
    {
        public string CurrentPage
        {
            get { return MangasService.GetPageUrl(mangaPreview.Id, chapterIndex, pageIndex); }
        }

        public string NextPage
        {
            get { return MangasService.GetPageUrl(mangaPreview.Id, chapterIndex, pageIndex + 1); }
        }

        public int ChapterIndex
        {
            get { return chapterIndex; }
        }

        public int PageIndex
        {
            get { return pageIndex; }
        }

        public event EventHandler PagesChanged;

        public PagesService() {}

        public void SetManga(MangaPreview manga)
        {
            mangaPreview = manga;

            MangasService.GetChapters(mangaPreview.Id).ContinueWith((precedent) =>
            {
                chapters = precedent.Result;
                OnPagesChanged();
            });
        }

        public void GoToNextPage()
        {
            pageIndex += 1;
            if (pageIndex >= int.Parse(chapters[chapterIndex].Pages))
            {
                pageIndex = 0;
                chapterIndex += 1;
            }
            OnPagesChanged();
        }

        public void GoToChapter(int chapterId)
        {
            chapterIndex = chapterId;
            pageIndex = 0;

            OnPagesChanged();
        }

        public void GoToPage(int pageId)
        {
            pageIndex = pageId;

            OnPagesChanged();
        }

        protected virtual void OnPagesChanged()
        {
            PagesChanged?.Invoke(this, EventArgs.Empty);
        }

        private List<Chapter> chapters;

        private MangaPreview mangaPreview;

        private int pageIndex = 0;
        private int chapterIndex = 0;

    }
}
