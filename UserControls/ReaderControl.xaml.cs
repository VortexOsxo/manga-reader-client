using MangaReader.Services;
using MangaReader.Services.Reader;
using MangaReader.Services.ReaderServices;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MangaReader.UserControls
{
    public partial class ReaderControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PagesService pagesService;
        private ReaderService readerService;
        private ScrollerService scrollerService;

        public string CurrentPage { get { return pagesService.CurrentPage; } }
        public string NextPage { get { return pagesService.NextPage; } }

        public int CurrentPageTop { get { return (int) (scrollerService.scrollPercentage * CurrentImage.ActualHeight); } }

        public int NextPageTop { get { return CurrentPageTop + (int) CurrentImage.ActualHeight; } }

        public double CanvasLeft
        {
            get { return (SystemParameters.PrimaryScreenWidth - 800) / 2; } // TODO: remove hardcoded value (800 comes from the image size in xaml and 1920 from the user screen width)
        }

        public ReaderControl()
        {
            readerService = ReaderService.Instance;
            pagesService = readerService.pagesService;
            scrollerService = readerService.scrollerService;

            pagesService.PagesChanged += OnPagesChanged;

            InitializeComponent();
            DataContext = this;
        }

        private void Control_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            scrollerService.Scroll(e.Delta / CurrentImage.ActualHeight);

            OnPropertyChanged(nameof(CurrentPageTop));
            OnPropertyChanged(nameof(NextPageTop));
        }

        private void OnPagesChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(NextPage));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
