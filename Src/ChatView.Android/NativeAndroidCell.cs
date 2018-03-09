﻿using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using ChatView.Shared;
using Xamarin.Forms;

namespace ChatView.Droid
{
    public class NativeAndroidCell : LinearLayout, INativeElementView
    {
        public Element Element => NativeCell;
        public MessageCell NativeCell { get; private set; }

        public TextView MessageText { get; set; }
        public TextView DateText { get; set; }
        public TextView StatusText { get; set; }
        public TextView NameText { get; set; }

        private const int Message_Text_Id = 100;
        private const int Date_Text_Id = 101;
        private const int Status_Text_Id = 102;
        private const int Name_Text_Id = 103;

        public NativeAndroidCell(Context context, MessageCell cell)
            : base(context)
        {
            NativeCell = cell;

            var view = CreateLayout(context);
            AddView(view);
        }

        private Android.Views.View CreateLayout(Context context)
        {
            Android.Widget.RelativeLayout layout = new Android.Widget.RelativeLayout(context);
            layout.SetPadding(30,15,30,15);

            //main layout
            Android.Widget.RelativeLayout mainView = new Android.Widget.RelativeLayout(context);
            var paramLayout = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            paramLayout.AddRule(NativeCell.IsIncoming ? LayoutRules.AlignParentLeft : LayoutRules.AlignParentRight);
            paramLayout.SetMargins(NativeCell.IsIncoming ? 0 : 50, 0, NativeCell.IsIncoming ? 50 : 0, 0);
            mainView.SetPadding(20, 20, 20, 20);
            mainView.LayoutParameters = paramLayout;

            //set drawable
            GradientDrawable shape = new GradientDrawable();
            shape.SetCornerRadius(20);
            shape.SetColor(NativeCell.IsIncoming ? Android.Graphics.Color.Rgb(66, 165, 245) : Android.Graphics.Color.Rgb(0, 230, 118));
            mainView.Background = shape;



            //message text
            MessageText = new TextView(context);
            MessageText.SetTextColor(Android.Graphics.Color.Black);
            MessageText.Text = NativeCell.MessageBody;
            MessageText.Id = Message_Text_Id;
            var paramMessageText = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            paramMessageText.AddRule(LayoutRules.Below, Name_Text_Id);
            MessageText.LayoutParameters = paramMessageText;


            // date text
            DateText = new TextView(context);
            DateText.SetTextColor(Android.Graphics.Color.Black);
            DateText.Text = NativeCell.Date;
            DateText.Id = Date_Text_Id;
            var paramDateText = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            paramDateText.AddRule(LayoutRules.Below, Message_Text_Id);
            paramDateText.AddRule(LayoutRules.RightOf, Status_Text_Id);
            DateText.LayoutParameters = paramDateText;

         
            // status text
            StatusText = new TextView(context);
            StatusText.SetTextColor(Android.Graphics.Color.Black);
            StatusText.Text = GetStatusString(NativeCell.Status);
            StatusText.Id = Status_Text_Id;
            var paramStatusText = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            paramStatusText.AddRule(LayoutRules.Below, Message_Text_Id);
            StatusText.LayoutParameters = paramStatusText;
			
			
            // name text
            NameText = new TextView(context);
            NameText.SetTextColor(Android.Graphics.Color.Black);
            NameText.Text = NativeCell.Name;
            NameText.Id = Name_Text_Id;
            var paramNameText = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            NameText.LayoutParameters = paramNameText;


            mainView.AddView(MessageText);
            mainView.AddView(DateText);
            mainView.AddView(StatusText);

            if (!string.IsNullOrWhiteSpace(NameText.Text) && NativeCell.IsIncoming)
                mainView.AddView(NameText);

            layout.AddView(mainView);

            return layout;
        }

        public void UpdateCell(MessageCell messageCell)
        {
            this.MessageText.Text = messageCell.MessageBody;
            this.DateText.Text = messageCell.Date;
            this.StatusText.Text = GetStatusString(messageCell.Status);
            this.NameText.Text = messageCell.Name;

            this.NativeCell = messageCell;
        }


        private string GetStatusString(MessageStatuses status)
        {
            string stat = string.Empty;
            if (status == MessageStatuses.Sent)
                stat = "✓";
            else if (status == MessageStatuses.Delivered)
                stat = "✓✓";
            return stat;
        }
    }
}
