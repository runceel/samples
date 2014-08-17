using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace DispatcherObjectSample01
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッドからの普通の呼び出しなのでOK
            var d = new DrivedObject();
            d.DoSomething();
        }

        private async void NGButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッド以外からの呼び出しなので例外が出る
            var d = new DrivedObject();
            try
            {
                await Task.Run(() => d.DoSomething());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void DispatcherButton_Click(object sender, RoutedEventArgs e)
        {
            // UIスレッド以外だがDispatcher経由での呼び出しなのでOK
            var d = new DrivedObject();
            await Task.Run(async () =>
            {
                if (!d.CheckAccess())
                {
                    await d.Dispatcher.InvokeAsync(() => d.DoSomething()); // OK
                }
            });
        }
    }
}
