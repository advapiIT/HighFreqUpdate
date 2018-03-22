using Catel.MVVM;
using IF.WPF.Infragistics.Persistence.DockManager.Interfaces;
using Infragistics.Windows.DockManager;

namespace DockingInfragistics.ViewModels
{
    public class BottomViewModel : ViewModelBase, IInitialPosition
    {
        public InitialPaneLocation InitialDockState => InitialPaneLocation.DockedRight;
    }
}
