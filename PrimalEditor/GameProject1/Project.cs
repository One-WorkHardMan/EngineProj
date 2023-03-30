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
using System.Windows;

namespace PrimalEditor.GameProject1
{
    [DataContract(Name ="Game")]
    public class Project:ViewModelBase
    {
        public static string Extension { get; } = ".primal";
        [DataMember]
        //设置只读属性
        public string Name { get; private set; } = "New Project";
        [DataMember]
        public string Path { get; private set; }

        public string FullPath => $"{Path}{Name}{Extension}";

// 将下面的这个集合命名为Scenes
        [DataMember(Name = "Scenes")]

        private ObservableCollection<Scene> _scenes = new ObservableCollection<Scene>();

        public ReadOnlyObservableCollection<Scene> Scenes { get; private set; }

        private Scene _activateScene;

        public Scene ActivateScene {
            get =>_activateScene;
            set {
                if (_activateScene != value) {
                    _activateScene = value;
                    OnPropertyChanged(nameof(ActivateScene));
                }
            }
        }

        // 当前窗口下的数据上下文转换成了Project对象
        public static Project Current => Application.Current.MainWindow.DataContext as Project;

        // 从文件中加载一个项目对象
        public static Project Load(String file) {
            Debug.Assert(File.Exists(file));
            return Serializer.FromFile<Project>(file);
        }

        public void Unload() { 
            
        }
        public static void Save(Project project) {
            Serializer.ToFiles<Project>(project,project.FullPath);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            if (_scenes != null) {
                Scenes = new ReadOnlyObservableCollection<Scene>(_scenes);
                OnPropertyChanged(nameof(Scenes));
            }
            // 序列化完成之后，_scenes中已经被从.primal文件读取的scene类填满，然后将里面默认激活的scene的场景绑定到ActivarteScene
            ActivateScene = Scenes.FirstOrDefault(x => x.IsActive);
        }

        public Project(string name,string path) {
            Name = name;
            Path = path;

            /* _scenes.Add(new Scene(this, "Default Sene"));*/
            OnDeserialized(new StreamingContext());
        }
    }
}
