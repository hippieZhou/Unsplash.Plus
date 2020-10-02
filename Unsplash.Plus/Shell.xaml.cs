using Unsplash.Plus.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Unsplash.Plus
{
    public sealed partial class Shell : Page
    {
        public ShellViewModel ViewModel => DataContext as ShellViewModel;
        public Shell()
        {
            this.InitializeComponent();
            ViewModel.Initialize(ContentFrame);
        }
    }
}
