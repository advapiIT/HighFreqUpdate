using Infragistics.Windows.DataPresenter;
using System.IO;
using System.Threading.Tasks;

namespace IF.WPF.Infragistics.Persistence
{
    public interface IGridPersistenceService
    {
        Task PersistGridAsync(XamDataGrid grid, Stream stream, bool closeStream = false);

        Task RestoreGridAsync(XamDataGrid grid, Stream stream, bool closeStream = false);
    }
}
