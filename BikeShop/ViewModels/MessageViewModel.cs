using BikeShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BikeShop.ViewModel
{
    public class MessageViewModel
    {
        //textbox에 속성하나, btn에 명령 하나를 사용하는 방법에 사용됨
        //public string MessageText { get; set; }

        //MessageCommand를 사용할 수 있도록 속성 선언
        public MessageCommand DisplayMessageCommand { get; private set; }
        public MessageViewModel()
        {
            //인스턴스 할당 시 수행할 DisplayMessage라는 함수를 파라미터로 입력
            DisplayMessageCommand = new MessageCommand(DisplayMessage);
        }

        //2. textbox의 속성을 이용하지 않고 btn 클릭시 명령에
        //파라미터로 현재 입력한 textbox의 텍스트를 대입하는 방법
        //결과는 똑같음
        public void DisplayMessage(string message)
        {
            //MessageBox.Show(MessageText);
            MessageBox.Show(message);
        }
    }
}
