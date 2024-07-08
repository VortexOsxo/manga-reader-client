using MangaReader.Model;
using MangaReader.Services;
using MangaReader.UserControls;
using System.Windows.Controls;
using System.Windows;

namespace MangaReader.Pages
{
    public partial class SelectionPage : Page
    {
        public SelectionPage()
        {
            InitializeComponent();
            DataContext = this;

            MangasService.GetPreviews().ContinueWith((precedent) => {
                LoadPreviews(precedent.Result);
            });
        }

        private void LoadPreviews(List<MangaPreview> previews)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var preview in previews)
                {
                    var mangaPreviewControl = new MangaPreviewControl(preview);
                    PreviewsPanel.Children.Add(mangaPreviewControl);
                }
            });
        }
    }
}
