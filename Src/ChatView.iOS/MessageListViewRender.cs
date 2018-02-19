using System;
using ChatView;
using ChatView.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MessageListView), typeof(MessageListViewRender))]
namespace ChatView
{
    public class MessageListViewRender : ListViewRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (Control != null)
            {
                Control.ScrollEnabled = false;
            }
        }
    }
}
