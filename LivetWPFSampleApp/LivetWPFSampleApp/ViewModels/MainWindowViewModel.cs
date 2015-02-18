using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetWPFSampleApp.Models;
using Codeplex.Reactive;
using Codeplex.Reactive.Extensions;
using System.Windows;

namespace LivetWPFSampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public ReadOnlyReactiveCollection<PersonViewModel> People { get; private set; }

        private readonly AppContext Model = AppContext.Instance;

        public ReactiveProperty<PersonViewModel> InputPerson { get; private set; }

        public ReactiveProperty<PersonViewModel> SelectedPerson { get; private set; }

        public ReactiveCommand AddCommand { get; private set; }

        public ReactiveCommand DeleteCommand { get; private set; }

        public ReactiveCommand EditCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.People = this.Model.Master.People
                .ToReadOnlyReactiveCollection(x => new PersonViewModel(x))
                .AddTo(this.CompositeDisposable);

            this.SelectedPerson = new ReactiveProperty<PersonViewModel>();

            this.InputPerson = this.Model.Master
                .ObserveProperty(x => x.InputPerson)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty();

            this.AddCommand = this.InputPerson
                .Where(x => x != null)
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveCommand(false);
            this.AddCommand.Subscribe(_ =>
                {
                    this.Model.Master.AddPerson();
                });

            this.DeleteCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand();
            this.DeleteCommand
                .Select(_ => new ConfirmationMessage("削除してもいいですか", "確認", MessageBoxImage.Information, MessageBoxButton.OKCancel, "DeleteConfirm"))
                .SelectMany(m => this.Messenger.RaiseAsync(m).ToObservable().Select(_ => m))
                .Where(m => m.Response == true)
                .Select(_ => this.SelectedPerson.Value.Model.ID)
                .Subscribe(x =>
                {
                    this.Model.Master.Delete(x);
                });

            this.EditCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand();
            this.EditCommand
                .Subscribe(_ =>
                {
                    this.Model.Detail.SetEditTarget(this.SelectedPerson.Value.Model.ID);
                    this.Messenger.Raise(new TransitionMessage("EditWindowOpen"));
                });
        }

        public void Initialize()
        {
            this.Model.Master.Load();
        }
    }
}
