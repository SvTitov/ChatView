using System;
using Xamarin.Forms;
using ChatView;
using ChatView.Shared;
using Xamarin.Forms.Platform.iOS;
using ChatViewTest.iOS;
using System.Threading.Tasks;
using ChatViewTest.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Foundation;
using System.Reflection;

[assembly: ExportRenderer(typeof(MessageListView), typeof(CustomListView))]
namespace ChatViewTest.iOS.Renderers
{
    public class CustomListView : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            this.Element.PropertyChanged += OnElementPropertyChanged;

            if (Control != null)
            {
                Control.Transform = CoreGraphics.CGAffineTransform.MakeScale(1f, -1f);
                Control.RowHeight = UITableView.AutomaticDimension;
                Control.Source = new ListViewDataSourceWrapper(this.GetFieldValue<UITableViewSource>(typeof(ListViewRenderer), "_dataSource"));
                Control.BackgroundColor = UIColor.Clear;

                var longPressGesture = new UILongPressGestureRecognizer(LongPressMethod);
                Control.AddGestureRecognizer(longPressGesture);

            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.Element.PropertyChanged -= OnElementPropertyChanged;
        }


        void LongPressMethod(UILongPressGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.State == UIGestureRecognizerState.Began)
            {
                var indexPath = Control.IndexPathForRowAtPoint(gestureRecognizer.LocationInView(Control));
                if (indexPath != null)
                {
                    var cell = Control.CellAt(indexPath);

                    var selectCategory = new UIActionSheet( ((NativeIOSCell)cell).NativeCell.MessageBody.Substring(0, 5) , null, "KEK", "LOL");
                    selectCategory.ShowInView(this);
                }
            }
        }



        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;

            if (MessageListView.IsScrollProperty.PropertyName == e.PropertyName)
            {
                Control.ScrollEnabled = (Element as MessageListView).IsScroll;
            }
        }
    }


    public static class PrivateExtensions
    {
        public static T GetFieldValue<T>(this object @this, Type type, string name)
        {
            var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
            return (T)field.GetValue(@this);
        }

        public static T GetPropertyValue<T>(this object @this, Type type, string name)
        {
            var property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            return (T)property.GetValue(@this);
        }
    } 
}
