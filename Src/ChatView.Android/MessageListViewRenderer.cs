using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using ChatView;
using ChatView.Shared;
using ChatView.Shared.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MessageListView), typeof(ChatView.Droid.MessageListViewRenderer))]
namespace ChatView.Droid
{
    public class MessageListViewRenderer : ListViewRenderer
    {
        public static void Initialize() { }

        Context _context;

        public MessageListViewRenderer(Context context)
            : base(context)
        {
            _context = context;
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                Control.StackFromBottom = true;
                Control.TranscriptMode = Android.Widget.TranscriptMode.AlwaysScroll;
                Control.ItemLongClick += OnLongClick;
            }
		}
        private void OnLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var item = Control.GetItemAtPosition(e.Position);
            var value = item.GetType().GetProperty("Instance").GetValue(item, null);
            (Element as MessageListView).InvokeLongClick(value);
        }

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
            Control.ItemLongClick -= OnLongClick;
		}
	}
}
