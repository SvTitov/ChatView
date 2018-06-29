using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using ChatView.Shared.Models;
using ChatView.Shared.Views;
using Xamarin.Forms;

namespace ChatView.Droid.NativeCell
{
    public class ImageNativeCell : LinearLayout, INativeElementView
    {
        public ImageView Image { get; set; }

        public ImageNativeCell(Context context, ImageMessageCell nativeCell)
            : base(context)
        {
            NativeCell = nativeCell;

            var inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.ImageCellTemplate, null);
            Image = view.FindViewById<ImageView>(Resource.Id.imageContainer);

            if (NativeCell.ImageByteArray != null)
            {
                UpdateImage(NativeCell.ImageByteArray);
            }

            AddView(view);
        }

        public ImageMessageCell NativeCell { get; private set; }

        public Element Element => throw new NotImplementedException();

        internal void UpdateCell(ImageMessageCell messageCell)
        {
            
        }

        public void UpdateImage(byte[] imageData)
        {
            var decoded = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Image.SetImageBitmap(Bitmap.CreateScaledBitmap(decoded, Image.Width, Image.Height, false));
            Image.RefreshDrawableState();
            Image.Invalidate();
        }
    }
}
