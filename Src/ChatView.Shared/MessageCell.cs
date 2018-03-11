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

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(MessageCell), string.Empty);

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty TextFontSizeProperty =
            BindableProperty.Create("TextFontSize", typeof(int), typeof(MessageCell), 14);

        public int TextFontSize 
        { 
            get => (int)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        public static readonly BindableProperty InfoFontSizeProperty =
            BindableProperty.Create("InfoFontSize", typeof(int), typeof(MessageCell), 10);

        public int InfoFontSize
        { 
            get => (int)GetValue(InfoFontSizeProperty);
            set => SetValue(InfoFontSizeProperty, value);
        }

        public static readonly BindableProperty NameFontSizeProperty =
            BindableProperty.Create("NameFontSize", typeof(int), typeof(MessageCell), 16);

        public int NameFontSize
        {
            get => (int)GetValue(NameFontSizeProperty);
            set => SetValue(NameFontSizeProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof(int), typeof(MessageCell), 20);

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty IncomingColorProperty =
            BindableProperty.Create("IncomingColor", typeof(Color), typeof(MessageCell), Color.FromRgb(66,165,245));

        public Color IncomingColor
        {
            get => (Color)GetValue(IncomingColorProperty);
            set => SetValue(IncomingColorProperty, value);
        }

        public static readonly BindableProperty OutgoingColorProperty =
            BindableProperty.Create("OutgoingColor", typeof(Color), typeof(MessageCell), Color.FromRgb(0,230,118));

        public Color OutgoingColor
        {
            get => (Color)GetValue(OutgoingColorProperty);
            set => SetValue(OutgoingColorProperty, value);
        }

        public static readonly BindableProperty TextFontColorProperty =
            BindableProperty.Create("TextFontColor", typeof(Color), typeof(MessageCell), Color.Black);

        public Color TextFontColor
        {
            get => (Color)GetValue(TextFontColorProperty);
            set => SetValue(TextFontColorProperty, value);
        }

        public static readonly BindableProperty NameFontColorProperty =
            BindableProperty.Create("NameFontColor", typeof(Color), typeof(MessageCell), Color.Blue);

        public Color NameFontColor
        {
            get => (Color)GetValue(NameFontColorProperty);
            set => SetValue(NameFontColorProperty, value);
        }

        public static readonly BindableProperty InfoFontColorProperty =
            BindableProperty.Create("InfoFontColor", typeof(Color), typeof(MessageCell), Color.Black);

        public Color InfoFontColor
        {
            get => (Color)GetValue(InfoFontColorProperty);
            set => SetValue(InfoFontColorProperty, value);
        }
    }
}
