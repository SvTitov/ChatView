using System;
using Android.Content;
using Android.Widget;
using ChatView.Shared;
using Xamarin.Forms;

namespace ChatView
{
    public class NativeAndroidCell : LinearLayout, INativeElementView
    {
        public Element Element => NativeCell;
        public MessageCell NativeCell { get; private set; }

        public NativeAndroidCell(Context context, MessageCell cell)
            : base(context)
        {
            NativeCell = cell;

        }

    }
}
