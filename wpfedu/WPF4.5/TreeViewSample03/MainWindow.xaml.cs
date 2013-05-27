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

namespace TreeViewSample03
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.treeView.ItemsSource = new List<Person>
            {
                new Person
                {
                    Name = "田中　太郎",
                    Children = new List<Person>
                    {
                        new Person { Name = "田中　花子" },
                        new Person { Name = "田中　一郎" },
                        new Person
                        {
                            Name = "木村　貫太郎",
                            Children = new List<Person>
                            {
                                new Person { Name = "木村　はな" },
                                new Person { Name = "木村　梅" },
                            }
                        }
                    }
                },
                new Person
                {
                    Name = "田中　次郎",
                    Children = new List<Person>
                    {
                        new Person { Name = "田中　三郎" }
                    }
                }
            };
        }
    }
}
