using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatView.Shared;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatViewTest.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatViewTestPage : ContentPage
    {
        public ChatViewTestPage()
        {
            InitializeComponent();
            this.BindingContext = new ChatViewModel();

            //OfList.OnLongClick += OfList_OnLongClick;
        }

        void OfList_OnLongClick(object sender, object e)
        {
        }
    }
}
