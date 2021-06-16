using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.TestCase {
    public class TestFolder : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public TestFolder() {
            Name = "";
            Items = new ObservableCollection<TestClass>();
        }

        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        ObservableCollection<TestClass> _items;
        public ObservableCollection<TestClass> Items {
            get { return _items; }
            set {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }


    }
}
