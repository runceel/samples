using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace TwitterAppSample.Views.Fragments
{
    public sealed partial class StatusMessageFragment : UserControl
    {
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(StatusMessageFragment), new PropertyMetadata(false, IsSelectedChanged));

        private static void IsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                VisualStateManager.GoToState((Control)d, "Selected", true);
            }
            else
            {
                VisualStateManager.GoToState((Control)d, "NoSelected", true);
            }
        }

        public object PageDataContext
        {
            get { return (object)GetValue(PageDataContextProperty); }
            set { SetValue(PageDataContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageDataContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageDataContextProperty =
            DependencyProperty.Register("PageDataContext", typeof(object), typeof(StatusMessageFragment), new PropertyMetadata(null));

        public StatusMessageFragment()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var selector = this.GetSelectorItem();
            this.SetBinding(IsSelectedProperty, new Binding
                {
                    Path = new PropertyPath("IsSelected"),
                    Source = selector
                });

            var page = this.GetPage();
            this.SetBinding(PageDataContextProperty, new Binding
                {
                    Path = new PropertyPath("DataContext"),
                    Source = page
                });
        }

        private SelectorItem GetSelectorItem()
        {
            DependencyObject dp = this;
            while ((dp = VisualTreeHelper.GetParent(dp)) != null)
            {
                var i = dp as SelectorItem;
                if (i != null) { return i; }
            }

            return null;
        }

        private Page GetPage()
        {
            DependencyObject dp = this;
            while ((dp = VisualTreeHelper.GetParent(dp)) != null)
            {
                var i = dp as Page;
                if (i != null) { return i; }
            }

            return null;
        }
    }
}
