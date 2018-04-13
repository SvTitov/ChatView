using System;
using System.ComponentModel;
using ChatView.iOS.NativeCells;
using ChatView.Shared.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageMessageCell), typeof(ChatView.iOS.Cells.ImageMessageCellRenderer))]
namespace ChatView.iOS.Cells
{
    public class ImageMessageCellRenderer : ViewCellRenderer
    {
        private ImageNativeCell _cell;

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
            var messageCell = (ImageMessageCell)item;

            _cell = reusableCell as ImageNativeCell;
            if (_cell == null)
                _cell = new ImageNativeCell(messageCell, item.GetType().FullName);
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
            //var messageCell = (ImageMessageCell)sender;
            //if (e.PropertyName == ImageMessageCell.ImageUriProperty.PropertyName)
            //{
            //    _cell.MessageText.Text = messageCell.MessageBody;
            //}
        }
	}
}
