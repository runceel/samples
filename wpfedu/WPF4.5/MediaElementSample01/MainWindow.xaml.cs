using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaElementSample01
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 動画を開く
            var dialog = new OpenFileDialog();
            dialog.Filter = "動画|*.mp4";
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            // SourceにURIを指定して再生する。
            // LoadedBehaviorがPlay(デフォルト値)なので自動再生される。
            var uri = new Uri(dialog.FileName);
            this.mediaElement.Source = uri;
        }
    }
}
