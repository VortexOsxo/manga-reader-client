using MangaReader.Model;
using MangaReader.UserControls;
using System.Windows.Controls;

namespace MangaReader.Pages
{
    public partial class MangaInfoPage : Page
    {
        
        private MangaPreview manga;

        public MangaInfoPage(MangaPreview manga)
        {
            InitializeComponent();
            DataContext = this;
            this.manga = manga;

            CreateControls();
        }

        private void CreateControls()
        {
            var preview = new MangaPreviewControl(manga);
            PreviewGrid.Children.Add(preview);

            var chapters = new ChapterSelectionControl(manga);
            ChaptersGrid.Children.Add(chapters);
        }
    }
}
