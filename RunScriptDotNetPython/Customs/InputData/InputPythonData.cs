using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.InputData {

    public class InputPythonData : InputClass {

        public InputPythonData() : base() {
            PathFile = "";
            Parameters = new ObservableCollection<InputParameter>();
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
