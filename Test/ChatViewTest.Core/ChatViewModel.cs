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
        private ObservableCollection<MessageModel> _list = new ObservableCollection<MessageModel>();

        public ObservableCollection<MessageModel> List 
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
            List.AddRange(new MessageModel[] { new MessageModel { Message = "Планеты Солнечной системы (фото и описание) Это самая бгда Меркурий находится достаточно дам.", Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = true, Name = "Svyatoslav Titov", Status = MessageStatuses.Delivered } });
               // new MessageModel {Message = "Updated text.", Date=DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = false, Status = MessageStatuses.Delivered}}); 

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
            var model = new MessageModel { Message = new Random().Next(1, 200).ToString(), Date = DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = ((count++ % 2) == 0), Status = MessageStatuses.Sent };
            List.Insert(0, model);
            await Task.Factory.StartNew(async () => 
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                model.Message = "daw dawd awd aw ";
                model.Status = MessageStatuses.Delivered;
            });

            //List.Add(new MessageModel { Message = new Random().Next().ToString(), Date = DateTime.Now.ToString("YYYY.MM.dd") });
        }
    }
}
