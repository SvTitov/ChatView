using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatView.Shared;
using ChatView.Shared.Abstraction;
using Xamarin.Forms;

namespace ChatViewTest.Core
{
    public class ChatViewModel :INotifyPropertyChanged
    {
        private MessageCollection<BaseMessageModel> _list = new MessageCollection<BaseMessageModel>();

        public MessageCollection<BaseMessageModel> List 
        { 
            get { return _list; }
            set
            { 
                _list = value; 
                NotifyPropertyChanged("List");
            }
        }

        public ICommand AddCommand { get; set; }

        public ChatViewModel()
        {
            HttpClient client = new HttpClient(); 

            //List.Add(new MessagesBuilder()
                     //.CreateImageMessage(new Uri(@"http://cdn3.craftsy.com/blog/wp-content/uploads/2015/01/cloudland_falls_spring_vertical_4408-Edit.jpg"), DateTime.Now.ToString(), true));

            //List.Add(new MessagesBuilder().CreateImageMessage(new Uri(@"https://i2.wp.com/beebom.com/wp-content/uploads/2016/01/Reverse-Image-Search-Engines-Apps-And-Its-Uses-2016.jpg?resize=640%2C426"), DateTime.Now.ToString(), true));
            //List.Add(new MessagesBuilder().CreateTextMessage("good for you dw w  dwa wad awd aw dawd awd wad wd ad ada dad a dad awd ", "242", false));

//            List.AddRange(new MessageModel[] { 
//                new MessageModel { Message = @"The rose is red, the violet’s blue,
//The honey’s sweet, and so are you.
//Thou are my love and I am thine;
//I drew thee to my Valentine:
//The lot was cast and then I drew,
//And Fortune said it shou’d be you.", Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = true, Name="Svyatoslav Titov" ,Status = MessageStatuses.Delivered },
            //    new MessageModel { Message = "Oh, that's cool! 💖".ToString(), Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = ((count++ % 2) != 0), Status = MessageStatuses.Sent, Name="Name" },
            //});

            AddCommand = new Command(OnAdd);
        }

        private async void OnAdd(object obj)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetByteArrayAsync(@"https://i2.wp.com/beebom.com/wp-content/uploads/2016/01/Reverse-Image-Search-Engines-Apps-And-Its-Uses-2016.jpg?resize=640%2C426");
            var image = new MessagesBuilder().CreateImageMessage(new Uri(@"https://i2.wp.com/beebom.com/wp-content/uploads/2016/01/Reverse-Image-Search-Engines-Apps-And-Its-Uses-2016.jpg?resize=640%2C426"),
                                                                 DateTime.Now.ToString(),
                                                                 true,
                                                                 callback: async ()=> await client.GetByteArrayAsync(@"https://www.hd-wallpapersdownload.com/script/bulk-upload/lion-big-wallpapers.jpg") );
            List.Add(image);
            //var textImage = new MessagesBuilder().CreateTextMessage("Hello", DateTime.Now.ToString("YYYY.MM.dd"), false);
            //List.Add(textImage);
        }

        //private async void Nothing(Func<Task<byte[]>> func)
        //{
        //    var res = await func();
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
