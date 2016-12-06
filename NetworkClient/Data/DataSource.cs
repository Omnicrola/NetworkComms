using System.Collections.ObjectModel;

namespace NetworkClient.Data
{
    public abstract class DataSource<T>
    {
        public abstract DataCollection<T> Data { get; }
        public abstract T Create();
    }
}