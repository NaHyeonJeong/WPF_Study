using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeShop.ViewModel.Command
{
    public class MessageCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        //public Action _execute; //textbox, btn에 속성하나 명령하나 사용하는 방법
        private Action<string> _execute;

        public MessageCommand(Action<string> execute)
        {
            //생성자로 전달되는 함수명(execute)을 실제 실행할 명령(_execute)로 대입
            _execute = execute;
        }
        public bool CanExecute(object? parameter)
        {
            //이 함수는 원래 명령 실행 전 선행하는 조건에 해당함
            //이번에는 별도의 조건이 없기 때문에 항상 true를 반환
            return true;
        }

        public void Execute(object? parameter)
        {
            //_execute 수행
            _execute.Invoke(parameter as string);
        }
    }
}
