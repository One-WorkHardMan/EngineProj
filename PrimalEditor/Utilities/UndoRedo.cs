using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    public interface IUndoRedo
    {
        string Name { get; }

        void Undo();

        void Redo();
    }

    //实现IUndoRedo接口：

    public class UndoRedoAction : IUndoRedo {

        private Action _undoAction;
        private Action _redoAction;

        
        // 操作的名称
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


    public class UndoRedo
    {
        private readonly ObservableCollection<IUndoRedo> _redolist = new ObservableCollection<IUndoRedo>();
        private readonly ObservableCollection<IUndoRedo> _undolist = new ObservableCollection<IUndoRedo>();

        public ReadOnlyObservableCollection<IUndoRedo> Redolist { get; }
        public ReadOnlyObservableCollection<IUndoRedo> Undolist { get; }


        public void Reset() { 
            _undolist.Clear();
            _redolist.Clear();  
        }

        // 把需要撤销的 指令 加到 undolist 集合中！！并且每次添加新的cmd会把redolist清空。
        public void Add(IUndoRedo cmd) {
            _undolist.Add(cmd);
            _redolist.Clear();
        }

        //Undo就是 Ctrl z
        public void Undo() {
            if (_undolist.Any()) {
                var cmd = _undolist.Last();
                _undolist.RemoveAt(_undolist.Count - 1);
                cmd.Undo();
                //_redolist.Add(cmd);
                _redolist.Insert(0,cmd);
            }
        }

        //Redo 就是取消刚才的取消。
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
