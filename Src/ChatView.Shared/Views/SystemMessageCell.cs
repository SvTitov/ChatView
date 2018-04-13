using System;
using Xamarin.Forms;

namespace ChatView.Shared.Views
{
    public class SystemMessageCell : MessageCell
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(Uri), typeof(MessageCell), string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
