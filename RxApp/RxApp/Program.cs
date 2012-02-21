using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Linq;


namespace RxApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Subject<int>();
            var plan1 = s.Select(i => i).And(Observable.Return(1)).And(Observable.Return("A")).And(Observable.Return(0.1)).Then(Tuple.Create);
            var plan2 = s.Select(i => i).And(Observable.Return(2)).And(Observable.Return("A")).And(Observable.Return(0.1)).Then(Tuple.Create);
            var plan3 = s.Select(i => i).And(Observable.Return(3)).And(Observable.Return("A")).And(Observable.Return(0.1)).Then(Tuple.Create);

            Observable.When(plan1, plan2, plan3)
                .Subscribe(t =>
                    Console.WriteLine("{0} {1} {2} {3}", t.Item1, t.Item2, t.Item3, t.Item4));

            s.OnNext(100);
            s.OnNext(1);
            s.OnCompleted();
        }
    }
}
