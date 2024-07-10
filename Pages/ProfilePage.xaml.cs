using MangaReader.Services;
using MangaReader.UserControls;
using System.Windows.Controls;

namespace MangaReader.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            MangaSelectionGrid.Children.Add(new MangaSelectionControl(HistoryService.GetHistory()));
        }
    }
}
