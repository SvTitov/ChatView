using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatView.Shared.Abstraction;
using ChatView.Shared.Models;

namespace ChatView.Shared
{
    public class MessagesBuilder
    {
        public BaseMessageModel CreateTextMessage(
            string messageText,
            string date,
            bool isIncoming,
            MessageStatuses messageStatus = MessageStatuses.None,
            string name = null,
            string tag = null)
        {
            return new TextMessage()
            {
                Message = messageText,
                Date = date,
                IsIncoming = isIncoming,
                Name = name,
                Status = messageStatus,
                Tag = tag
            };
        }

        public BaseMessageModel CreateImageMessage(
            Uri imageUri,
            string date,
            bool isIncoming,
            string placeholder = null,
            MessageStatuses messageStatus = MessageStatuses.None,
            byte[] imageArray = null,
            Func<Task<byte[]>> callback = null,
            string name = null,
            object tag = null)
        {
            return new ImageMessage()
            {
                ImageUri = imageUri,
                Date = date,
                IsIncoming = isIncoming,
                ImageByteArray = imageArray,
                Status = messageStatus,
                ImageLoadCallback = callback,
                Name = name,
                Tag = tag,
                Placeholder = placeholder
            };
        }

        public BaseMessageModel CreqteSystemMessage(string messageText)
        {
            return new SystemMessage()
            {
                MessageText = messageText
            };
        }


        public IEnumerable<BaseMessageModel> AddRange(params BaseMessageModel[] models)
        {
            foreach (var model in models)
                yield return model;
        }
    }
}
