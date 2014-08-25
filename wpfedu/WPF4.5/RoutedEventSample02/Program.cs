using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoutedEventSample02
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parent = new Person { Name = "parent" };
            var child = new Person { Name = "child" };

            parent.AddChild(child);

            parent.ToAge += (object s, RoutedEventArgs e) =>
            {
                Console.WriteLine(((Person)e.Source).Name);
            };

            parent.RaiseEvent(new RoutedEventArgs(Person.ToAgeEvent));
            child.RaiseEvent(new RoutedEventArgs(Person.ToAgeEvent));
        }
    }

    class Person : FrameworkElement
    {
        // イベント名Eventの命名規約のstaticフィールドに格納する
        public static RoutedEvent ToAgeEvent = EventManager.RegisterRoutedEvent(
            "ToAge", // イベント名
            RoutingStrategy.Tunnel, // イベントタイプ
            typeof(RoutedEventHandler), // イベントハンドラの型
            typeof(Person)); // イベントのオーナー
        // CLRのイベントのラッパー
        public event RoutedEventHandler ToAge
        {
            add { this.AddHandler(ToAgeEvent, value); }
            remove { this.RemoveHandler(ToAgeEvent, value); }
        }

        // 子を追加するメソッド
        public void AddChild(Person child)
        {
            this.AddLogicalChild(child);
        }
    }
}
