﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// OpenProjectView.xaml 的交互逻辑
    /// </summary>
    public partial class OpenProjectView : UserControl
    {
        public OpenProjectView()
        {
            InitializeComponent();

            //this.DataContext = new OpenProject();

            //在Open窗口初始化的时候 ，设置 第一个项目元素为Focous；一直报错，不知道哪里错了、、、


/*            Loaded += (s, e) =>
            {
                var item = projectsListBox.ItemContainerGenerator
                .ContainerFromIndex(projectsListBox.SelectedIndex) as ListBoxItem;
                item?.Focus();
            };*/


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedProject();
        }
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSelectedProject();
        }

        private void OpenSelectedProject()
        {

            var project = OpenProject.Open(projectsListBox.SelectedItem as ProjectData);

            bool dialogResult = false;

            //获取父级 窗口
            var win = Window.GetWindow(this);
            if (project!=null)
            {
                dialogResult = true;
                win.DataContext = project;
            }
            win.DialogResult = dialogResult;
            win.Close();
        }


    }
}
