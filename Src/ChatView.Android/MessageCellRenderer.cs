using System;
using System.ComponentModel;
using ChatView.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MessageCell), typeof(ChatView.Droid.MessageCellRenderer))]
namespace ChatView.Droid
{
    public class MessageCellRenderer : ViewCellRenderer
    {
        NativeAndroidCell _cell;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            var messageCell = (MessageCell) item;

            _cell = _cell as NativeAndroidCell;
            if (_cell == null)
                _cell = new NativeAndroidCell(context, messageCell);
            else
                _cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

            messageCell.PropertyChanged += OnNativeCellPropertyChanged;
            _cell.UpdateCell(messageCell);

            return _cell;
        }

        private void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var messageCell = (MessageCell)sender;
            if (e.PropertyName == MessageCell.MessageBodyProperty.PropertyName)
                _cell.MessageText.Text = messageCell.MessageBody;
            if (e.PropertyName == MessageCell.DateProperty.PropertyName)
                _cell.DateText.Text = messageCell.Date;
            if (e.PropertyName == MessageCell.NameProperty.PropertyName)
                _cell.NameText.Text = messageCell.Name;
            if (e.PropertyName == MessageCell.StatusProperty.PropertyName)
                _cell.StatusText.Text = StatusHelper.GetStatusString(messageCell.Status);
        }
    }
}
