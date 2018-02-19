using System;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace ChatView.Shared
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
            get 
            {
                return (bool)GetValue(IsScrollProperty);
            }
            set 
            {
                SetValue(IsScrollProperty, value);
            }
        }



        private static void IsScrollChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MessageListView)bindable;
            control.IsScroll = (bool) newValue;
        }

#endregion

        public MessageListView()
        {
            ItemTemplate = new DataTemplate(() => 
            {
                var cell = new MessageCell();
                cell.SetBinding(MessageCell.MessageBodyProperty, "Message");
                cell.SetBinding(MessageCell.DateProperty, "Date");
                cell.SetBinding(MessageCell.IsIncomingProperty, "IsIncoming");

                return cell;
            });

            this.SeparatorVisibility = SeparatorVisibility.None;
        }

        protected override Cell CreateDefault(object item)
        {
            return base.CreateDefault(item);
        }


        protected override void SetupContent(Cell content, int index)
        {
            base.SetupContent(content, index);

            //var dd = content.GetType().GetField  ("DefaultCellHeight", BindingFlags.Public
            //                                 | BindingFlags.Static 
            //                                 | BindingFlags.FlattenHierarchy);
			//
            //var value = (int) dd.GetValue(dd);
			//
            //if (this.HeightRequest < _defaultSize.Item2 && this.HeightRequest < this.HeightRequest + value)
            //{
            //    if (this.HeightRequest < 0)
            //        this.HeightRequest = 0;
			//
            //    var hh = content.Height;
            //    var hhh = content.RenderHeight;
			//
            //    this.HeightRequest += value;
            //    this.InvalidateMeasure();
            //}
        }

    }
}
