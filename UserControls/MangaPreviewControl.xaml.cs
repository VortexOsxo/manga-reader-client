using MangaReader.Model;
using MangaReader.Pages;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MangaReader.UserControls
{
    public partial class MangaPreviewControl : UserControl
    {
        public MangaPreviewControl(MangaPreview manga)
        {
            this.DataContext = manga;
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not MangaPreview mangaPreview) return;

            NavigationService.GetNavigationService(this).Navigate(new MangaInfoPage(mangaPreview));
        }
    }
}
