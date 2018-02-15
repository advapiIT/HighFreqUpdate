using System.IO;
using System.Threading.Tasks;
using Infragistics.Windows.DockManager;

namespace IF.WPF.Infragistics.Persistence.Services.Interfaces
{
    public interface IDockManagerPersistenceService
    {
        Task PersistGrid(XamDockManager dockManager, Stream stream, bool closeStream = false);

        Task RestoreGrid(XamDockManager dockManager, Stream stream, bool closeStream = false);
    }
}
