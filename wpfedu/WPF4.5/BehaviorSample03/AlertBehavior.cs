using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace BehaviorSample03
{
    [TypeConstraint(typeof(Button))]
    public class AlertBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            // AssociatedObjectのイベントを購読する
            this.AssociatedObject.Click += this.ButtonClicked;
        }

        protected override void OnDetaching()
        {
            // イベントの購読解除
            this.AssociatedObject.Click += this.ButtonClicked;
        }

        // イベントで処理をする
        private void ButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Hello world");
        }

    }
}
