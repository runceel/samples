using Livet;
using Livet.Messaging.Windows;
using LivetWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Linq;

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
                .ToReactiveCommand()
                .AddTo(this.CompositeDisposable);
            this.EditCommand
                .Subscribe(_ =>
                    {
                        this.Model.Detail.Update();
                        this.Messenger.Raise(new WindowActionMessage(WindowAction.Close, "WindowClose"));
                    })
                .AddTo(this.CompositeDisposable);
        }

        public void Initialize()
        {
        }
    }
}
