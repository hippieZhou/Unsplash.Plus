using Microsoft.Xaml.Interactivity;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Xaml.Interactions.Core
{
    /// <summary>
    /// A behavior that listens for the <see cref="ListViewBase.ItemClick"/> event on its source and executes a specified command when that event is fired
    /// </summary>
    public sealed class ItemClickBehavior : Behavior<ListViewBase>
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ItemClickBehavior), new PropertyMetadata(DependencyProperty.UnsetValue));

        /// <summary>
        /// Handles a clicked item and invokes the associated command
        /// </summary>
        /// <param name="sender">The current <see cref="ListViewBase"/> instance</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance with the clicked item</param>
        private void HandleItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(Command is ICommand command)) return;
            if (!command.CanExecute(e.ClickedItem)) return;

            command.Execute(e.ClickedItem);
        }

        /// <inheritdoc/>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null) AssociatedObject.ItemClick += HandleItemClick;
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null) AssociatedObject.ItemClick -= HandleItemClick;
        }
    }
}
