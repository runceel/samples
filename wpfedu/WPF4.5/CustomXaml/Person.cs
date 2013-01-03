namespace CustomXaml
{
    using System;

    public class Person
    {
        public Person()
        {
            this.Birthday = DateTime.Now;
        }

        public DateTime Birthday { get; private set; }
    }
}
