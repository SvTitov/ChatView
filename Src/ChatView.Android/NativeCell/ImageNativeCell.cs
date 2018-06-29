using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using ChatView.Shared.Models;
using ChatView.Shared.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ChatView.Droid.NativeCell
{
    public class ImageNativeCell : LinearLayout, INativeElementView
    {
        public ImageView Image { get; set; }

        private Android.Views.View _mainView;
        private LinearLayout _linearLayout;

        public ImageNativeCell(Context context, ImageMessageCell nativeCell)
            : base(context)
        {
            NativeCell = nativeCell;

            var inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            _mainView = inflater.Inflate(Resource.Layout.ImageCellTemplate, null);
            _linearLayout = _mainView.FindViewById<LinearLayout>(Resource.Id.ll);
            _linearLayout.SetGravity(NativeCell.IsIncoming ? GravityFlags.Right : GravityFlags.Left);

            Image = _mainView.FindViewById<ImageView>(Resource.Id.imageContainer);

            Image.Background = GetRoundedDrawable();

            if (NativeCell.ImageByteArray != null)
            {
                UpdateImage(NativeCell.ImageByteArray);
            }

            AddView(_mainView);
        }

        public ImageMessageCell NativeCell { get; private set; }

        public Element Element => throw new NotImplementedException();

        internal void UpdateCell(ImageMessageCell messageCell)
        {
            NativeCell = messageCell;
        }

        /// <summary>
        /// Get rounded drawable for corner radius
        /// </summary>
        /// <returns></returns>
        private Drawable GetRoundedDrawable()
        {
            //set corner radius
            GradientDrawable shape = new GradientDrawable();
            shape.SetCornerRadius(NativeCell.CornerRadius * 2);
            shape.SetColor(NativeCell.IsIncoming ? NativeCell.IncomingColor.ToAndroid() : NativeCell.OutgoingColor.ToAndroid());

            return shape;
        }

        private Bitmap CreateRoundedBitmap(Bitmap bitmap, int radius)
        {
            var output = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(output);

            var color = 0xff424242;
            Paint paint = new Paint();
            Rect rect = new Rect(0, 0, bitmap.Width, bitmap.Height);
            RectF rectF = new RectF(rect);

            paint.AntiAlias = true;
            canvas.DrawARGB(0, 0, 0, 0);
            paint.Color = Xamarin.Forms.Color.FromUint(color).ToAndroid();
            canvas.DrawRoundRect(rectF, radius, radius, paint);

            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
            canvas.DrawBitmap(bitmap, rect, rect, paint);

            return output;
        }

        public void UpdateImage(byte[] imageData)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(()=> 
            {
                var decoded = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
                var roundedImage = CreateRoundedBitmap(decoded, NativeCell.CornerRadius * 2);

                this.Image.LayoutParameters.Width = roundedImage.Width;
                this.Image.LayoutParameters.Height = roundedImage.Height;

                this.Image.SetImageBitmap(Bitmap.CreateScaledBitmap(roundedImage, roundedImage.Width, roundedImage.Height, false));
            });
        }
    }
}
