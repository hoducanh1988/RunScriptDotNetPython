using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.InputData {
    public class InputDotNetData : InputClass {

        public InputDotNetData() {
            Classes = new ObservableCollection<InputClass>();
        }

        ObservableCollection<InputClass> _classes;
        public ObservableCollection<InputClass> Classes {
            get { return _classes; }
            set {
                _classes = value;
                OnPropertyChanged(nameof(Classes));
            }
        }
    }
}
