using Infragistics.Windows.DataPresenter;
using System.IO;
using System.Threading.Tasks;

namespace IF.WPF.Infragistics.Persistence
{
    public interface IGridPersistenceService
    {
        Task PersistGrid(XamDataGrid grid, Stream stream, bool closeStream = false);

        Task RestoreGrid(XamDataGrid grid, Stream stream, bool closeStream = false);
    }
}
