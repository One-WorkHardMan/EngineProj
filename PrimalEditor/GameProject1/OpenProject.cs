using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject1
{
    // 项目的创建数据，主要是路径和打开时间？
    [DataContract]
    public class ProjectData {
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string ProjectPath { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
       
        public string FullPath { get => $"{ProjectPath}{ProjectName}{Project.Extension}";  }
        public Byte[] Icon { get; set; }
        public Byte[] Screenshot { get; set; }
    }
    [DataContract]
    public class ProjectDataList {

        [DataMember]
        public List<ProjectData> Projects { get; set; }
    }


    class OpenProject
    {
        private static readonly string _applicationDataPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\PrimalEditor\";
        private static readonly string _projectDataPath;
        //一个私有的不可见只读的 可观测集合；
        private static readonly ObservableCollection<ProjectData> _projects = new ObservableCollection<ProjectData>();
        //对上面的集合进行封装，一个共有的集合；
        public static ReadOnlyObservableCollection<ProjectData> Projects { get; }

        // 读项目的 创建数据
        public static void ReadProjectData() {
            if (File.Exists(_projectDataPath)) {

                // 妈的，这个b人写的有问题，cnm的！
                var projects = Serializer.FromFile<List<ProjectData>>(_projectDataPath).OrderByDescending(x => x.Date);

                //var projects = projectsList.Projects;

                _projects.Clear();
                foreach (var project in projects) {
                    if (File.Exists(project.FullPath)) { 
                        project.Icon = File.ReadAllBytes($@"{project.ProjectPath}\.primal\Icon.png");
                        project.Screenshot = File.ReadAllBytes($@"{project.ProjectPath}\.primal\Screenshot.png");
                        _projects.Add(project);
                    }
                }
            }
        }
        private static void WriteProjectData()
        {
            // 按照时间进行排序
            var projects = _projects.OrderBy(x => x.Date).ToList();
            Serializer.ToFiles(projects, _projectDataPath);

            
        }
        // 写项目的 创建数据
        public static Project Open(ProjectData data) {
            ReadProjectData();
            
            // 比较最近打开文件的路径和我现在打开的路径是否相同？相同就返回集合的第一个数据element
            var project = _projects.FirstOrDefault(x => x.FullPath == data.FullPath);
            if (project!=null) {
                project.Date = DateTime.Now;
            }
            else {
                project = data;
                project.Date = DateTime.Now;
                _projects.Add(project);
            }
            WriteProjectData();
            return Project.Load(project.FullPath);
        }


        //静态构造函数只执行一次
        static OpenProject()
        {
            try
            {
                // 判断文件夹是否存在
                if (!Directory.Exists(_applicationDataPath)) Directory.CreateDirectory(_applicationDataPath);
                _projectDataPath = $@"{_applicationDataPath}ProjectData.xml";
                Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
                ReadProjectData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
