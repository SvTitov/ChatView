using System;
using System.ComponentModel;
using ChatView.Shared;

namespace ChatViewTest.Core
{
    public class MessageModel : INotifyPropertyChanged
    {
        private string _message;
        private MessageStatuses _status;
        private string _date;
        private bool _isIncoming;
        private string _name;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }

        public string Date 
        { 
            get { return _date; }
            set 
            {
                _date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }

        public bool IsIncoming
        {
            get { return _isIncoming; }
            set 
            {
                _isIncoming = value;
                NotifyPropertyChanged(nameof(IsIncoming));
            }
        }

        public string Name 
        {
            get { return _name; }
            set 
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }


        public MessageStatuses Status 
        { 
            get { return _status; }
            set 
            { 
                _status = value;
                NotifyPropertyChanged(nameof(Status));
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
