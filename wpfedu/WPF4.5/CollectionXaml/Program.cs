namespace CollectionXaml
{
    using System;
    using System.Windows.Markup;

    class Program
    {
        static void Main(string[] args)
        {
            var s = typeof(Program).Assembly.GetManifestResourceStream("CollectionXaml.Item.xaml");
            var item = XamlReader.Load(s) as Item;

            // Itemの内容を表示
            Console.WriteLine(item.Id);
            foreach (var i in item.Children)
            {
                Console.WriteLine("  {0}", i.Id);
            }
        }
    }
}
