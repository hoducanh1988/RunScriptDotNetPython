using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using RunScriptDotNetPython.Global;
using RunScriptDotNetPython.Customs.InputData;
using RunScriptDotNetPython.Customs.TestCase;
using RunScriptDotNetPython.Customs;
using System.Collections.ObjectModel;
using System.Reflection;
using RunScriptDotNetPython.Models;
using RunScriptDotNetPython.Views.Properties;
using RunScriptDotNetPython.ViewModels;
using System.IO;
using System.Diagnostics;

namespace RunScriptDotNetPython.Views {
    /// <summary>
    /// Interaction logic for TreeTestView.xaml
    /// </summary>
    public partial class TreeTestView : UserControl {

        public TreeTestView() {
            InitializeComponent();
            bindingData();
        }

        private void bindingData() {
            this.trvInput.ItemsSource = myGlobal.InputCollection;
            this.trvTestCase.ItemsSource = myGlobal.testFolders;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button bt = sender as Button;
            string b_tag = (string)bt.Tag;

            switch (b_tag) {
                case "add_script": {
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.Title = "Open Python Script / DotNet DLL File";
                        fileDialog.Filter = "(*.py,*.pyw,*.dll)|*.py;*.pyw;*.dll";
                        if (fileDialog.ShowDialog() == true) {
                            if (fileDialog.FileName.EndsWith(".dll")) addNetDllFile(fileDialog, myGlobal.InputCollection);
                            else addPythonFile(fileDialog, myGlobal.InputCollection);
                        }
                        break;
                    }
                case "add_tree": {
                        var item = trvInput.SelectedItem;
                        switch (item) {
                            case null: { //No Object, No Function
                                    MessageBox.Show("Vui lòng chọn function cần thêm vào testtree.", "Chọn Function", MessageBoxButton.OK, MessageBoxImage.Error);
                                    break;
                                }
                            case object a when item is InputFunction: { //DotNet Function
                                    var root = myGlobal.testFolders[0];
                                    TestItem ti = new TestItem() {
                                        ID = root.Items.Count,
                                        Name = $"DotNET::{(item as InputFunction).NameSpace}_{(item as InputFunction).ClassName}_{(item as InputFunction).Name}",
                                        ClassName = (item as InputFunction).ClassName,
                                        FunctionName = (item as InputFunction).Name,
                                        IsChecked = true,
                                        Type = "Net",
                                        RetryTime = 1,
                                        MeasureTime = 1,
                                        StopWhenFail = true,
                                        LowerLimit = "",
                                        UpperLimit = "",
                                        PathFile = (item as InputFunction).PathFile,
                                    };
                                    if ((item as InputFunction).Parameters.Count > 0) {
                                        foreach (var p in (item as InputFunction).Parameters) {
                                            ti.Arguments.Add(new TestArgument() { Name = p.Name, Value = "", Type = p.Type });
                                        }
                                    }

                                    root.Items.Add(ti);

                                    break;
                                }
                            case object b when item is InputPythonData: { //Python Function
                                    var root = myGlobal.testFolders[0];
                                    TestItem ti = new TestItem() {
                                        ID = root.Items.Count,
                                        Name = $"{(item as InputPythonData).Name}",
                                        IsChecked = true,
                                        Type = "Python",
                                        RetryTime = 1,
                                        MeasureTime = 1,
                                        StopWhenFail = true,
                                        LowerLimit = "",
                                        UpperLimit = "",
                                        PathFile = (item as InputPythonData).PathFile,
                                    };
                                    if ((item as InputPythonData).Parameters.Count > 0) {
                                        foreach (var p in (item as InputPythonData).Parameters) {
                                            ti.Arguments.Add(new TestArgument() { Name = p.Name, Value = "", Type = p.Type });
                                        }
                                    }

                                    root.Items.Add(ti);
                                    break;
                                }
                            default: { //Yes Object, No Function
                                    break;
                                }
                        }
                        ExpandAllNote();
                        break;
                    }
                case "add_timer": {
                        var root = myGlobal.testFolders[0];
                        TestDelay delay = new TestDelay() { Name = "Sleep", Unit = "ms", Value = "1000", ID = root.Items.Count };
                        root.Items.Add(delay);
                        ExpandAllNote();
                        break;
                    }
                case "remove_tree": {
                        var item = trvTestCase.SelectedItem;
                        var root = myGlobal.testFolders[0];
                        switch (item) {
                            case null: { break; }
                            case object a when item is TestDelay: {
                                    root.Items.Remove(item as TestDelay);
                                    break;
                                }
                            case object b when item is TestItem: {
                                    root.Items.Remove(item as TestItem);
                                    break;
                                }
                        }
                        ExpandAllNote();
                        break;
                    }
                case "up_tree": {
                        var item = trvTestCase.SelectedItem;
                        upTestItem(item as TestClass);
                        break;
                    }
                case "down_tree": {
                        var item = trvTestCase.SelectedItem;
                        downTestItem(item as TestClass);
                        break;
                    }
            }
        }

