using Unsplash.Plus.Models;
using Unsplash.Plus.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Unsplash.Plus.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailView : Page
    {
        public DetailViewModel ViewModel => DataContext as DetailViewModel;
        public DetailView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardToMain", HeroImage);
                anim.Configuration = new DirectConnectedAnimationConfiguration();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.SelectedItem = e.Parameter as PhotoItem;
            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("mainToDetail");
            anim?.TryStart(HeroImage, new UIElement[] { Header });
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
