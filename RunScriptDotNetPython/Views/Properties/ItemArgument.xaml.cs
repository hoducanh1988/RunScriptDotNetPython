using RunScriptDotNetPython.Customs.TestCase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RunScriptDotNetPython.Views.Properties {
    /// <summary>
    /// Interaction logic for ItemArgument.xaml
    /// </summary>
    public partial class ItemArgument : UserControl {
        public ItemArgument(TestArgument ta) {
            InitializeComponent();
            ItemArgumentModel iam = new ItemArgumentModel(ta);
            iam.Name = ta.Name;
            iam.Value = ta.Value;
            this.DataContext = iam;
        }


        public class ItemArgumentModel : INotifyPropertyChanged {
            TestArgument t;

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name) {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }

            public ItemArgumentModel(TestArgument _t) {
                this.t = _t;
                Name = Value = "";
            }

            string _name;
            public string Name {
                get { return _name; }
                set {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
            string _value;
            public string Value {
                get { return _value; }
                set {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                    if (value != "") {
                        this.t.Value = value;
                    }
                }
            }
        }
    }
}
