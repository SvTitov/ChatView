using System;
using ChatView.Shared;

namespace ChatViewTest.Core
{
    public class MessageModel
    {
        public string Message { get; set; }
        public string Date { get; set; }
        public bool IsIncoming { get; set; }
        public string Name { get; set; }
        public MessageStatuses Status { get; set; }
    }
}
