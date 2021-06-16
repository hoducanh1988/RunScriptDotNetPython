using RunScriptDotNetPython.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunScriptDotNetPython.Commands {
    public class MainWindowOpenResultCommand : ICommand {
        private MainWindowViewModel _mwvm;
        public MainWindowOpenResultCommand(MainWindowViewModel mwvm) {
            _mwvm = mwvm;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //enable button save setting
        public bool CanExecute(object parameter) {
            return true;
        }

        //save setting
        public void Execute(object parameter) {
            System.Windows.MessageBox.Show("open result");
        }

        #endregion

    }
}
