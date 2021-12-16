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
    /// Menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //버튼 클릭 시 Contact.xaml 페이지로 이동(Navigate)
            NavigationService.Navigate( 
                new Uri("/Views/Contact.xaml", UriKind.Relative)
                );
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //버튼 클릭 시 Discussion.xaml 페이지로 이동(Navigate)
            NavigationService.Navigate(
                new Uri("/Views/Discussion.xaml", UriKind.Relative)
                );
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //버튼 클릭 시 ProductsManagement.xaml 페이지로 이동(Navigate)
            NavigationService.Navigate(
                new Uri("/Views/ProductsManagement.xaml", UriKind.Relative)
                );
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("/Views/Practice.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("/Views/Tabcontrol.xaml", UriKind.Relative));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //DB 연습 페이지로 이동
            NavigationService.Navigate(
                new Uri("/Views/DBPractice.xaml", UriKind.Relative));
        }
    }
}
