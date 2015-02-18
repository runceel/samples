using Livet;
using Livet.Messaging;
using LivetWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows;

namespace LivetWPFSampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// 編集対象のデータ一覧
        /// </summary>
        public ReadOnlyReactiveCollection<PersonViewModel> People { get; private set; }

        /// <summary>
        /// モデルのインスタンス
        /// </summary>
        private readonly AppContext Model = AppContext.Instance;

        /// <summary>
        /// 追加用の入力ホルダー
        /// </summary>
        public ReactiveProperty<PersonViewModel> InputPerson { get; private set; }

        /// <summary>
        /// 選択中のデータ
        /// </summary>
        public ReactiveProperty<PersonViewModel> SelectedPerson { get; private set; }

        /// <summary>
        /// 追加コマンド
        /// </summary>
        public ReactiveCommand AddCommand { get; private set; }

        /// <summary>
        /// 削除コマンド
        /// </summary>
        public ReactiveCommand DeleteCommand { get; private set; }

        /// <summary>
        /// 編集コマンド
        /// </summary>
        public ReactiveCommand EditCommand { get; private set; }

        public MainWindowViewModel()
        {
            // MのコレクションをVMのコレクションに変換
            this.People = this.Model.Master.People
                .ToReadOnlyReactiveCollection(x => new PersonViewModel(x))
                .AddTo(this.CompositeDisposable);

            this.SelectedPerson = new ReactiveProperty<PersonViewModel>();

            // 入力対象のPerson
            this.InputPerson = this.Model.Master
                .ObserveProperty(x => x.InputPerson)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);

            // 入力対象のPersonにエラーがないときだけ押せるコマンド
            this.AddCommand = this.InputPerson
                .Where(x => x != null)
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveCommand(false)
                .AddTo(this.CompositeDisposable);
            this.AddCommand.Subscribe(_ =>
                {
                    this.Model.Master.AddPerson();
                })
                .AddTo(this.CompositeDisposable);

            // 選択中の項目があるときだけ押せるコマンド
            this.DeleteCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand()
                .AddTo(this.CompositeDisposable);
            this.DeleteCommand
                // 確認メッセージを投げて
                .Select(_ => new ConfirmationMessage("削除してもいいですか", "確認", MessageBoxImage.Information, MessageBoxButton.OKCancel, "DeleteConfirm"))
                // 結果を受け取って
                .SelectMany(m => this.Messenger.RaiseAsync(m).ToObservable().Select(_ => m))
                // 判別して
                .Where(m => m.Response == true)
                // 対象のデータを
                .Select(_ => this.SelectedPerson.Value.Model.ID)
                // 削除する
                .Subscribe(x => this.Model.Master.Delete(x))
                .AddTo(this.CompositeDisposable);

            // 選択中の項目があるときだけ押せるコマンド
            this.EditCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand()
                .AddTo(this.CompositeDisposable);
            this.EditCommand
                .Subscribe(_ =>
                {
                    // 編集のターゲットを設定して画面を表示するメッセージを投げる
                    this.Model.Detail.SetEditTarget(this.SelectedPerson.Value.Model.ID);
                    this.Messenger.Raise(new TransitionMessage("EditWindowOpen"));
                })
                .AddTo(this.CompositeDisposable);
        }

        public void Initialize()
        {
            this.Model.Master.Load();
        }
    }
}
