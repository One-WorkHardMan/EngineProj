using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrimalEditor
{
    class RelayCommand<T> : ICommand
    {
        // 两个委托 行为委托和断言委托
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canexecute;

        /// <summary>
        /// 这个代码的意思就是，给CanExecuteChanged事件添加处理函数的时候也要给CommandManager.RequerySuggested
        /// 同样添加一样的函数 这个 value就是这个意思。
        /// </summary>
        public event EventHandler? CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canexecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute((T)parameter);
        }
        public RelayCommand(Action<T> execute,Predicate<T> canexecute = null) {

            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canexecute = canexecute;
        }
    }
}
