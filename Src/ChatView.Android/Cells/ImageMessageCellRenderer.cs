using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;
using ChatView.Droid.NativeCell;
using ChatView.Shared.Models;
using ChatView.Shared.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageMessageCell), typeof(ChatView.Droid.Cells.ImageMessageCellRenderer))]
namespace ChatView.Droid.Cells
{
    public class ImageMessageCellRenderer : ViewCellRenderer
    {

        ImageNativeCell _cell;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var messageCell = (ImageMessageCell)item;

            _cell = _cell as ImageNativeCell;
            if (_cell == null)
                _cell = new ImageNativeCell(context, messageCell);
            else
                _cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

            messageCell.PropertyChanged += OnNativeCellPropertyChanged;
            _cell.UpdateCell(messageCell);

            return _cell;
        }

        private void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //var messageCell = (TextMessageCell)sender;
            //if (e.PropertyName == TextMessageCell.MessageBodyProperty.PropertyName)
                //_cell.MessageText.Text = messageCell.MessageBody;
        }
    }
}
