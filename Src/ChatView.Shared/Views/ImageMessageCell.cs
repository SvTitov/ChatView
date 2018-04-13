using System;
using Xamarin.Forms;

namespace ChatView.Shared.Views
{
    public class ImageMessageCell : UserMessageCell
    {
        public static readonly BindableProperty ImageUriProperty =
            BindableProperty.Create("ImageUri", typeof(Uri), typeof(MessageCell), null);

        public Uri ImageUri
        {
            get { return (Uri)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public static readonly BindableProperty IsCachedProperty =
            BindableProperty.Create("IsCached", typeof(bool), typeof(MessageCell), true);

        public bool IsCached
        {
            get { return (bool)GetValue(IsCachedProperty); }
            set { SetValue(IsCachedProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(MessageCell), string.Empty);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
    }
}
