using Unsplash.Plus.Models;
using Unsplash.Plus.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Unsplash.Plus.Views
{
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
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackConnectedAnimation", HeroImage);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.SelectedItem = e.Parameter as PhotoItem;
            ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (imageAnimation != null)
            {
                imageAnimation.TryStart(HeroImage, new UIElement[] { Header });
            }
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
