using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        public static UndoRedo undoRedo { get; } = new UndoRedo();

        //设置指令
        public ICommand AddSceneCommand { get; private set; }
        public ICommand RemoveSceneCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        // 添加一个场景函数：
        private void AddScene(String sceneName) { 
            Debug.Assert(sceneName != null);
            _scenes.Add(new Scene(this, sceneName));

        }
        //移除某个场景：
        private void RemoveScene(Scene scene)
        {
            Debug.Assert(_scenes.Contains(scene));
            _scenes.Remove(scene);
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
            // 对于add操作的 取消和重复 应该怎么执行？
            ///
            /// 取消，就是要把加上的场景给remove；
            /// 重复，就是要把场景又加到_scenes 的对应的Index上；
            /// 
            AddSceneCommand = new RelayCommand<object>(x => {
                AddScene($"New Scene{_scenes.Count+1}");
                var newscene = _scenes.Last();
                var sceneIndex = _scenes.Count- 1;
                undoRedo.Add(new UndoRedoAction(
                    () => RemoveScene(newscene),
                    () => _scenes.Insert(sceneIndex,newscene),
                    $"Add{newscene.Name}"
                    ));
            });

            RemoveSceneCommand = new RelayCommand<Scene>(
                x => {
                    var sceneIndex = _scenes.IndexOf(x);
                    RemoveScene(x);
                    undoRedo.Add(new UndoRedoAction(
                        ()=>_scenes.Insert(sceneIndex,x),
                        ()=>RemoveScene(x),
                        $"Remove{x.Name}"
                        ));
                },x=>!x.IsActive //限制如果这个场景正在active，则不能被remove。
                );


            UndoCommand = new RelayCommand<object>(x => undoRedo.Undo());
            RedoCommand = new RelayCommand<object>(x => undoRedo.Redo());
            SaveCommand = new RelayCommand<object>(x => Save(this));




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
