using PrimalEditor.GameProject1;
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

namespace PrimalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;
            OpenProjiectDialog();
         /*   throw new NotImplementedException();*/
        }

        private void OpenProjiectDialog() {
            var projectBrowser = new ProjectBrowserDialog();
            if (projectBrowser.ShowDialog() == false)
            {
                Application.Current.Shutdown();
            }
            else { };
        }
    }
}
