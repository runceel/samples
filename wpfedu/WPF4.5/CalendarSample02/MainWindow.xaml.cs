using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalendarSample02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 今日より前は、選択不可能にする。
            this.calendar.BlackoutDates.AddDatesInPast();
            // 翌日から4日間も選択不可能にする
            this.calendar.BlackoutDates.Add(
                new CalendarDateRange(
                    DateTime.Today.AddDays(1),
                    DateTime.Today.AddDays(4)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 選択された日付を連結して表示
            var selected = string.Join(Environment.NewLine, 
                this.calendar.SelectedDates.Select(d => d.ToString()));
            MessageBox.Show(selected);
        }
    }
}
