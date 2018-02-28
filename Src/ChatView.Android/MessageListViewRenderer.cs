using System;
using Android.Content;
using ChatView;
using ChatView.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MessageListView), typeof(MessageListViewRenderer))]
namespace ChatView
{
    public class MessageListViewRenderer : ListViewRenderer
    {
        Context _context;

        public MessageListViewRenderer(Context context)
            : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                Control.StackFromBottom = true;
            }
        }
    }
}
