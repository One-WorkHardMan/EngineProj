using PrimalEditor;
using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject1
{
    [DataContract]
    public class ProjectTemplate {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders;

        public byte[] Icon { get; set; }
        public byte[] Screenshot { get; set; }
        public string IconFilePath { get; set; }
        public string ScreenshotFilePath { get; set; }
        public string ProjectFilePath { get; set; }


    }
    class NewProject : ViewModelBase {
       private readonly string _templatePath = @"E:\Proj\VS_Proj\Engine\PrimalEditor\ProjectTemplate";
       //private readonly string _templatePath = @"D:\Project\VStudio_Proj\EngineProj\PrimalEditor\ProjectTemplate";
        //这里是项目设置的原因，写成..\相对地址就出错了


        private ObservableCollection<ProjectTemplate> _projectTemplates=new ObservableCollection<ProjectTemplate> ();
        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates { get; }
        private string _projectname = "NewProject";
        public string ProjectName
        {
            get => _projectname;
            set
            {
                if (_projectname != value)
                {
                    _projectname = value;
                    OnPropertyChanged(nameof(ProjectName));
                }
            }
        }
        private string _projectpath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\PrimalProjects\";
        //@是逐字字符串
        public string ProjectPath
        {
            get => _projectpath;
            set
            {
                if (_projectpath != value)
                {
                    _projectpath = value;
                    OnPropertyChanged(nameof(ProjectPath));
                }
            }
        }

        /*        private bool ValidateProjection() { 

                }*/



        public NewProject()
        {
            ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemplate>(_projectTemplates);
            try
            {
                var templatesFiles = Directory.GetFiles(_templatePath, "Template.xml", SearchOption.AllDirectories);
                Debug.WriteLine(_templatePath);
                Debug.Assert(templatesFiles.Any());
                foreach (var file in templatesFiles)
                {
                    /*                    var template = new ProjectTemplate()
                                        {
                                            ProjectType = "Empty Project",
                                            ProjectFile = "project.primal",
                                            Folders = new List<string> { ".Primal", "Content", "Gamecode" }
                                        };
                                        //序列化传输，写入xml
                                        Serializer.ToFiles(template, file);*/
                    var  template = Serializer.FromFile<ProjectTemplate>(file);
                    template.IconFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Icon.png"));
                    template.Icon = File.ReadAllBytes(template.IconFilePath);

                    template.ScreenshotFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Screenshot.png"));
                    template.Screenshot= File.ReadAllBytes(template.ScreenshotFilePath);    

                    template.ProjectFilePath= Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), template.ProjectFile));

                    _projectTemplates.Add(template);
                }
            }
            catch (Exception ex) { 
                Debug.WriteLine(ex.Message);
            }
         

        }

        //创建游戏项目函数
        public string CreateProject(ProjectTemplate projectTemplate) {
            //先判断这个路径的名字结尾是不是 \
            if (!Path.EndsInDirectorySeparator(ProjectPath)) ProjectPath += @"\";

            // 将项目路径和项目名称组成和一个新的路径，就是游戏项目的最小的路径
            var path = $@"{ProjectPath}{ProjectName}\";

            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                foreach (var folder in projectTemplate.Folders) {
                    Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path), folder)));
                }
                //前面生成了是哪个文件夹，其中一个.primal 的文件夹，然后我们设置为隐藏文件夹
                var dirinfo = new DirectoryInfo(path+@".primal\");
                dirinfo.Attributes = FileAttributes.Hidden;

                File.Copy(projectTemplate.IconFilePath,Path.GetFullPath(Path.Combine(dirinfo.FullName,"Icon.png")));
                File.Copy(projectTemplate.ScreenshotFilePath, Path.GetFullPath(Path.Combine(dirinfo.FullName, "Screenshot.png")));

                //从模板中读，然后将模板文件primal的缺省参数写好，再写回到具体的项目文件中
                var templatepaimalfle = File.ReadAllText(projectTemplate.ProjectFilePath);
                templatepaimalfle = String.Format(templatepaimalfle,ProjectName, path);
                File.WriteAllText(Path.GetFullPath(Path.Combine(path,$"{ProjectName}{Project.Extension}")), templatepaimalfle);

/*
                var project = new Project(ProjectName,path);
                Serializer.ToFiles(project, path + ProjectName + Project.Extension);*/

                return path;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
