using System;
using System.ComponentModel;
using ChatView.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ChatView.Droid.NativeCells;
using ChatView.Shared.Views;

[assembly: ExportRenderer(typeof(TextMessageCell), typeof(ChatView.Droid.Cells.TextMessageCellRenderer))]
namespace ChatView.Droid.Cells
{
    public class TextMessageCellRenderer : ViewCellRenderer
    {
        TextNativeCell _cell;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            var messageCell = (TextMessageCell) item;

            _cell = _cell as TextNativeCell;
            if (_cell == null)
                _cell = new TextNativeCell(context, messageCell);
            else
                _cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

            messageCell.PropertyChanged += OnNativeCellPropertyChanged;
            _cell.UpdateCell(messageCell);

            return _cell;
        }

        private void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var messageCell = (TextMessageCell)sender;
            if (e.PropertyName == TextMessageCell.MessageBodyProperty.PropertyName)
                _cell.MessageText.Text = messageCell.MessageBody;
            if (e.PropertyName == TextMessageCell.DateProperty.PropertyName)
                _cell.DateText.Text = messageCell.Date;
            if (e.PropertyName == TextMessageCell.NameProperty.PropertyName)
                _cell.NameText.Text = messageCell.Name;
            if (e.PropertyName == TextMessageCell.StatusProperty.PropertyName)
                _cell.StatusText.Text = StatusHelper.GetStatusString(messageCell.Status);
        }
    }
}
