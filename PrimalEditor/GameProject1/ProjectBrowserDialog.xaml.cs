using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PrimalEditor.GameProject1
{
    /// <summary>
    /// ProjectBrowserDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectBrowserDialog : Window
    {
        public ProjectBrowserDialog()
        {
            InitializeComponent();
        }



        private void OnToggleButton_Click(object sender,RoutedEventArgs e) {
            if (sender == openprojectButton) {
                if (createprojectButton.IsChecked == true) { 
                    createprojectButton.IsChecked = false;
                    browserContent.Margin = new Thickness(0);
                }
                openprojectButton.IsChecked = true;
            }
            if (sender == createprojectButton) {
                if (openprojectButton.IsChecked == true)
                {
                    openprojectButton.IsChecked = false;
                    browserContent.Margin = new Thickness(-800,0,0,0);
                }
                createprojectButton.IsChecked = true;
            }
        }


    }
}
