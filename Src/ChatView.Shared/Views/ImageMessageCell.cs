using System;
using System.Threading.Tasks;
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

        public static readonly BindableProperty ImageByteArrayProperty =
            BindableProperty.Create("ImageByteArray", typeof(byte[]), typeof(MessageCell), null);

        public byte[] ImageByteArray
        {
            get { return (byte[])GetValue(ImageByteArrayProperty);}
            set { SetValue(ImageByteArrayProperty, value); }
        }

        public static readonly BindableProperty ImageLoadCallbackProperty =
            BindableProperty.Create("ImageLoadCallback", typeof(Func<Task<byte[]>>), typeof(MessageCell), null);

        public Func<Task<byte[]>> ImageLoadCallback
        {
            get { return (Func<Task<byte[]>>)GetValue(ImageLoadCallbackProperty); }
            set { SetValue(ImageLoadCallbackProperty, value); }
        }
    }
}
