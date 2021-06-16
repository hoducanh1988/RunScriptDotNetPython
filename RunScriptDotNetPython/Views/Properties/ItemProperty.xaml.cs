using RunScriptDotNetPython.Customs.TestCase;
using RunScriptDotNetPython.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ItemProperty.xaml
    /// </summary>
    public partial class ItemProperty : UserControl {

        public ItemProperty(TestClass tc, PropertyInfo p) {
            InitializeComponent();
            ItemPropertyModel<TestClass> ipm = new ItemPropertyModel<TestClass>(tc, p.Name);
            ipm.Name = p.Name;
            ipm.Value = p.GetValue(tc, null).ToString();
            this.DataContext = ipm;
        }

        public class ItemPropertyModel <T> : INotifyPropertyChanged where T : class, new() {
            T t = null;
            string p_name = "";

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name) {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }

            public ItemPropertyModel(T _t, string _p_name) {
                this.t = _t;
                this.p_name = _p_name;
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
                        PropertyInfo pi = t.GetType().GetProperty(p_name);
                        pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);
                    }
                }
            }
        }




    }
}
