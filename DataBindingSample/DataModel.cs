using System.Collections.Specialized;
using System.ComponentModel;

namespace DataBindingSample
{
    public class DataModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Number { get; set; }

        public bool IsValid { get; set; }
    }
}