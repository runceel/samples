namespace MarkupExtensionSample
{
    using System;
    using System.Windows.Markup;

    class Program
    {
        static void Main(string[] args)
        {
            // XAMLを読み込んでIdを表示
            var item = XamlReader.Load(
                typeof(Program).Assembly.GetManifestResourceStream("MarkupExtensionSample.Item.xaml")) as Item;
            Console.WriteLine(item.Id);

            // 再度XAMLを読み込んでIdを表示
            var item2 = XamlReader.Load(
                typeof(Program).Assembly.GetManifestResourceStream("MarkupExtensionSample.Item.xaml")) as Item;
            Console.WriteLine(item2.Id);
        }
    }
}