        private void trvTestCase_Selected(object sender, RoutedEventArgs e) {
            var item = trvTestCase.SelectedItem;
            switch (item) {
                case object a when item is TestItem:
                case object b when item is TestDelay: {
                        addProperty(item as TestClass);
                        addArgument(item as TestClass);
                        break;
                    }
                default: {
                        this.sp_Properties.Children.Clear();
                        this.sp_Parameters.Children.Clear();
                        break;
                    }
            }
        }

        private void addProperty(TestClass item) {
            this.sp_Properties.Children.Clear();
            List<string> ParameterNames = new List<string>() { "ID", "Name", "Type", "IsChecked", "StopWhenFail", "RetryTime", "MeasureTime", "UpperLimit", "LowerLimit", "PathFile", "ClassName", "FunctionName", "Unit", "Value" };
            foreach (var pn in ParameterNames) {
                var p = item.GetType().GetProperty(pn);
                if (p != null) {
                    ItemProperty view = new ItemProperty(item, p);
                    this.sp_Properties.Children.Add(view);
                }
            }
        }

        private void addArgument(TestClass item) {
            this.sp_Parameters.Children.Clear();
            foreach (PropertyInfo info in item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                if (info.PropertyType == typeof(ObservableCollection<TestArgument>)) {
                    var ars = (ObservableCollection<TestArgument>)item.GetType().GetProperty(info.Name).GetValue(item, null);
                    if (ars.Count > 0) {
                        foreach (var a in ars) {
                            ItemArgument view = new ItemArgument(a);
                            this.sp_Parameters.Children.Add(view);
                        }
                    }
                }
            }
        }

        private void upTestItem(TestClass item) {
            var idx = myGlobal.testFolders[0].Items.IndexOf(item);
            if (idx == 0) return;
            Move<TestClass>(myGlobal.testFolders[0].Items, idx, idx - 1);

            try {
                TreeViewItem tv = (TreeViewItem)trvTestCase.ItemContainerGenerator.ContainerFromIndex(0);
                    //myGlobal.testFolders[0].Items[idx - 1] as TreeViewItem;
                tv.IsSelected = true;
            } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
        }

