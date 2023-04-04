using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    interface IUndoRedo
    {
        string Name { get; }

        void Undo();

        void Redo();
    }

    public class UndoRedoAction : IUndoRedo {

        private Action _undoAction;
        private Action _redoAction;

        public string Name { get; }

        public void Redo() => _redoAction();
  

        public void Undo() => _undoAction();

        public UndoRedoAction(String name) {
            Name = name;
        }

        public UndoRedoAction(Action undoAction, Action redoAction, string name):this(name)
        {
            
            _undoAction = undoAction;
            _redoAction = redoAction;
            
        }
    }


    class UndoRedo
    {
        private readonly ObservableCollection<IUndoRedo> _redolist = new ObservableCollection<IUndoRedo>();
        private readonly ObservableCollection<IUndoRedo> _undolist = new ObservableCollection<IUndoRedo>();

        public ReadOnlyObservableCollection<IUndoRedo> Redolist { get; }
        public ReadOnlyObservableCollection<IUndoRedo> Undolist { get; }


        public void Reset() { 
            _undolist.Clear();
            _redolist.Clear();  
        }

        public void Add(IUndoRedo cmd) {
            _undolist.Add(cmd);
            _redolist.Clear();
        }

        public void Undo() {
            if (_undolist.Any()) {
                var cmd = _undolist.Last();
                _undolist.RemoveAt(_undolist.Count - 1);
                cmd.Undo();
                //_redolist.Add(cmd);
                _redolist.Insert(0,cmd);
            }
        }
        public void Redo() {
            if (_redolist.Any()) {
                var cmd = _redolist.First();
                _redolist.RemoveAt(0);
                cmd.Redo();
                _undolist.Add(cmd);
            }
        }

        public UndoRedo() {
            Undolist = new ReadOnlyObservableCollection<IUndoRedo>(_undolist);
            Redolist = new ReadOnlyObservableCollection<IUndoRedo>(_redolist);
        }

    }
}
