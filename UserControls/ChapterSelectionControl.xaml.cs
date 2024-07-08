using MangaReader.Model;
using MangaReader.Pages;
using MangaReader.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MangaReader.UserControls
{
    public partial class ChapterSelectionControl : UserControl
    {
        private MangaPreview manga;

        public ChapterSelectionControl(MangaPreview manga)
        {
            InitializeComponent();
            this.manga = manga;

            CreateChapters();
        }

        private void CreateChapters()
        {
            for (int i = 1; i <= manga.AvailableChapters; i++)
            {
                TextBlock chapterTextBlock = new TextBlock
                {
                    Text = $"Chapter {i}",
                    Margin = new Thickness(5),
                    Tag = i-1,
                };

                chapterTextBlock.MouseLeftButtonDown += ChapterTextBlock_MouseLeftButtonDown;
                ChaptersPanel.Children.Add(chapterTextBlock);

                if (i == manga.AvailableChapters) continue;
            }
        }

        private void ChapterTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock clickedTextBlock = sender as TextBlock;
            if (clickedTextBlock == null) return;

            int chapterNumber = (int)clickedTextBlock.Tag;
            GoToChapter(chapterNumber);
        }

        private void FirstChapterTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GoToChapter(0);
        }

        private void LastChapterTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GoToChapter(manga.AvailableChapters - 1);
        }

        private void GoToChapter(int chapterId)
        {
            var reader = ReaderService.Instance;

            reader.SetManga(this.manga);
            reader.pageServices.GoToChapter(chapterId);

            NavigationService.GetNavigationService(this).Navigate(new ReadPage());
        }
    }
}
