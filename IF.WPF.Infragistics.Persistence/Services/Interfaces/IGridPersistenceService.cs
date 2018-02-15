
using Infragistics.Windows.DataPresenter;
using System.Threading.Tasks;

namespace IF.WPF.Infragistics.Persistence
{
    public interface IGridPersistenceService
    {
        Task PersistGrid(XamDataGrid grid);

        Task RestoreGrid(XamDataGrid grid);
    }
}
