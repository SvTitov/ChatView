﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            List.AddRange(new MessageModel[] { new MessageModel {Message = "Планеты Солнечной системы (фото и описание) Это самая бгда Меркурий находится достаточно дам.", Date=DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = true},
                          new MessageModel {Message = "Дамы я кекаю как бох.", Date=DateTime.Now.ToString("yyyy.MM.dd"), IsIncoming = false}}); 

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
        private void OnAdd(object obj)
        {
            List.Insert(0, new MessageModel { Message = new Random().Next().ToString(), Date = "✓✓", IsIncoming = ((count++ % 2 ) == 0) });
            //List.Add(new MessageModel { Message = new Random().Next().ToString(), Date = DateTime.Now.ToString("YYYY.MM.dd") });
        }
    }
}
