using System;
using ChatView.Shared.Abstraction;

namespace ChatView.Shared.Models
{
    public class SystemMessage : BaseMessageModel
    {
        private string _messageText;

        public string MessageText 
        {
            get => _messageText;
            set 
            {
                _messageText = value;
                NotifyPropertyChanged(nameof(MessageText));
            }
        }
    }
}
