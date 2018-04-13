using System;
using ChatView.Shared.Abstraction;

namespace ChatView.Shared.Models
{
    public class ImageMessage : BaseUsersMessageModel
    {
        private Uri _imageUrl;

        public Uri ImageUri 
        {
            get => _imageUrl;
            set 
            {
                _imageUrl = value;
                NotifyPropertyChanged(nameof(ImageUri));
            }
        }
    }
}
