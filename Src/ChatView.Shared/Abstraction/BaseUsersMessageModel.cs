using System;
namespace ChatView.Shared.Abstraction
{
    public abstract class BaseUsersMessageModel : BaseMessageModel
    {
        private MessageStatuses _status;
        private string _date;
        private bool _isIncoming;
        private string _name;

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
    }
}
