using System;
using ChatView.Shared.Abstraction;

namespace ChatView.Shared.Models
{
    public class TextMessage : BaseUsersMessageModel
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }
    }
}
