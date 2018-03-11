using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatView.Shared;
using Xamarin.Forms;

namespace ChatViewTest.Core
{
    public class ChatViewModel :INotifyPropertyChanged
    {
        private MessageCollection<MessageModel> _list = new MessageCollection<MessageModel>();

        public MessageCollection<MessageModel> List 
        { 
            get { return _list; }
            set
            { 
                _list = value; 
                NotifyPropertyChanged("List");
            }
        }

        public ChatViewModel()
        {
            List.AddRange(new MessageModel[] { 
                new MessageModel { Message = @"The rose is red, the violet’s blue,
The honey’s sweet, and so are you.
Thou are my love and I am thine;
I drew thee to my Valentine:
The lot was cast and then I drew,
And Fortune said it shou’d be you.", Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = true, Name="Svyatoslav Titov" ,Status = MessageStatuses.Delivered },
                new MessageModel { Message = "Oh, that's cool! 💖".ToString(), Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = ((count++ % 2) != 0), Status = MessageStatuses.Sent, Name="Name" },
            });

            AddCommand = new Command(OnAdd);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand AddCommand { get; set; }

        int count = 0;
        private async void OnAdd(object obj)
        {
            var model = new MessageModel { Message = "Oh, that's cool! 💖".ToString(), Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = ((count++ % 2) != 0), Status = MessageStatuses.Sent, Name="Name" };
            List.Add(model);
        }
    }
}
