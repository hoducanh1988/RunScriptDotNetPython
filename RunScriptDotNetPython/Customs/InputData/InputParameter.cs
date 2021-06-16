using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.InputData {
    public class InputParameter : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public InputParameter() {
            Name = Type = "";
        }

        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        string _type;
        public string Type {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

    }
}
