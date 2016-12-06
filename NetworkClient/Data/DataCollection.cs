using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using NetworkClient.Annotations;

namespace NetworkClient.Data
{
    public class DataCollection<T> : ReadOnlyObservableCollection<T>
    {
        public DataCollection([NotNull] ObservableCollection<T> list) : base(list)
        {
            CollectionChanged += HandleChange;
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataChanged?.Invoke(sender, e);
        }

        public event EventHandler<NotifyCollectionChangedEventArgs> DataChanged;
    }
}