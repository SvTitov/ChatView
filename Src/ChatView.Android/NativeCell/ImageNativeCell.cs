using System;
using Android.Content;
using Android.Widget;
using ChatView.Shared.Models;
using ChatView.Shared.Views;
using Xamarin.Forms;

namespace ChatView.Droid.NativeCell
{
    public class ImageNativeCell : LinearLayout, INativeElementView
    {
        public ImageNativeCell(Context context, ImageMessageCell nativeCell)
            : base(context)
        {
            NativeCell = nativeCell;
        }

        public ImageMessageCell NativeCell { get; private set; }

        public Element Element => throw new NotImplementedException();

        internal void UpdateCell(ImageMessageCell messageCell)
        {
            
        }
    }
}
