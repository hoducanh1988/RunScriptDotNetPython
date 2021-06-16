using RunScriptDotNetPython.ViewModels;
using System;
using System.Collections.Generic;
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
using RunScriptDotNetPython.Views;

namespace RunScriptDotNetPython {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            this.grid_main.Children.Clear();
            this.grid_main.Children.Add(new TreeTestView());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button bt = sender as Button;
            switch (bt.Tag) {
                case "minimum": {
                        this.WindowState = WindowState.Minimized;
                        break;
                    }
                case "maximum": {
                        if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
                        else this.WindowState = WindowState.Maximized;
                        break;
                    }
                case "close": {
                        this.Close();
                        break;
                    }
            }
        }
    }
}
