using System.Collections.Generic;

namespace TreeViewSample03
{
    public class Person
    {
        public string Name { get; set; }
        public List<Person> Children { get; set; }
    }
}
