using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConentModelSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // オブジェクトを作成
            var anthem = new Animal
            {
                Name = "アンセム",
                Age = 9,
                Picture = new BitmapImage(new Uri("/anthem.png", UriKind.Relative))
            };

            // ボタンに設定
            this.buttonObject.Content = anthem;
        }
    }
}
