using System;
using System.ComponentModel;

namespace ChatView.Shared.Abstraction
{
    public abstract class BaseMessageModel : INotifyPropertyChanged
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public object Tag { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
