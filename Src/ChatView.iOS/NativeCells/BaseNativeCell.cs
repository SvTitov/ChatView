using System;
using System.Drawing;
using ChatView.Shared.Views;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace ChatView.iOS.NativeCells
{
    public abstract class BaseNativeCell<T> : UITableViewCell, INativeElementView 
        where T : MessageCell
    {
        public Element Element => NativeCell;

        public T NativeCell { get; set; }

        public BaseNativeCell(string cellId)
            : base(UITableViewCellStyle.Default, cellId)
        {   }

        public virtual void UpdateCell(T cell)
        {   }

        public virtual float GetHeight(UIView tv)
        {
            // default value
            return 55;
        }

        static internal SizeF GetSizeForText(UIView tv, string text, UIFont font)
        {
            NSString s = new NSString(text);
            var size = s.StringSize(font, new CGSize(tv.Bounds.Width * .7f /*- 10 - 22*/, tv.Bounds.Height), UILineBreakMode.WordWrap);
            return new SizeF((float)size.Width, (float)size.Height);
        }
    }
}
