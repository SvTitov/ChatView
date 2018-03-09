using System;
namespace ChatView.Shared
{
    public class StatusHelper
    {
        public static string GetStatusString(MessageStatuses status)
        {
            string stat = string.Empty;
            if (status == MessageStatuses.Sent)
                stat = "✓";
            else if (status == MessageStatuses.Delivered)
                stat = "✓✓";
            return stat;
        }
    }
}
