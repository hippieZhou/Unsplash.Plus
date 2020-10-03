using Unsplash.Plus.Models;

namespace Unsplash.Plus.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private PhotoItem _selectedItem;
        public PhotoItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
    }
}
