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
            Console.WriteLine("p.FullName = {0}, p.Salary = {1}, p.Birthday = {2}", 
                p.FullName,
                p.Salary,
                p.Birthday);
        }
    }
}
