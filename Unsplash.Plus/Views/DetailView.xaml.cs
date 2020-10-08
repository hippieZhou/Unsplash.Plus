using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using System.Threading.Tasks;
using Unsplash.Plus.Models;
using Unsplash.Plus.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace Unsplash.Plus.Views
{
    public sealed partial class DetailView : Page
    {
        private bool _firstTimeAnimation = true;
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

            ViewModel.SelectedItem = e.Parameter as Photo;
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

        private async void OnPlanTripClick(object sender, RoutedEventArgs e)
        {
            PlanTrip.IsEnabled = false;

            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(HeroImage);
            HeroImageMirror.Source = bitmap;

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("storePlace", HeroImageMirror);

            if (!App.Locator.Main.PickedPlaces.Contains(ViewModel.SelectedItem))
            {
                App.Locator.Main.PickedPlaces.Add(ViewModel.SelectedItem);
            }

            var navView = this.FindAscendant<Shell>().FindDescendant<WinUI.NavigationView>();
            if (navView.PaneCustomContent.FindDescendantByName("PlaceStore") is Button placeStoreButton)
            {
                var dot = placeStoreButton.FindDescendant<Ellipse>();

                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("storePlace");
                animation?.TryStart(dot);
                dot.Visibility = Visibility.Visible;

                // JL: Need to figutre out why the first time the animation doesn't run although animation returns true.
                if (_firstTimeAnimation)
                {
                    _firstTimeAnimation = false;
                    await Task.Delay(50);
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("storePlace", HeroImageMirror);
                    var animation1 = ConnectedAnimationService.GetForCurrentView().GetAnimation("storePlace");
                    animation1?.TryStart(dot);
                }
            }

            PlanTrip.IsEnabled = true;
        }
    }
}
