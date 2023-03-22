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

namespace PrimalEditor.GameProject1
{
    /// <summary>
    /// NewProjectView.xaml 的交互逻辑
    /// </summary>
    public partial class NewProjectView : UserControl
    {
        public NewProjectView()
        {
            InitializeComponent();
        }

        private void On_Create_new_Project(object sender, RoutedEventArgs e)
        {
            //as 是强制类型转换，将DataContext 转换 NewProject 类
            var vm = DataContext as NewProject;

            var projectpath = vm.CreateProject(templateListBox.SelectedItem as ProjectTemplate);

            bool dialogResult = false;

            //获取当前窗口
            var win = Window.GetWindow(this);
            if (!string.IsNullOrEmpty(projectpath)) {
                dialogResult = true;
            }
            win.DialogResult = dialogResult;
            win.Close();
            

        }
    }
}
