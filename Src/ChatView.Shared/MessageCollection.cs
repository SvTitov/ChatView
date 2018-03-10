using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChatView.Shared
{
    public class MessageCollection<T> : ObservableCollection<T> where T : class
    {
        #region ctor

        public MessageCollection()
        { }

        public MessageCollection(IEnumerable<T> collection)
            : base(collection)
        { }

        public MessageCollection(List<T> list)
            : base(list)
        { }

		#endregion

		protected override void InsertItem(int index, T item)
		{
            if (Device.RuntimePlatform == Device.iOS)
                base.InsertItem(0, item);
            else
                base.InsertItem(index, item);
		}

        public void AddRange(IEnumerable<T> toAdd)
        {
            foreach (var item in toAdd)
                Add(item);
        }
	}
}
