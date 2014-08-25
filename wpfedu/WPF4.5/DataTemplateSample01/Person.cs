
namespace DataTemplateSample01
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsOver40 { get { return this.Age >= 40; } }
    }
}
