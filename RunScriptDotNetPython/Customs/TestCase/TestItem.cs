using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptDotNetPython.Customs.TestCase {

    public class TestItem : TestClass {

        public TestItem() : base() {
            Type = PathFile = LowerLimit = UpperLimit = "";
            IsChecked = StopWhenFail = true;
            RetryTime = MeasureTime = 1;
            ClassName = "";
            FunctionName = "";
            Arguments = new ObservableCollection<TestArgument>();
        }

        ObservableCollection<TestArgument> _arguments;
        public ObservableCollection<TestArgument> Arguments {
            get { return _arguments; }
            set {
                _arguments = value;
                OnPropertyChanged(nameof(Arguments));
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

        string _class_name;
        public string ClassName {
            get { return _class_name; }
            set {
                _class_name = value;
                OnPropertyChanged(nameof(PathFile));
            }
        }

        string _function_name;
        public string FunctionName {
            get { return _function_name; }
            set {
                _function_name = value;
                OnPropertyChanged(nameof(FunctionName));
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

        bool _is_checked;
        public bool IsChecked {
            get { return _is_checked; }
            set {
                _is_checked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        bool _stop_when_fail;
        public bool StopWhenFail {
            get { return _stop_when_fail; }
            set {
                _stop_when_fail = value;
                OnPropertyChanged(nameof(StopWhenFail));
            }
        }

        int _retry_time;
        public int RetryTime {
            get { return _retry_time; }
            set {
                _retry_time = value;
                OnPropertyChanged(nameof(RetryTime));
            }
        }

        int _measure_time;
        public int MeasureTime {
            get { return _measure_time; }
            set {
                _measure_time = value;
                OnPropertyChanged(nameof(MeasureTime));
            }
        }

        string _lower_limit;
        public string LowerLimit {
            get { return _lower_limit; }
            set {
                _lower_limit = value;
                OnPropertyChanged(nameof(LowerLimit));
            }
        }

        string _upper_limit;
        public string UpperLimit {
            get { return _upper_limit; }
            set {
                _upper_limit = value;
                OnPropertyChanged(nameof(UpperLimit));
            }
        }
    }
}
