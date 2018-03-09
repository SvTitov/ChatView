using System;
using System.Drawing;
using ChatView.Shared;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace ChatView.iOS
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
        public UILabel NameText { get; set; }

        public static float Spacing { get; set; } = 10;
        private static UIFont _uIFontMessage = UIFont.SystemFontOfSize(14);
        private static UIFont _uIFontInfo = UIFont.SystemFontOfSize(10);
        private static UIFont _uIFontName = UIFont.SystemFontOfSize(16);

        #region ctor

        public NativeIOSCell(MessageCell cell, string cellId)
            : base(UITableViewCellStyle.Default, cellId)
        {
            NativeCell = cell;

            ContentView.BackgroundColor = UIColor.Clear;
            SelectionStyle = UITableViewCellSelectionStyle.None;
            InitView();

            cell.PropertyChanged -= OnPropertyChangedEventHandler;
            cell.PropertyChanged += OnPropertyChangedEventHandler;

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
                Font = _uIFontInfo
            };

            StatusText = new UILabel()
            {
                TextColor = UIColor.Black,
                Lines = 1,
                Font = _uIFontInfo
            };

            NameText = new UILabel()
            {
                TextColor = UIColor.Blue,
                Lines = 1,
                Font = _uIFontName
            };

            _view.AddSubview(StatusText);
            _view.AddSubview(NameText);
            _view.AddSubview(MessageText);
            _view.AddSubview(DateText);
            ContentView.Add(_view);
        }

        #endregion

        #region overrided
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _view.BackgroundColor = NativeCell.IsIncoming ? UIColor.FromRGB(66, 165, 245) : UIColor.FromRGB(0, 230, 118);

            var frame = ContentView.Frame;
            var sizeForMessage = GetSizeForText(this, MessageText.Text, _uIFontMessage) + BubblePadding;
            var sizeForDate = GetSizeForText(this, DateText.Text, _uIFontInfo) + BubblePadding;

            _view.SetNeedsDisplay();

            var noBubbleSize = sizeForMessage - BubblePadding;
            var bubbleSize = sizeForMessage + BubblePadding;

            if (NativeCell.IsIncoming)
            {
                int nameHeight = (string.IsNullOrWhiteSpace(NameText.Text) ? 0 : 20);

                float bubleWidth = bubbleSize.Width;
                if (sizeForDate.Width > bubbleSize.Width)
                    bubleWidth = sizeForDate.Width + BubblePadding.Width;

                _view.Frame = new CGRect(frame.X + BubblePadding.Width, frame.Y, bubleWidth, bubbleSize.Height + nameHeight);

                if (!string.IsNullOrWhiteSpace(NameText.Text))
                    NameText.Frame = new CGRect(frame.X + BubblePadding.Width, frame.Y + 5, bubleWidth, 20);

                MessageText.Frame = new CGRect(frame.X + BubblePadding.Width, frame.Y + 6 + nameHeight, noBubbleSize.Width, noBubbleSize.Height);
                DateText.Frame = new CGRect(bubleWidth - sizeForDate.Width,
                                            bubbleSize.Height - sizeForDate.Height + nameHeight,
                                            sizeForDate.Width,
                                            sizeForDate.Height);
            }
            else
            {
                var sizeForStatus = GetSizeForText(this, StatusText.Text, _uIFontInfo) + BubblePadding;
                var infoLineSize = sizeForStatus + sizeForDate;

                float bubleWidth = bubbleSize.Width;
                if (infoLineSize.Width > bubbleSize.Width)
                    bubleWidth = infoLineSize.Width;

                _view.Frame = new CGRect(frame.GetMaxX() - bubleWidth - BubblePadding.Width, frame.Y, bubleWidth, bubbleSize.Height);
                MessageText.Frame = new CGRect(frame.X + BubblePadding.Width, frame.Y + 6, noBubbleSize.Width, noBubbleSize.Height);

                DateText.Frame = new CGRect(bubleWidth - sizeForDate.Width,
                                            bubbleSize.Height - sizeForDate.Height,
                                            sizeForDate.Width,
                                            sizeForDate.Height);
                StatusText.Frame = new CGRect(frame.X + BubblePadding.Width,
                                              bubbleSize.Height - sizeForStatus.Height,
                                              sizeForStatus.Width,
                                              sizeForStatus.Height);
            }
        }
        #endregion

        private void InitView()
        {
            var rect = new RectangleF(0, 0, 1, 1);
            _view = new UIView(rect);

            _view.Layer.BorderColor = UIColor.Black.CGColor;
            _view.Layer.BorderWidth = 0f;
            _view.Layer.CornerRadius = 10;
        }

        void OnPropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            bool needUpdate = false;
            if (MessageCell.StatusProperty.PropertyName == e.PropertyName)
            {
                StatusText.Text = GetStatusString((sender as MessageCell).Status);
                needUpdate = true;
            }
            if (MessageCell.MessageBodyProperty.PropertyName == e.PropertyName)
            {
                this.MessageText.Text = (sender as MessageCell).MessageBody;
                needUpdate = true;
            }

            if (needUpdate)
            {
                SetNeedsLayout();
            }
        }

        public void UpdateCell(MessageCell cell)
        {
            MessageText.Text = cell.MessageBody;
            DateText.Text = cell.Date;
            StatusText.Text = GetStatusString(cell.Status);
            NameText.Text = cell.Name;

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
            var size = s.StringSize(font, new CGSize(tv.Bounds.Width * .7f - 10 - 22, tv.Bounds.Height), UILineBreakMode.WordWrap);
            return new SizeF((float)size.Width, (float)size.Height);
        }

        public float GetHeight(UIView tv)
        {
            return GetSizeForText(tv, this.MessageText.Text, _uIFontMessage).Height
                                        + BubblePadding.Height
                                        + GetSizeForText(tv, this.DateText.Text, _uIFontInfo).Height
                                        + Spacing
                                        + (string.IsNullOrWhiteSpace(NameText.Text) ? 0 : 20);
        }
    }
}
