using PrimalEditor.GameProject1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Components
{
    [DataContract]
    public class GameEntity: ViewModelBase
    {
        private string _name;
        [DataMember]
        public string Name { get=>_name;
        set{
                if (_name != value) { 
                    _name= value;
                    OnPropertyChanged(nameof(_name));
                } 
            } 
        }
        [DataMember]
        public Scene ParentScene { get; private set; }
        [DataMember(Name = nameof(Components))]
        private readonly ObservableCollection<Component> _components = new ObservableCollection<Component>();
        public ReadOnlyObservableCollection<Component> Components { get; }

        public GameEntity(Scene scene) { 
            Debug.Assert(scene!=null);
            ParentScene = scene;
        }



    }
}