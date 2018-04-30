using System;
using ChatView.Shared.Models;
using Xamarin.Forms;

namespace ChatView.Shared
{
    public class CellDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ImageCellTemplate { get; set; }
        public DataTemplate TextCellTemplate { get; set; }
        public DataTemplate SystemCellTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ImageMessage)
                return ImageCellTemplate;
            if (item is TextMessage)
                return TextCellTemplate;
            if (item is SystemMessage)
                return SystemCellTemplate;

            return null;
        }
    }
}
