using System;
using Xamarin.Forms;

namespace ChatView.Shared.Views
{
    public class TextMessageCell : UserMessageCell
    {
        public static readonly BindableProperty MessageBodyProperty =
            BindableProperty.Create("MessageBody", typeof(string), typeof(MessageCell), "");

        public string MessageBody
        {
            get { return (string)GetValue(MessageBodyProperty); }
            set { SetValue(MessageBodyProperty, value); }
        }

        public static readonly BindableProperty TextFontSizeProperty =
            BindableProperty.Create("TextFontSize", typeof(int), typeof(MessageCell), 14);

        public int TextFontSize
        {
            get => (int)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        public static readonly BindableProperty TextFontColorProperty =
            BindableProperty.Create("TextFontColor", typeof(Color), typeof(MessageCell), Color.Black);

        public Color TextFontColor
        {
            get => (Color)GetValue(TextFontColorProperty);
            set => SetValue(TextFontColorProperty, value);
        }
    }
}
