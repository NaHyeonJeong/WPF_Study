using BikeShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BikeShop
{
    /// <summary>
    /// Discussion.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Discussion : Page
    {
        public Discussion()
        {
            InitializeComponent();
            DataContext = new MessageViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("메시지 보내기에 성공하였습니다", "결과");
        }
    }
}