        private void downTestItem(TestClass item) {
            var idx = myGlobal.testFolders[0].Items.IndexOf(item);
            if (idx == myGlobal.testFolders[0].Items.Count - 1) return;
            Move<TestClass>(myGlobal.testFolders[0].Items, idx, idx + 1);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            MenuItem mi = sender as MenuItem;
            string mTag = (string)mi.Tag;

            switch (mTag) {
                case "run": {
                        var item = trvTestCase.SelectedItem;
                        Thread t = new Thread(new ThreadStart(() => {
                            bool kq = false;
                            if (item != null && item is TestItem) {
                                string result = "";
                                result = (item as TestItem).Type.ToLower().Equals("python") ? RunTestPythonItem(item as TestItem): RunTestNetItem(item as TestItem);
                                decimal x;
                                bool r = decimal.TryParse(result, out x);
                                if (r) {
                                    decimal ll, ul;
                                    bool r1 = false, r2 = false;
                                    r1 = decimal.TryParse((item as TestItem).LowerLimit, out ll);
                                    r2 = decimal.TryParse((item as TestItem).UpperLimit, out ul);
                                    if (r1 == false || r2 == false) kq = false;
                                    else kq = ll <= x && x <= ul;
                                }
                                else kq = result.Contains((item as TestItem).LowerLimit.ToLower()) || result.Equals((item as TestItem).UpperLimit.ToLower());
                            }
                            MessageBox.Show(kq ? "Passed" : "Failed");
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break; 
                    }
                case "refresh": { break; }
                default: { break; }
            }

        }

        #region support-functions

        private void addPythonFile(OpenFileDialog fileDialog, ObservableCollection<InputClass> collection) {
            InputPythonData py = new InputPythonData();
            py.Name = $"Python::{fileDialog.SafeFileName}";
            py.PathFile = fileDialog.FileName;
            var w = collection.Where(x => x.Name.Equals(py.Name)).FirstOrDefault();
            if (w != null) { 
                MessageBox.Show($"Script file ''{py.Name}'' đã tồn tại.", "Input Python Script", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //load arguments
            string[] buffer = File.ReadAllLines(py.PathFile);
            foreach (var b in buffer) {
                if (b.Contains("sys.argv[")) {
                    InputParameter arg = new InputParameter() { Name = b.Split('=')[0].Trim() };
                    py.Parameters.Add(arg);
                }
            }
            collection.Add(py);
        }

        private void addNetDllFile(OpenFileDialog fileDialog, ObservableCollection<InputClass> collection) {
            InputDotNetData idn = new InputDotNetData();
            idn.Name = $"DotNET::{fileDialog.SafeFileName}";
            string path_file = fileDialog.FileName;
            var w = collection.Where(x => x.Name.Equals(idn.Name)).FirstOrDefault();
            if (w != null) { MessageBox.Show($"DotNet DLL file ''{idn.Name}'' đã tồn tại.", "Input DotNet DLL", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            Assembly assembly = Assembly.LoadFile(path_file);
            foreach (Type type in assembly.GetTypes()) {
                if (type.IsClass) {
                    var ic = new InputClass() { Name = type.Name };
                    string default_func = "Equals,GetHashCode,GetType,ToString";
                    MethodInfo[] method = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    if (method != null) {
                        foreach (var m in method) {
                            if (default_func.Contains(m.Name) == false) {
                                var fc = new InputFunction() { Name = m.Name, NameSpace = type.Namespace, FullName = m.ToString(), ClassName = ic.Name, PathFile = path_file };
                                ParameterInfo[] pis = m.GetParameters();
                                foreach (var p in pis) {
                                    fc.Parameters.Add(new InputParameter() { Name = p.Name, Type = p.ParameterType.ToString() });
                                }
                                ic.Functions.Add(fc);
                            }
                        }
                    }
                    idn.Classes.Add(ic);
                }
            }
            collection.Add(idn);
        }

        private void ExpandAllNote() {
            Style Style = new Style {
                TargetType = typeof(TreeViewItem)
            };

            Style.Setters.Add(new Setter(TreeViewItem.IsExpandedProperty, true));
            trvTestCase.ItemContainerStyle = Style;
        }

        private void Move<T>(ObservableCollection<T> list, int oldIndex, int newIndex) {
            // exit if positions are equal or outside array
            if ((oldIndex == newIndex) || (0 > oldIndex) || (oldIndex >= list.Count) || (0 > newIndex) ||
                (newIndex >= list.Count)) return;
            // local variables
            var i = 0;
            T tmp = list[oldIndex];
            // move element down and shift other elements up
            if (oldIndex < newIndex) {
                for (i = oldIndex; i < newIndex; i++) {
                    list[i] = list[i + 1];
                    var id_info = list[i].GetType().GetProperty("ID");
                    id_info.SetValue(list[i], oldIndex);
                }
            }
            // move element up and shift other elements down
            else {
                for (i = oldIndex; i > newIndex; i--) {
                    list[i] = list[i - 1];
                    var id_info = list[i].GetType().GetProperty("ID");
                    id_info.SetValue(list[i], oldIndex);
                }
            }
            // put element from position 1 to destination
            list[newIndex] = tmp;
            var id = list[newIndex].GetType().GetProperty("ID");
            id.SetValue(list[newIndex], newIndex);
        }

        #endregion

        #region run test

        private string RunTestPythonItem(TestItem item) {
            string result = string.Empty;
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            //python interprater location
            start.FileName = @"C:\Python39\python.exe";
            //argument with file name and input parameters
            string arg = item.PathFile;
            foreach (var a in item.Arguments) { arg += " " + string.Format("\"{0}\"", a.Value); }
            start.Arguments = arg;
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            start.LoadUserProfile = true;
            using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start)) {
                using (StreamReader reader = process.StandardOutput) {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    if (string.IsNullOrEmpty(result) == true) result = stderr;
                }
            }

            return result;
        }


        private string RunTestNetItem(TestItem item) {
            string result = string.Empty;

            Assembly a = Assembly.LoadFile(item.PathFile);
            string[] buffer = item.PathFile.Split('\\');
            Type t = a.GetType($"{buffer[buffer.Length - 1].Replace(".dll", "")}.{item.ClassName}");
            var Instance = Activator.CreateInstance(t);

            MethodInfo m = t.GetMethod(item.FunctionName); // For example
            if (m == null) return null;
            if (item.Arguments.Count == 0) result = m.Invoke(Instance, null).ToString();
            else {
                object[] parameters = new object[item.Arguments.Count];
                for (int i = 0; i < item.Arguments.Count; i++) parameters[i] = Convert.ChangeType(item.Arguments[i].Value, Type.GetType(item.Arguments[i].Type));
                result = m.Invoke(Instance, parameters).ToString();
            }
            return result;
        }


        #endregion

    }
}
