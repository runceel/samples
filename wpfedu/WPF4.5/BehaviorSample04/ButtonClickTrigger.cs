using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace BehaviorSample04
{
    [TypeConstraint(typeof(Button))]
    public class ButtonClickTrigger : TriggerBase<Button>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Click += this.ButtonClick;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.InvokeActions(e);
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Click -= this.ButtonClick;
        }
    }
}
