using System;
using System.Drawing;
using Akavache;
using ChatView.Shared.Views;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using System.Reactive.Linq;
using Xamarin.Forms.Platform.iOS;
using Splat;
using System.Threading.Tasks;

namespace ChatView.iOS.NativeCells
{
    public class ImageNativeCell : BaseNativeCell<ImageMessageCell>
    {
        private UIView _view;
        private IBitmap _currentImage;
        private UIImage _placeholder;
        static internal int _space = 20;


        public UIImageView ImageView { get; set; }
        public UIActivityIndicatorView ActivityIndicator { get; set;}

        static internal SizeF BubblePadding = new SizeF(16, 16);

        public ImageNativeCell(ImageMessageCell cell, string cellId)
            : base(cellId)
        {
            BlobCache.ApplicationName = "image__cache";

            NativeCell = cell;

            ContentView.BackgroundColor = UIColor.Clear;
            SelectionStyle = UITableViewCellSelectionStyle.None;

            InitView();

            ImageView = new UIImageView();
            ImageView.Layer.MasksToBounds = true;
            ImageView.Layer.CornerRadius = NativeCell.CornerRadius;


            ActivityIndicator = new UIActivityIndicatorView();
            ActivityIndicator.HidesWhenStopped = true;
            ActivityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;


            _view.AddSubview(ImageView);
            _view.AddSubview(ActivityIndicator);
            ContentView.Add(_view);
        }


		public async override void LayoutSubviews()
		{
			base.LayoutSubviews();

            if (_currentImage != null)
                return;

            ActivityIndicator.Center = _view.Center;
            ActivityIndicator.StartAnimating();

            var frame = ContentView.Frame;

            _view.BackgroundColor = NativeCell.IsIncoming ? NativeCell.IncomingColor.ToUIColor() : NativeCell.OutgoingColor.ToUIColor();

            // if we have placeholder
            if (!string.IsNullOrEmpty(NativeCell.Placeholder))
                InitPlaceholder(frame);
		}

		public async override void MovedToSuperview()
		{
            base.MovedToSuperview();

            var frame = ContentView.Frame;

            _currentImage = await BlobCache.LocalMachine.LoadImageFromUrl(NativeCell.ImageUri.AbsoluteUri, true);
            var _resizedImage = ResizeImage(new CGSize(_currentImage.Width, _currentImage.Height), new CGSize(this.Bounds.Width * 0.7,
                                                                                                          this.Bounds.Height * 0.8));

            ImageView.Frame = new CGRect(frame.X,
                                         frame.Y,
                                         _resizedImage.Width,
                                         _resizedImage.Height);

            _view.Frame = new CGRect(frame.X + BubblePadding.Width,
                                         frame.Y,
                                         _resizedImage.Width,
                                     _resizedImage.Height + 20);

            ImageView.Image = _currentImage.ToNative();

            ActivityIndicator.StopAnimating();
		}

		private void InitPlaceholder(CGRect frame)
        {
            // load image
            _placeholder = UIImage.FromFile(NativeCell.Placeholder);
            var imageSize = ResizeImage(_placeholder.Size, new CGSize(this.Bounds.Width * 0.7,    // 0.7 from tableview width
                                                               this.Bounds.Height));

            _view.Frame = new CGRect(frame.X + BubblePadding.Width,
                                 frame.Y,
                                 imageSize.Width,
                                 imageSize.Height + 20); // 20 to info line

            ImageView.Image = _placeholder;
            ImageView.Image = ImageView.Image.Scale(imageSize);

            ImageView.Frame = new CGRect(frame.X,
                                     frame.Y,
                                     imageSize.Width,
                                     imageSize.Height);
        }

        private CGSize ResizeImage(CGSize original, CGSize max)
        {
            float wRatio =  (float)(max.Width / original.Width);
            float hRatio = (float)(max.Height / original.Height);
            float minRatio = Math.Min(wRatio, hRatio);
            if (minRatio > 1)
                return original;

            return new CGSize(original.Width * minRatio, original.Height * minRatio);
        }

		public override float GetHeight(UIView tv)
		{
            var image = BlobCache.LocalMachine.LoadImage(NativeCell.ImageUri.AbsolutePath).GetAwaiter<IBitmap>().GetResult();
            if (image != null)
                return image.Height + _space;
            if (_placeholder != null)
                return (float) _placeholder.Size.Height + 20 + _space;

            return base.GetHeight(tv);
		}

		private void InitView()
        {
            var rect = new RectangleF(0, 0, 1, 1);
            _view = new UIView(rect);

            _view.Layer.BorderColor = UIColor.Black.CGColor;
            _view.Layer.BorderWidth = 0f;
            _view.Layer.CornerRadius = NativeCell.CornerRadius;
        }
    }
}
