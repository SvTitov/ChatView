using System;
using System.Drawing;
using ChatView.Shared;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace ChatViewTest.iOS.Renderers
{
    internal class NativeIOSCell : UITableViewCell, INativeElementView
    {
        public Element Element => NativeCell;
        public MessageCell NativeCell { get; set; }
        private UIView _view;

        static internal SizeF BubblePadding = new SizeF(16, 16);

        public UILabel MessageText { get; set; }
        public UILabel DateText { get; set; }
        public UILabel StatusText { get; set; }

        public static float Spacing { get; set; } = 10;
        private static UIFont _uIFontMessage = UIFont.SystemFontOfSize(14);
        private static UIFont _uIInfoDate = UIFont.SystemFontOfSize(10);



        public NativeIOSCell(MessageCell cell, string cellId )
            : base(UITableViewCellStyle.Default, cellId)
        {
            NativeCell = cell;

            ContentView.BackgroundColor = UIColor.Clear;
            SelectionStyle = UITableViewCellSelectionStyle.None;
            InitView();

            MessageText = new UILabel()
            {
                TextColor = UIColor.Black,
                Lines = 0,
                Font = _uIFontMessage,
                LineBreakMode = UILineBreakMode.WordWrap,
                BackgroundColor = UIColor.Clear
            };

            DateText = new UILabel()
            {
                TextColor = UIColor.Black,
                Lines = 1,
                Font = _uIInfoDate
            };

            StatusText = new UILabel()
            {
                TextColor = UIColor.Black,
                Lines = 1,
                Font = _uIInfoDate
            };


            _view.AddSubview(MessageText);
            _view.AddSubview(DateText);
            ContentView.Add(_view);
        }

        private void InitView()
        {
            var rect = new RectangleF(0, 0, 1, 1);
            _view = new UIView(rect);

            _view.Layer.BorderColor = UIColor.Black.CGColor;
            _view.Layer.BorderWidth = 0f;
            _view.Layer.CornerRadius = 10;
        }

        public void UpdateCell(MessageCell cell)
        {
            MessageText.Text = cell.MessageBody;
            DateText.Text = cell.Date;
            StatusText.Text = GetStatusString(cell.Status);

            NativeCell = cell;
            SetNeedsLayout();
        }

        private string GetStatusString(MessageStatuses status)
        {
            string stat = string.Empty;
            if (status == MessageStatuses.Sent)
                stat = "✓";
            else if (status == MessageStatuses.Delivered)
                stat = "✓✓";
            return stat;
        }

        static internal SizeF GetSizeForText(UIView tv, string text, UIFont font)
        {
            NSString s = new NSString(text);
            var size = s.StringSize(font, new CGSize(tv.Bounds.Width * .7f - 10 - 22 , tv.Bounds.Height), UILineBreakMode.WordWrap);
            return new SizeF((float)size.Width, (float)size.Height);
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _view.BackgroundColor = NativeCell.IsIncoming ? UIColor.FromRGB(66, 165, 245) : UIColor.FromRGB(0, 230, 118);

            var frame = ContentView.Frame;
            var sizeForMessage = GetSizeForText(this, MessageText.Text, _uIFontMessage) + BubblePadding;
            var sizeForDate = GetSizeForText(this, DateText.Text, _uIInfoDate) + BubblePadding;

            _view.SetNeedsDisplay();

            var noBubbleSize = sizeForMessage - BubblePadding;
            var bubbleSize = sizeForMessage + BubblePadding;

            if (NativeCell.IsIncoming)
            {
                _view.Frame = new CGRect(frame.X + 12, frame.Y, bubbleSize.Width, bubbleSize.Height);
                MessageText.Frame = new CGRect(frame.X + 12, frame.Y + 6, noBubbleSize.Width, noBubbleSize.Height);
                DateText.Frame = new CGRect(bubbleSize.Width - sizeForDate.Width,
                                            bubbleSize.Height - sizeForDate.Height,
                                            sizeForDate.Width,
                                            sizeForDate.Height);
            }
            else
            {
                _view.Frame = new CGRect(frame.GetMaxX() - bubbleSize.Width - 12, frame.Y, bubbleSize.Width, bubbleSize.Height);
                MessageText.Frame = new CGRect(frame.X + 12, frame.Y + 6, noBubbleSize.Width, noBubbleSize.Height);

                DateText.Frame = new CGRect(bubbleSize.Width - sizeForDate.Width,
                                            bubbleSize.Height - sizeForDate.Height,
                                            sizeForDate.Width,
                                            sizeForDate.Height);
            }
        }

        public float GetHeight(UIView tv)
        {
            return GetSizeForText(tv, this.MessageText.Text, _uIFontMessage).Height + 
                   BubblePadding.Height + 
                   GetSizeForText(tv, this.DateText.Text, _uIInfoDate).Height + Spacing;
        }
    }
}
