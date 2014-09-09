using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CreateControlSample02
{
    [TemplatePart(Type = typeof(RepeatButton), Name = "PART_UpButton")]
    [TemplatePart(Type = typeof(RepeatButton), Name = "PART_DownButton")]
    [TemplateVisualState(GroupName = "PositiveNegative", Name = "Negative")]
    [TemplateVisualState(GroupName = "PositiveNegative", Name = "Positive")]
    public class NumericUpDown : Control
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", 
                typeof(int), 
                typeof(NumericUpDown), 
                new PropertyMetadata(0, ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumericUpDown)d).UpdateState(true);
        }


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // XAMLで定義されたボタン格納用変数
        private RepeatButton upButton;
        private RepeatButton downButton;

        // ボタンのクリックイベント
        private void UpClick(object sender, RoutedEventArgs e) { this.Value++; }
        private void DownClick(object sender, RoutedEventArgs e) { this.Value--; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 前のテンプレートのコントロールの後処理
            if (this.upButton != null)
            {
                this.upButton.Click -= this.UpClick;
            }
            if (this.downButton != null)
            {
                this.downButton.Click -= this.DownClick;
            }

            // テンプレートからコントロールの取得
            this.upButton = this.GetTemplateChild("PART_UpButton") as RepeatButton;
            this.downButton = this.GetTemplateChild("PART_DownButton") as RepeatButton;

            // イベントハンドラの登録
            if (this.upButton != null)
            {
                this.upButton.Click += this.UpClick;
            }
            if (this.downButton != null)
            {
                this.downButton.Click += this.DownClick;
            }

            // VSMの更新
            this.UpdateState(false);
        }

        private void UpdateState(bool useTransition)
        {
            if (this.Value >= 0)
            {
                VisualStateManager.GoToState(this, "Positive", useTransition);
            }
            else
            {
                VisualStateManager.GoToState(this, "Negative", useTransition);
            }
        }

    }
}
