using System.IO;
using System.Threading.Tasks;
using Infragistics.Windows.DockManager;

namespace IF.WPF.Infragistics.Persistence.Services.Interfaces
{
    public interface IDockManagerPersistenceService
    {
        Task PersistGridAsync(XamDockManager dockManager, Stream stream, bool closeStream = false);

        Task RestoreGridAsync(XamDockManager dockManager, Stream stream, bool closeStream = false);
    }
}
