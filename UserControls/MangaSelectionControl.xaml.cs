using MangaReader.Model;
using System.Windows.Controls;

namespace MangaReader.UserControls
{
    public partial class MangaSelectionControl : UserControl
    {
        public MangaSelectionControl(Task<List<MangaPreview>> mangas)
        {
            InitializeComponent();
            DataContext = this;

            mangas.ContinueWith((precedent) => {
                Dispatcher.Invoke(() => LoadPreviews(precedent.Result));
            });
        }

        private void LoadPreviews(List<MangaPreview> previews)
        {
            foreach (var preview in previews)
            {
                var mangaPreviewControl = new MangaPreviewControl(preview);
                PreviewsPanel.Children.Add(mangaPreviewControl);
            }
        }
    }
}
