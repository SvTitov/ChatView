using System;
namespace ChatViewTest.Core
{
    public class MessageModel
    {
        public string Message { get; set; }
        public string Date { get; set; }
        public bool IsIncoming { get; set; }
    }
}
