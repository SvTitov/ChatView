using System;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace ChatView.Shared.Views
{
    public class MessageListView : ListView
    {
        #region Bindable

        public static readonly BindableProperty IsScrollProperty = BindableProperty.Create(
            nameof(IsScroll),
            typeof(bool),
            typeof(MessageListView),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: false,
            propertyChanged: IsScrollChanged);

        public bool IsScroll
        {
            get => (bool)GetValue(IsScrollProperty);
            set => SetValue(IsScrollProperty, value);
        }

        private static void IsScrollChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MessageListView)bindable;
            control.IsScroll = (bool)newValue;
        }

        #endregion

        public MessageListView()
        {
            //ItemTemplate = new DataTemplate(() => 
            //{
            //    //var cell = new MessageCell();
            //    //cell.SetBinding(MessageCell.MessageBodyProperty, "Message");
            //    //cell.SetBinding(MessageCell.DateProperty, "Date");
            //    //cell.SetBinding(MessageCell.IsIncomingProperty, "IsIncoming");
            //    //cell.SetBinding(MessageCell.NameProperty, "Name");
            //    //cell.SetBinding(MessageCell.StatusProperty, "Status");

            //    //return cell;
            //});

            ItemTemplate = new CellDataTemplateSelector
            {
                ImageCellTemplate = new DataTemplate(() =>
                {
                    ImageMessageCell cell = new ImageMessageCell();
                    cell.SetBinding(ImageMessageCell.ImageUriProperty, "ImageUri");
                    cell.SetBinding(ImageMessageCell.ImageByteArrayProperty, "ImageByteArray");
                    cell.SetBinding(ImageMessageCell.ImageLoadCallbackProperty, "ImageLoadCallback");
                    cell.SetBinding(UserMessageCell.CornerRadiusProperty, "CornerRadius");
                    cell.SetBinding(ImageMessageCell.PlaceholderProperty, "Placeholder");

                    return cell;
                }),
                TextCellTemplate = new DataTemplate(() =>
                {
                    var cell = new TextMessageCell();
                    cell.SetBinding(TextMessageCell.MessageBodyProperty, "Message");
                    cell.SetBinding(UserMessageCell.DateProperty, "Date");
                    cell.SetBinding(UserMessageCell.IsIncomingProperty, "IsIncoming");
                    cell.SetBinding(UserMessageCell.NameProperty, "Name");
                    cell.SetBinding(UserMessageCell.StatusProperty, "Status");

                    return cell;
                })
            };


            this.SeparatorVisibility = SeparatorVisibility.None;
        }

        public event EventHandler<object> OnLongClick;

        public void InvokeLongClick(object cell)
        {
            OnLongClick?.Invoke(this, cell);
        }
    }
}
