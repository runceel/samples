using System.Windows;
using System.Windows.Controls;

namespace DataTemplateSample02
{
    public class PersonDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var p = (Person)item;
            if (p.Age < 40)
            {
                // Ageが40より小さければPersonTemplate1
                return (DataTemplate)((FrameworkElement)container).FindResource("PersonTemplate1");
            }
            else
            {
                // Ageが40以上ならPersonTemplate2
                return (DataTemplate)((FrameworkElement)container).FindResource("PersonTemplate2");
            }
        }
    }
}
