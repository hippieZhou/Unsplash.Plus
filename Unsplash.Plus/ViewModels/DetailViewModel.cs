using Unsplash.Plus.Models;

namespace Unsplash.Plus.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private Photo _selectedItem;
        public Photo SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
    }
}
