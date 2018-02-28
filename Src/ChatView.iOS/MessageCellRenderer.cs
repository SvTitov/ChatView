using System;
using System.ComponentModel;
using ChatView;
using ChatView.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MessageCell), typeof(MessageCellRenderer))]
namespace ChatView
{
    public class MessageCellRenderer : ViewCellRenderer
    {
        private NativeIOSCell _cell;

        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var messageCell = (MessageCell)item;

            _cell = reusableCell as NativeIOSCell;
            if (_cell == null)
                _cell = new NativeIOSCell(messageCell, item.GetType().FullName);
            else
            {
                _cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;
                _cell.RemoveFromSuperview();
            }

            messageCell.PropertyChanged += OnNativeCellPropertyChanged;
            _cell.UpdateCell(messageCell);

            _cell.ContentView.Transform = CoreGraphics.CGAffineTransform.MakeScale(1f, -1f);


            return _cell;
        }


        private void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var messageCell = (MessageCell)sender;
            if (e.PropertyName == MessageCell.MessageBodyProperty.PropertyName)
            {
                _cell.MessageText.Text = messageCell.MessageBody;
            }
        }
    }
}
