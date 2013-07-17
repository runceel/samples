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

namespace DatePickerSample01
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.datePickerBlackout.BlackoutDates.AddDatesInPast();
            this.datePickerBlackout.BlackoutDates.Add(
                new CalendarDateRange(
                    DateTime.Today.AddDays(10),
                    DateTime.Today.AddDays(20)));
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            this.textBlockSelectedDate.Text = datePicker.SelectedDate.ToString();
        }
    }
}
