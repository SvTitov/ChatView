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
        NativeAndroidCell cell;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            var messageCell = (MessageCell) item;

            cell = cell as NativeAndroidCell;
            if (cell == null)
                cell = new NativeAndroidCell(context, messageCell);
            else
                cell.NativeCell.PropertyChanged -= OnNativeCellPropertyChanged;

            messageCell.PropertyChanged += OnNativeCellPropertyChanged;
            cell.UpdateCell(messageCell);

            return cell;
        }

        private void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }
    }
}
