using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampRcon.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                RaiseOnPropertyChanged();
            }
        }
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaiseOnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}