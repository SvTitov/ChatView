using System;
using Xamarin.Forms;

namespace ChatView.Shared
{
    public class MessageCell : ViewCell
    {
        public static readonly BindableProperty MessageBodyProperty =
            BindableProperty.Create("MessageBody", typeof(string), typeof(MessageCell), "");

        public string MessageBody
        {
            get { return (string)GetValue(MessageBodyProperty); }
            set { SetValue(MessageBodyProperty, value); }
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create("Date", typeof(string), typeof(MessageCell), "");

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty IsIncomingProperty =
            BindableProperty.Create("IsIncoming", typeof(bool), typeof(MessageCell), false); 

        public bool IsIncoming
        {
            get { return (bool)GetValue(IsIncomingProperty); }
            set { SetValue(IsIncomingProperty, value); }
        }

        public static readonly BindableProperty StatusProperty =
            BindableProperty.Create("Status", typeof(MessageStatuses), typeof(MessageCell), MessageStatuses.None);

        public MessageStatuses Status
        {
            get { return (MessageStatuses)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }
    }
}
