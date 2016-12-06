using System.Collections.ObjectModel;

namespace NetworkClient.Data
{
    public abstract class DataSource<T>
    {
        public abstract ReadOnlyObservableCollection<T> Data { get; }
        public abstract T Create();
    }
}