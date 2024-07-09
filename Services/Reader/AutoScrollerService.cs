using System.Windows.Threading;

namespace MangaReader.Services.Reader
{
    internal class AutoScrollerService
    {
        private ScrollerService scrollerService;

        private DispatcherTimer autoScrollTimer;

        public AutoScrollerService(ScrollerService scrollerService)
        {
            this.scrollerService = scrollerService;

            autoScrollTimer = new DispatcherTimer();
            autoScrollTimer.Interval = TimeSpan.FromSeconds(0.005);
            autoScrollTimer.Tick += AutoScroll;
        }

        public void StartAutoScroll()
        {
            autoScrollTimer.Start();
        }
        
        public void StopAutoScroll()
        {
            autoScrollTimer.Stop();   
        }

        private void AutoScroll(object? sender, EventArgs e)
        {
            scrollerService.Scroll(-0.0005);
        }
    }
}
