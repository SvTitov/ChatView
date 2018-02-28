using System;
using ChatView;
using ChatView.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MessageCell), typeof(MessageCellRenderer))]
namespace ChatView
{
    public class MessageCellRenderer : ViewCellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {



            throw new NotImplementedException();
        }
    }
}
