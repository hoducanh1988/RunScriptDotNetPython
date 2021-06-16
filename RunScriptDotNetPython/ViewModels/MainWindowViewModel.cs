using RunScriptDotNetPython.Commands;
using RunScriptDotNetPython.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunScriptDotNetPython.ViewModels {
    public class MainWindowViewModel {

        public MainWindowViewModel() {
            _mainview = new MainWindowModel();
            OpenVariable = new MainWindowOpenVariableCommand(this);
            OpenTreeTest = new MainWindowOpenTreeTestCommand(this);
            OpenGo = new MainWindowOpenGoCommand(this);
            OpenResult = new MainWindowOpenResultCommand(this);
        }

        //binding setting name
        MainWindowModel _mainview;
        public MainWindowModel MainView {
            get { return _mainview; }
        }

        //command
        public ICommand OpenVariable {
            get;
            private set;
        }
        public ICommand OpenTreeTest {
            get;
            private set;
        }
        public ICommand OpenGo {
            get;
            private set;
        }
        public ICommand OpenResult {
            get;
            private set;
        }
    }
}
