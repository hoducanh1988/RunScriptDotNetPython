using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.InputData {
    public class InputClass : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public InputClass() {
            Name = "";
            Index = 0;
            Functions = new ObservableCollection<InputFunction>();
        }

        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        int _index;
        public int Index {
            get { return _index; }
            set {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
        ObservableCollection<InputFunction> _funcs;
        public ObservableCollection<InputFunction> Functions {
            get { return _funcs; }
            set {
                _funcs = value;
                OnPropertyChanged(nameof(Functions));
            }
        }

    }
}
