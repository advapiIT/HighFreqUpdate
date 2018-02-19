using System.ComponentModel;

namespace HighFreqUpdate.Interfaces
{
    public interface ICheckable : INotifyPropertyChanged
    {
        bool IsChecked { get; set; }
    }
}