using System;
using System.Collections.Generic;
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
            MessageStatuses messageStatus = MessageStatuses.None,
            string name = null,
            object tag = null)
        {
            return new ImageMessage()
            {
                ImageUri = imageUri,
                Date = date,
                IsIncoming = isIncoming,
                Status = messageStatus,
                Name = name,
                Tag = tag
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
