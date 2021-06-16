using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunScriptDotNetPython.Customs.InputData;
using RunScriptDotNetPython.Customs.TestCase;
using RunScriptDotNetPython.Customs;

namespace RunScriptDotNetPython.Global {
    public class myGlobal {

        public static ObservableCollection<InputClass> InputCollection = new ObservableCollection<InputClass>();
        public static ObservableCollection<TestFolder> testFolders = new ObservableCollection<TestFolder>() {
        new TestFolder(){ Name = "Root" }
        };
    }
}
