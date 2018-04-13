using System;
using System.ComponentModel;
using ChatView.iOS.NativeCells;
using ChatView.Shared;
using ChatView.Shared.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TextMessageCell), typeof(ChatView.iOS.Cells.TextMessageCellRenderer))]
namespace ChatView.iOS.Cells
{
    public class TextMessageCellRenderer : ViewCellRenderer
    {
        private TextNativeCell _cell;

        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var messageCell = (TextMessageCell)item;

            _cell = reusableCell as TextNativeCell;
            if (_cell == null)
                _cell = new TextNativeCell(messageCell, item.GetType().FullName);
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
            var messageCell = (TextMessageCell)sender;
            if (e.PropertyName == TextMessageCell.MessageBodyProperty.PropertyName)
            {
                _cell.MessageText.Text = messageCell.MessageBody;
            }
        }
    }
}
