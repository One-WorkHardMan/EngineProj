using PrimalEditor.Components;
using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        [DataMember(Name = nameof(GameEntities))]
        private readonly ObservableCollection<GameEntity> _gameentities = new ObservableCollection<GameEntity>();
        public ReadOnlyCollection<GameEntity> GameEntities { get; private set; }

        private void AddGameEntity(GameEntity entity) { 
            Debug.Assert(!_gameentities.Contains(entity));
            _gameentities.Add(entity);
        }

        private void RemoveGameEntity(GameEntity entity)
        {
            Debug.Assert(_gameentities.Contains(entity));   
            _gameentities.Remove(entity);
        }


        public ICommand AddGameEntityCommand { get; private set; }
        public ICommand RemoveGameEntityCommand { get;private set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext streamingContext) {
            if (_gameentities != null) {
                GameEntities = new ReadOnlyCollection<GameEntity>(_gameentities);
                OnPropertyChanged(nameof(GameEntities));
            }

            AddGameEntityCommand = new RelayCommand<GameEntity>(x =>
            {
                AddGameEntity(x);
                var entityIndex = _gameentities.Count - 1;
                Project.undoRedo.Add(
                    new UndoRedoAction(
                        ()=>RemoveGameEntity(x),
                        ()=>_gameentities.Insert(entityIndex,x),
                        $"add{x.Name} to {Name}"
                        )
                    );
            });

            RemoveGameEntityCommand = new RelayCommand<GameEntity>(x =>
            {
                var entityindex = _gameentities.IndexOf(x);
                RemoveGameEntity(x);
                Project.undoRedo.Add(new UndoRedoAction(
                    () => _gameentities.Insert(entityindex, x),
                    () => RemoveGameEntity(x),
                    $"Remove {x.Name}"
                    ));
            });

            
        }

        public Scene(Project project,string name) { 
            Project = project;
            Name = name;
            OnDeserialized(new StreamingContext());
        }
    }
}
