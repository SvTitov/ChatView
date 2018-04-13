using System;
using System.Drawing;
using ChatView.Shared;
using ChatView.Shared.Views;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ChatView.iOS.NativeCells
{
    internal class TextNativeCell : BaseNativeCell<TextMessageCell>
    {
        private UIView _view;

        static internal SizeF BubblePadding = new SizeF(16, 16);

        public UILabel MessageText { get; set; }
        public UILabel DateText { get; set; }
        public UILabel StatusText { get; set; }
        public UILabel NameText { get; set; }

        public static float Spacing { get; set; } = 10;

        #region ctor

        public TextNativeCell(TextMessageCell cell, string cellId)
            : base(cellId)
        {
            NativeCell = cell;

            ContentView.BackgroundColor = UIColor.Clear;
            SelectionStyle = UITableViewCellSelectionStyle.None;
            InitView();

            cell.PropertyChanged -= OnPropertyChangedEventHandler;
            cell.PropertyChanged += OnPropertyChangedEventHandler;

            MessageText = new UILabel()
            {
                TextColor = NativeCell.TextFontColor.ToUIColor(),
                Lines = 0,
                Font = UIFont.SystemFontOfSize(NativeCell.TextFontSize),
                LineBreakMode = UILineBreakMode.WordWrap,
                BackgroundColor = UIColor.Clear
            };

            DateText = new UILabel()
            {
                TextColor = NativeCell.InfoFontColor.ToUIColor(),
                Lines = 1,
                Font = UIFont.SystemFontOfSize(NativeCell.InfoFontSize)
            };

            StatusText = new UILabel()
            {
                TextColor = NativeCell.InfoFontColor.ToUIColor(),
                Lines = 1,
                Font = UIFont.SystemFontOfSize(NativeCell.InfoFontSize)
            };

            NameText = new UILabel()
            {
                TextColor = NativeCell.NameFontColor.ToUIColor(),
                Lines = 1,
                Font = UIFont.SystemFontOfSize(NativeCell.NameFontSize)
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

            _view.BackgroundColor = NativeCell.IsIncoming ? NativeCell.IncomingColor.ToUIColor() : NativeCell.OutgoingColor.ToUIColor();

            var frame = ContentView.Frame;
            var sizeForMessage = GetSizeForText(this, MessageText.Text, UIFont.SystemFontOfSize(NativeCell.TextFontSize)) + BubblePadding;
            var sizeForDate = GetSizeForText(this, DateText.Text, UIFont.SystemFontOfSize(NativeCell.InfoFontSize)) + BubblePadding;

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
                var sizeForStatus = GetSizeForText(this, StatusText.Text, UIFont.SystemFontOfSize(NativeCell.InfoFontSize)) + BubblePadding;
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
            _view.Layer.CornerRadius = NativeCell.CornerRadius;
        }

        void OnPropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            bool needUpdate = false;
            if (UserMessageCell.StatusProperty.PropertyName == e.PropertyName)
            {
                StatusText.Text = StatusHelper.GetStatusString((sender as TextMessageCell).Status);
                needUpdate = true;
            }
            if (TextMessageCell.MessageBodyProperty.PropertyName == e.PropertyName)
            {
                this.MessageText.Text = (sender as TextMessageCell).MessageBody;
                needUpdate = true;
            }

            if (needUpdate)
            {
                SetNeedsLayout();
            }
        }

        public override void UpdateCell(TextMessageCell cell)
        {
            MessageText.Text = cell.MessageBody;
            DateText.Text = cell.Date;
            StatusText.Text = StatusHelper.GetStatusString(cell.Status);
            NameText.Text = cell.Name;

            NativeCell = cell;
            SetNeedsLayout();
        }


        public override float GetHeight(UIView tv)
        {
            var cellSize = GetSizeForText(tv, this.NativeCell.MessageBody, UIFont.SystemFontOfSize(NativeCell.TextFontSize))
                    + BubblePadding
                    + GetSizeForText(tv, this.NativeCell.Date, UIFont.SystemFontOfSize(NativeCell.InfoFontSize));

            return cellSize.Height + Spacing + (NativeCell.IsIncoming ? 20 : 0);
        }
    }
}
