using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DependencyPropertySample02
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parent = new Person { FirstName = "taro", LastName = "tanaka" };
            var child = new Person { FirstName = "jiro" };

            parent.AddChild(child);

            Console.WriteLine("{0} {1}", parent.LastName, parent.FirstName);
            Console.WriteLine("{0} {1}", child.LastName, child.FirstName);
        }
    }

    public class Person : FrameworkElement
    {

        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register(
                "FirstName", 
                typeof(string), 
                typeof(Person),
                new FrameworkPropertyMetadata(null));

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }



        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(
                "LastName", 
                typeof(string), 
                typeof(Person),
                // 子要素へ継承するプロパティ
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits));

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public void AddChild(Person child)
        {
            this.AddLogicalChild(child);
        }
    }
}
