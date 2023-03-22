using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject1
{
    [DataContract(Name ="Game")]
    public class Project:ViewModelBase
    {
        public static string Extension { get; } = ".primal";
        [DataMember]
        //设置只读属性
        public string Name { get; private set; }
        [DataMember]
        public string Path { get; private set; }

        public string FullPath => $"{Path}{Name}{Extension}";

// 将下面的这个集合命名为Scenes
        [DataMember(Name = "Scenes")]

        private ObservableCollection<Scene> _scenes = new ObservableCollection<Scene>();

        public ReadOnlyObservableCollection<Scene> scenes { get; }


        public Project(string name,string path) {
            Name = name;
            Path = path;

            _scenes.Add(new Scene(this, "Default Sene"));
        }
    }
}
