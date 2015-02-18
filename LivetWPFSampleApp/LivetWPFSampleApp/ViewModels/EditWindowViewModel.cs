using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reactive.Linq;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetWPFSampleApp.Models;
using Codeplex.Reactive;
using Codeplex.Reactive.Extensions;

namespace LivetWPFSampleApp.ViewModels
{
    public class EditWindowViewModel : ViewModel
    {
        private readonly AppContext Model = AppContext.Instance;

        public ReactiveProperty<PersonViewModel> EditTarget { get; private set; }

        public ReactiveCommand EditCommand { get; private set; }

        public EditWindowViewModel()
        {
            this.EditTarget = this.Model.Detail
                .ObserveProperty(x => x.EditTarget)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);

            this.EditCommand = this.EditTarget
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveCommand();
            this.EditCommand
                .Subscribe(_ =>
                    {
                        this.Model.Detail.Update();
                        this.Messenger.Raise(new WindowActionMessage(WindowAction.Close, "WindowClose"));
                    });
        }

        public void Initialize()
        {
        }
    }
}
