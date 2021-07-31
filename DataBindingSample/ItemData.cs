using System.ComponentModel;

namespace DataBindingSample
{
    public class ItemData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ID { get; set; }
        public string Name { get; set; }
    }
}