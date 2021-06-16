using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.TestCase {
    public class TestDelay : TestClass {

        public TestDelay() : base() {
            Unit = Value = "";
            IsChecked = true;
        }

        string _unit;
        public string Unit {
            get { return _unit; }
            set {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
        string _value;
        public string Value {
            get { return _value; }
            set {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        bool _is_checked;
        public bool IsChecked {
            get { return _is_checked; }
            set {
                _is_checked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }


    }
}
