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

namespace FileDialogSample01
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

        private void FileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "ファイルを開く";
            dialog.Filter = "全てのファイル(*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                this.textBlockFileName.Text = dialog.FileName;
            }
            else
            {
                this.textBlockFileName.Text = "キャンセルされました";
            }
        }

        private void FileSaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = "ファイルを保存";
            dialog.Filter = "テキストファイル|*.txt";
            if (dialog.ShowDialog() == true)
            {
                this.textBlockFileName.Text = dialog.FileName;
            }
            else
            {
                this.textBlockFileName.Text = "キャンセルされました";
            }
        }
    }
}
