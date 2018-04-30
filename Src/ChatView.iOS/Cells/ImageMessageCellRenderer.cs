using System;
using System.ComponentModel;
using System.Threading.Tasks;
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
                _cell = new ImageNativeCell(messageCell, item.GetType().FullName, tv.Bounds);
            else
            {
                _cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;
                _cell.RemoveFromSuperview();
            }

            messageCell.PropertyChanged += OnNativeCellPropertyChanged;
            _cell.UpdateCell(messageCell);

            _cell.ContentView.Transform = CoreGraphics.CGAffineTransform.MakeScale(1f, -1f);

            // TODO cancel task
            if (messageCell.ImageLoadCallback != null)
            {
                Task.Run(async () =>
                {
                    if (messageCell.ImageByteArray == null)
                    {
                        var result = await messageCell.ImageLoadCallback();
                        messageCell.ImageByteArray = result;
                    }
                });
            }

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
