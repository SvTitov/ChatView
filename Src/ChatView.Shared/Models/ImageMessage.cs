using System;
using System.Threading.Tasks;
using ChatView.Shared.Abstraction;

namespace ChatView.Shared.Models
{
    public class ImageMessage : BaseUsersMessageModel
    {
        private Uri _imageUrl;
        private byte[] _imageByteArray;
        private Func<Task<byte[]>> _imageLoadCallback;
        private string _placeholder;

        public Uri ImageUri 
        {
            get => _imageUrl;
            set 
            {
                _imageUrl = value;
                NotifyPropertyChanged(nameof(ImageUri));
            }
        }

        public byte[] ImageByteArray
        {
            get { return _imageByteArray; }
            set
            {
                _imageByteArray = value;
                NotifyPropertyChanged(nameof(ImageByteArray));
            }
        }

        public Func<Task<byte[]>> ImageLoadCallback
        {
            get { return _imageLoadCallback; }
            set 
            {
                _imageLoadCallback = value;
                NotifyPropertyChanged(nameof(ImageLoadCallback));
            }
        }

        public string Placeholder
        {
            get => _placeholder;
            set 
            {
                _placeholder = value;
                NotifyPropertyChanged(nameof(Placeholder));
            }
        }
    }
}
