namespace CustomXaml
{
    using System;
    using System.Windows.Markup;

    class Program
    {
        public static void Main(string[] args)
        {
            // アセンブリから対象のXAMLのストリームを取得
            var s = typeof(Program).Assembly.GetManifestResourceStream("CustomXaml.Person.xaml");
            // パース
            var p = XamlReader.Load(s) as Person;
            // プロパティを表示してみる
            Console.WriteLine("FullName = {0}, Father.FullName = {1}, Mother.FullName = {2}", 
                p.FullName,
                p.Father.FullName,
                p.Mother.FullName);
        }
    }
}
