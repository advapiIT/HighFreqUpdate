
using Infragistics.Windows.DataPresenter;
using System.Threading.Tasks;

namespace HighFreqUpdate.Services.Interfaces
{
    public interface IGridPersistenceService
    {
        Task PersistGrid(XamDataGrid grid);

        Task RestoreGrid(XamDataGrid grid);
    }
}
