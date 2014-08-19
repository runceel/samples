using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AttachedPropertySample02
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                // 依存関係プロパティと同様にSetValue、GetValueで値の設定を取得が可能
                var p = new Person();
                p.SetValue(Sample.BirthdayProperty, DateTime.Now);
                Console.WriteLine(p.GetValue(Sample.BirthdayProperty));
            }
            {
                // 通常はラッパーを使ってアクセスする
                var p = new Person();
                Sample.SetBirthday(p, DateTime.Now);
                Console.WriteLine(Sample.GetBirthday(p));
            }
        }
    }

    public static class Sample
    {
        // RegisterAttachedメソッドを使って添付プロパティを作成する
        public static readonly DependencyProperty BirthdayProperty =
            DependencyProperty.RegisterAttached(
                "Birthday", 
                typeof(DateTime), 
                typeof(Sample), 
                new PropertyMetadata(DateTime.MinValue));

        // プログラムからアクセスするための添付プロパティのラッパー
        public static DateTime GetBirthday(DependencyObject obj)
        {
            return (DateTime)obj.GetValue(BirthdayProperty);
        }

        public static void SetBirthday(DependencyObject obj, DateTime value)
        {
            obj.SetValue(BirthdayProperty, value);
        }
    }

    public class Person : DependencyObject { }
}
