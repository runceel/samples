using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace MVVMSample01
{
    public class MainWindowViewModel : BindableBase
    {
        private string input;
        /// <summary>
        /// 入力値
        /// </summary>
        public string Input
        {
            get { return this.input; }
            set 
            {
                this.SetProperty(ref this.input, value);
                // 入力値に変かがある度にコマンドのCanExecuteの状態が変わったことを通知する
                this.ConvertCommand.RaiseCanExecuteChanged();
            }
        }

        private string output;

        /// <summary>
        /// 出力値
        /// </summary>
        public string Output
        {
            get { return this.output; }
            set { this.SetProperty(ref this.output, value); }
        }

        /// <summary>
        /// 変換コマンド
        /// </summary>
        public DelegateCommand ConvertCommand { get; private set; }

        public MainWindowViewModel()
        {
            // 変換コマンドに実際の処理をわたして初期化
            this.ConvertCommand = new DelegateCommand(
                this.ConvertExecute,
                this.CanConvertExecute);
        }

        /// <summary>
        /// 大文字に変換
        /// </summary>
        private void ConvertExecute()
        {
            this.Output = this.Input.ToUpper();
        }

        /// <summary>
        /// 何か入力されてたら実行可能
        /// </summary>
        /// <returns></returns>
        private bool CanConvertExecute()
        {
            return !string.IsNullOrWhiteSpace(this.Input);
        }

    }
}
