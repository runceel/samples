using System;
using System.Collections.Generic;
using System.Linq;

namespace XamDataGridSample01
{
    public class Person
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class MainWindowViewModel
    {
        public List<Person> People { get; set; }

        public MainWindowViewModel()
        {
            this.People = Enumerable.Range(1, 1000000)
                .Select(x => new Person
                {
                    Name = "okazuki " + x,
                    Birthday = DateTime.Now,
                    Salary = 200000 + x % 50000
                })
                .ToList();
        }
    }
}
