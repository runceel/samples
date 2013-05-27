namespace ScrollViewerSample03
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScrollToHalfVerticalOffsetButton_Click(object sender, RoutedEventArgs e)
        {
            // 垂直スクロールバーの位置を真ん中に設定
            this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.ScrollableHeight / 2);
        }
    }
}
