using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChatView.Shared
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> toAdd)
        {
            foreach (var item in toAdd)
                collection.Add(item);
        }
    }
}
