using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDataGridComboboxColumnSample
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Person> People { get; private set; }

        public MainWindowViewModel()
        {
            this.People = new ObservableCollection<Person>(
                Enumerable.Range(1, 100).Select(x => new Person { Name = "okazuki" + x }));
        }
    }
}
