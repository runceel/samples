using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using Livet.Messaging;
using Codeplex.Reactive;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace LivetWPFSampleApp.Models
{
    public class AppContext : NotificationObject
    {
        public static readonly AppContext Instance = new AppContext();

        private readonly Subject<object> interaction = new Subject<object>();

        public PeopleMaster Master { get; private set; }

        public PersonDetail Detail { get; private set; }

        public AppContext()
        {
            this.Master = new PeopleMaster(this.interaction.AsObservable());
            this.Detail = new PersonDetail(this.interaction);
        }
    }
}
