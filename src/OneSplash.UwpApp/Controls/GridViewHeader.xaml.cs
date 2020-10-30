using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace OneSplash.UwpApp.Controls
{
    public sealed partial class GridViewHeader : UserControl
    {
        public GridViewHeader()
        {
            this.InitializeComponent();
            animatedScrollRepeater.ItemsSource = GetColors();
        }
        private IList<string> GetColors()
        {
            IList<string> colors = (typeof(Colors).GetRuntimeProperties().Select(c => c.ToString())).ToList();
            for (int i = 0; i < colors.Count(); i++)
            {
                colors[i] = colors[i].Substring(17);

            }
            return colors;
        }
    }
}
