using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ImageSample02
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
            // 画像を開く
            var dialog = new OpenFileDialog();
            dialog.Filter = "画像|*.jpg;*.jpeg;*.png;*.bmp";
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            // ファイルをメモリにコピー
            var ms = new MemoryStream();
            using (var s = new FileStream(dialog.FileName, FileMode.Open))
            {
                s.CopyTo(ms);
            }
            // ストリームの位置をリセット
            ms.Seek(0, SeekOrigin.Begin);
            // ストリームをもとにBitmapImageを作成
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.EndInit();
            // BitmapImageをSourceに指定して画面に表示する
            this.image.Source = bmp;
        }
    }
}
