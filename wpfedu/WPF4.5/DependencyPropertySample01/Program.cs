using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DependencyPropertySample01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------");
            {
                // GetValue, SetValueの使用例
                var p = new Person();
                // 値を取得
                Console.WriteLine(p.GetValue(Person.NameProperty));
                // 値を設定
                p.SetValue(Person.NameProperty, "おおた");
                // 値を取得
                Console.WriteLine(p.GetValue(Person.NameProperty));
            }
            Console.WriteLine("--------");
            {
                // CLRのラッパーの使用例
                var p = new Person();
                Console.WriteLine(p.Name);
                p.Name = "おおた";
                Console.WriteLine(p.Name);
            }
            Console.WriteLine("--------");
            {
                // Childrenプロパティの使用
                var p1 = new Person();
                var p2 = new Person();

                p1.Children.Add(new Person());
                p2.Children.Add(new Person());

                Console.WriteLine("p1.Children.Count = {0}", p1.Children.Count);
                Console.WriteLine("p2.Children.Count = {0}", p2.Children.Count);
            }
            Console.WriteLine("--------");
            {
                var p = new Person();
                p.Age = 10;
                p.Age = -10;
                p.Age = 150;
            }
            Console.WriteLine("--------");
            {
                var p = new Person();
                try
                {
                    // 不正な値なので例外が出る
                    p.Age = int.MinValue;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }

    public class Person : DependencyObject
    {
        public Person()
        {
            // デフォルト値をコンストラクタで指定するようにする
            this.Children = new List<Person>();
        }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(
                "Name", // プロパティ名を指定
                typeof(string), // プロパティの型を指定
                typeof(Person), // プロパティを所有する型を指定
                new PropertyMetadata(
                    "default name", // デフォルト値の設定
                    NamePropertyChanged)); // プロパティの変更時に呼ばれるコールバックの設定

        private static void NamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("Nameプロパティが{0}から{1}に変わりました", e.OldValue, e.NewValue);
        } 

        // 依存関係プロパティのCLRのプロパティのラッパー
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register(
                "Age", 
                typeof(int), 
                typeof(Person), 
                new PropertyMetadata(
                    0,
                    AgeChanged,
                    CoerceAgeValue),
                ValidateAgeValue);

        private static bool ValidateAgeValue(object value)
        {
            // MinValueとMaxValueはやりすぎだろ
            int age = (int)value;
            return age != int.MaxValue && age != int.MinValue;
        }

        private static object CoerceAgeValue(DependencyObject d, object baseValue)
        {
            // 年齢は0-120の間
            var value = (int)baseValue;
            if (value < 0)
            {
                return 0;
            }
            if (value > 120)
            {
                return 120;
            }
            return value;
        }

        private static void AgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("Ageプロパティが{0}から{1}に変わりました。", e.OldValue, e.NewValue);
        }


        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register(
                "Children", 
                typeof(List<Person>), 
                typeof(Person), 
                new PropertyMetadata(new List<Person>())); // デフォルト値は共有される


        public List<Person> Children
        {
            get { return (List<Person>)GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        // RegisterReadOnlyメソッドでDependencyPropertyKeyを取得
        private static readonly DependencyPropertyKey BirthdayPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Birthday",
                typeof(DateTime),
                typeof(Person),
                new PropertyMetadata(DateTime.Now));
        // DependencyPropertyは、DependencyPropertyKeyから取得する
        public static readonly DependencyProperty BirthdayProperty = BirthdayPropertyKey.DependencyProperty;

        public DateTime Birthday
        {
            // getは従来通り
            get { return (DateTime)GetValue(BirthdayProperty); }
            // setはDependencyPropertyKeyを使って行う
            private set { SetValue(BirthdayPropertyKey, value); }
        }



    }
}
