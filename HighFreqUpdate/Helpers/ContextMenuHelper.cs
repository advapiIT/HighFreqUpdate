using Catel.Windows;
using HighFreqUpdate.Extensions;
using Infragistics.Controls.Menus;

using System.Windows;
using System.Windows.Controls;

namespace HighFreqUpdate.Helpers
{
    public static class ContextMenuHelper
    {
        /// <summary>
        /// Creates a new instance of <c>RadContextMenu with default style value</c>
        /// </summary>
        /// <returns>The created RadContextMenu </returns>
        public static ContextMenuManager CreateNewContextMenuWithDefaults()
        {
            var contextMenu = new ContextMenuManager
            {
                ContextMenu = new XamContextMenu
                {
                    FontStyle = FontStyles.Normal,
                    FontSize = SystemFonts.MessageFontSize
                }
            };

            return contextMenu;
        }

        /// <summary>
        /// Set default values for ContextMenu
        /// </summary>
        /// <param name="contextMenu"></param>
        public static void SetDefaultValues(XamContextMenu contextMenu)
        {
            contextMenu.FontSize = SystemFonts.MessageFontSize;
            contextMenu.FontStyle = FontStyles.Normal;
        }


        /// <summary>
        /// Returns the view model name that's associated to the eleemnt's containing view
        /// </summary>
        /// <param name="element">The Ui Element</param>
        /// <returns>The FullName of the viewmodel, <c>null</c> otherwise</returns>
        public static string GetViewModelName(FrameworkElement element)
        {

            var view = element.ParentOfType<UserControl>() ?? (FrameworkElement)element.ParentOfType<DataWindow>();

            return view == null ? string.Empty : view.DataContext.GetType().FullName;
        }
    }
}
