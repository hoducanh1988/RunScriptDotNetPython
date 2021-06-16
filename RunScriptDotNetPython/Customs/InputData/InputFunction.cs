using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.InputData {
    public class InputFunction : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public InputFunction() {
            NameSpace = Name = ClassName = FullName = PathFile = "";
            Index = 0;
            Parameters = new ObservableCollection<InputParameter>();
        }

        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        string _full_name;
        public string FullName {
            get { return _full_name; }
            set {
                _full_name = value;
                OnPropertyChanged(nameof(FullName));
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
        string _class_name;
        public string ClassName {
            get { return _class_name; }
            set {
                _class_name = value;
                OnPropertyChanged(nameof(ClassName));
            }
        }
        string _name_space;
        public string NameSpace {
            get { return _name_space; }
            set {
                _name_space = value;
                OnPropertyChanged(nameof(NameSpace));
            }
        }
        string _path_file;
        public string PathFile {
            get { return _path_file; }
            set {
                _path_file = value;
                OnPropertyChanged(nameof(PathFile));
            }
        }
        ObservableCollection<InputParameter> _parameters;
        public ObservableCollection<InputParameter> Parameters {
            get { return _parameters; }
            set {
                _parameters = value;
                OnPropertyChanged(nameof(Parameters));
            }
        }
    }
}
