using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemberAppClient
{
    public interface IAsyncRelayCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public class AsyncRelayCommand : IAsyncRelayCommand
    {
        #region Fields

        public event EventHandler CanExecuteChanged;


        private bool _isExecuting;
        readonly Func<Task> _execute;
        readonly Func<bool> _canExecute;

        #endregion // Fields

        #region Constructors

        public AsyncRelayCommand(Func<Task> execute)
            : this(execute, null)
        {
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if(execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

       
        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        Task IAsyncRelayCommand.ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        bool IAsyncRelayCommand.CanExecute()
        {
            throw new NotImplementedException();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync();
        }


        #endregion // ICommand Members
    }
}