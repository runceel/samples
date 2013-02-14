using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataGridSample02
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 適当なデータ100件生成する
            var data = new ObservableCollection<Person>(
                Enumerable.Range(1, 100).Select(i => new Person
                {
                    Name = "田中　太郎" + i,
                    Gender = i % 2 == 0 ? Gender.Men : Gender.Women,
                    Age = 20 + i % 50,
                    AuthMember = i % 5 == 0
                }));
            // CollectionViewSourceにデータを設定する
            var source = (CollectionViewSource)this.Resources["dataGridSource"];
            source.Source = data;
        }
    }
}
