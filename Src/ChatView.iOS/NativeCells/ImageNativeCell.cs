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
using Foundation;

namespace ChatView.iOS.NativeCells
{
    public class ImageNativeCell : BaseNativeCell<ImageMessageCell>
    {
        private UIView _view;
        private UIImage _placeholder;
        static internal int _space = 20;


        public UIImageView ImageView { get; set; }
        public UIActivityIndicatorView ActivityIndicator { get; set;}
        private CGRect _parentSize;

        static internal SizeF BubblePadding = new SizeF(16, 16);
        private UIImage _image;
        private CGSize _resizedImage;

        public ImageNativeCell(ImageMessageCell cell, string cellId, CGRect parentSize)
            : base(cellId)
        {
            cell.PropertyChanged -= OnPropertyChangedEventHandler;
            cell.PropertyChanged += OnPropertyChangedEventHandler;

            NativeCell = cell;

            _parentSize = parentSize;

            ContentView.BackgroundColor = UIColor.Clear;
            SelectionStyle = UITableViewCellSelectionStyle.None;

            InitView();

            ImageView = new UIImageView();
            ImageView.Layer.MasksToBounds = true;
            ImageView.Layer.CornerRadius = NativeCell.CornerRadius;


            ActivityIndicator = new UIActivityIndicatorView();
            ActivityIndicator.HidesWhenStopped = true;
            ActivityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;

            _view.BackgroundColor = NativeCell.IsIncoming ? NativeCell.IncomingColor.ToUIColor() : NativeCell.OutgoingColor.ToUIColor();

            _view.AddSubview(ImageView);
            _view.AddSubview(ActivityIndicator);
            ContentView.Add(_view);
        }


		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
            var frame = ContentView.Frame;

            if (NativeCell.ImageByteArray == null)
            {
                ActivityIndicator.Center = _view.Center;
                ActivityIndicator.StartAnimating();

                // if we have placeholder
                // TODO set empty bubble if placeholder is null
                if (!string.IsNullOrEmpty(NativeCell.Placeholder))
                    InitPlaceholder(frame);
            }
            else
            {
                var data = NSData.FromArray(NativeCell.ImageByteArray);
                _image = UIImage.LoadFromData(data);

                _resizedImage = ResizeImage(new CGSize(_image.Size.Width, _image.Size.Height), new CGSize(this.Bounds.Width * 0.7,
                                                                                                            _parentSize.Height * 0.8));

                ImageView.Frame = new CGRect(frame.X,
                                             frame.Y,
                                             _resizedImage.Width,
                                             _resizedImage.Height);

                _view.Frame = new CGRect(frame.X + BubblePadding.Width,
                                             frame.Y,
                                             _resizedImage.Width,
                                         _resizedImage.Height + 20);

                ImageView.Image = _image;

                ActivityIndicator.StopAnimating();
            }
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
            if (_image != null)
                return (float) _resizedImage.Height + 20 + _space;
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

        void OnPropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            bool needUpdate = false;
            if (ImageMessageCell.ImageByteArrayProperty.PropertyName == e.PropertyName)
            {
                needUpdate = true;
            }

            if (needUpdate)
            {
                InvokeOnMainThread(() =>
                {
                    try
                    {
                        SetNeedsLayout();
                        LayoutIfNeeded();
                        var tv = (this.Superview as UITableView);
                        if (tv != null)
                        {
                            var path = tv.IndexPathForCell(this);
                            if (path != null)
                            {
                                //tv.BeginUpdates();
                                tv.ReloadRows(new[] { path }, UITableViewRowAnimation.None);
                                //tv.EndUpdates();
                            }
                        }
                    }
                    catch 
                    {   }
                });
            }
        }
    }
}
