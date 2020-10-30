using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace OneSplash.UwpApp.Controls
{
    /// <summary>
    /// https://blog.pieeatingninjas.be/2016/01/17/custom-uwp-control-step-through-listview/
    /// https://www.cnblogs.com/hupo376787/p/11837285.html
    /// https://www.arthurrump.com/2018/07/21/animating-gridviewitems-with-windows-ui-composition-aka-the-visual-layer/
    /// </summary>
    public sealed partial class StepThroughListView : UserControl, INotifyPropertyChanged
    {
        public StepThroughListView()
        {
            this.InitializeComponent();
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                if (SelectedItem != value)
                {
                    SetValue(SelectedItemProperty, value);
                    RaisePropertyChanged();
                }
            }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set
            {
                SetValue(ItemTemplateProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(StepThroughListView), new PropertyMetadata(DependencyProperty.UnsetValue));


        private void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            var lv = sender as ListView;
           var item =  lv.Items.Cast<ListViewItem>();
        }
    }
}
