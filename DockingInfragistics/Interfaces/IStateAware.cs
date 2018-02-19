using System.Collections.Generic;

namespace DockingInfragistics.Interfaces
{
    public interface IStateAware
    {
        string PersistenceBag { get; }

        //  void LoadState(IDictionary<string, object> state);

        IDictionary<string, object> SavedPersistenceBag { set; }

    }
}
