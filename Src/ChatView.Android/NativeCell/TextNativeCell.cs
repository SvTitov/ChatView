using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using ChatView.Shared;
using ChatView.Shared.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ChatView.Droid.NativeCells
{
    public class TextNativeCell : LinearLayout, INativeElementView
    {
        public Element Element => NativeCell;
        public TextMessageCell NativeCell { get; private set; }

        public TextView MessageText { get; set; }
        public TextView DateText { get; set; }
        public TextView StatusText { get; set; }
        public TextView NameText { get; set; }

        private const int Message_Text_Id = 100;
        private const int Date_Text_Id = 101;
        private const int Status_Text_Id = 102;
        private const int Name_Text_Id = 103;

        public TextNativeCell(Context context, TextMessageCell cell)
            : base(context)
        {
            NativeCell = cell;

            var view = CreateLayout(context);
            AddView(view);
        }

        private Android.Views.View CreateLayout(Context context)
        {
            Android.Widget.RelativeLayout layout = new Android.Widget.RelativeLayout(context);
            var layoutParam = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            layout.SetPadding(30,15,30,15);
            layout.LayoutParameters = layoutParam;


            //main layout
            Android.Widget.LinearLayout mainView = new Android.Widget.LinearLayout(context);
            var paramLayout = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            mainView.Orientation = Orientation.Vertical;
            paramLayout.AddRule(NativeCell.IsIncoming ? LayoutRules.AlignParentLeft : LayoutRules.AlignParentRight);
            paramLayout.SetMargins(NativeCell.IsIncoming ? 0 : 50, 0, NativeCell.IsIncoming ? 50 : 0, 0);
            mainView.SetPadding(30, 30, 30, 30);
            mainView.LayoutParameters = paramLayout;

            //set drawable
            GradientDrawable shape = new GradientDrawable();
            shape.SetCornerRadius(NativeCell.CornerRadius * 2);
            shape.SetColor(NativeCell.IsIncoming ? NativeCell.IncomingColor.ToAndroid() : NativeCell.OutgoingColor.ToAndroid());
            mainView.Background = shape;


            // name text
            NameText = new TextView(context);
            NameText.SetTextColor(NativeCell.NameFontColor.ToAndroid());
            NameText.TextSize = NativeCell.NameFontSize;
            NameText.Text = NativeCell.Name;
            NameText.Id = Name_Text_Id;
            var paramNameText = new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            NameText.LayoutParameters = paramNameText;

            //message text
            MessageText = new TextView(context);
            MessageText.SetTextColor(NativeCell.TextFontColor.ToAndroid());
            MessageText.Text = NativeCell.MessageBody;
            MessageText.TextSize = NativeCell.TextFontSize;
            MessageText.Id = Message_Text_Id;
            var paramMessageText = new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            MessageText.LayoutParameters = paramMessageText;

            // status layout
            LinearLayout linearLayout = new LinearLayout(context);
            linearLayout.Orientation = Orientation.Horizontal;
            var paramlinearLayout = new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            paramlinearLayout.Gravity = Android.Views.GravityFlags.Right;
            linearLayout.SetGravity(Android.Views.GravityFlags.Right);
            linearLayout.LayoutParameters = paramlinearLayout;

            // status text
            StatusText = new TextView(context);
            StatusText.SetTextColor(NativeCell.InfoFontColor.ToAndroid());
            StatusText.Text = StatusHelper.GetStatusString(NativeCell.Status);
            StatusText.Id = Status_Text_Id;
            StatusText.TextSize = NativeCell.InfoFontSize;
            StatusText.SetPadding(0, 0, 10, 0);
            StatusText.Gravity = Android.Views.GravityFlags.Left;
            var paramStatusText = new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            paramStatusText.Gravity = Android.Views.GravityFlags.Left;
            StatusText.LayoutParameters = paramStatusText;
            if (!NativeCell.IsIncoming)
                linearLayout.AddView(StatusText);

            // date text
            DateText = new TextView(context);
            DateText.SetTextColor(NativeCell.InfoFontColor.ToAndroid());
            DateText.Text = NativeCell.Date;
            DateText.SetLines(1);
            DateText.TextSize = NativeCell.InfoFontSize;
            DateText.SetMinWidth(LayoutParams.WrapContent);
            DateText.Id = Date_Text_Id;
            var paramDateText = new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            DateText.LayoutParameters = paramDateText;
            linearLayout.AddView(DateText);

         
            if (!string.IsNullOrWhiteSpace(NameText.Text) && NativeCell.IsIncoming)
                mainView.AddView(NameText);

            mainView.AddView(MessageText);
            mainView.AddView(linearLayout);
            layout.AddView(mainView);

            return layout;
        }

        public void UpdateCell(TextMessageCell messageCell)
        {
            this.MessageText.Text = messageCell.MessageBody;
            this.DateText.Text = messageCell.Date;
            this.StatusText.Text = StatusHelper.GetStatusString(messageCell.Status);
            this.NameText.Text = messageCell.Name;

            this.NativeCell = messageCell;
        }
    }
}
