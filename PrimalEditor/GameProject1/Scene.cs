using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.GameProject1
{
    [DataContract]
    public class Scene:ViewModelBase
    {
        [DataMember]
        private string _name;

        public string Name { get => _name;
        set {
                if (_name != value) {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            } }
        [DataMember]
        public Project Project { get; private set; }

        /*        public bool IsActive => Project.ActivarteScene == this;*/

        private bool _isactive;
        [DataMember]
        public bool IsActive { get=>_isactive;
            set {
                if (_isactive != value) { 
                    _isactive= value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }  
        public Scene(Project project,string name) { 
            Project = project;
            Name = name;
        }
    }
}
