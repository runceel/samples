using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace BehaviorSample04
{
    [TypeConstraint(typeof(Button))]
    [DefaultTrigger(typeof(Button), typeof(ButtonClickTrigger))]
    public class AlertAction : TriggerAction<Button>
    {
        protected override void Invoke(object parameter)
        {
            MessageBox.Show("Hello world");
        }
    }
}
