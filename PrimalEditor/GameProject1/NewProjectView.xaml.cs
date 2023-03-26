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

            //Window.GetWindow(this);获取父级别窗口
            var win = Window.GetWindow(this);
            if (!string.IsNullOrEmpty(projectpath)) {
                dialogResult = true;

                //调用open函数，读取项目列表的数据然后再更新时间降序写回去
                var project = OpenProject.Open(new ProjectData() { ProjectPath = projectpath ,ProjectName = vm.ProjectName});
                // 将打开的project项目数据绑定给父级别窗口
                win.DataContext = project;
            }
            win.DialogResult = dialogResult;
            win.Close();
            

        }
    }
}
