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

namespace BikeShop.Views
{
    /// <summary>
    /// Practice2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Practice2 : Page
    {
        public Practice2()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            uiListMain.ItemsSource = GetStudentData();
        }

        private List<Student> GetStudentData()
        {
            List<Student> list = new List<Student>();

            list.Add(new Student() { strStudentName = "나현정", strStudentAge = "23", Point = 90 });
            list.Add(new Student() { strStudentName = "나형진", strStudentAge = "21", Point = 20 });
            list.Add(new Student() { strStudentName = "고양이", strStudentAge = "12", Point = 80 });
            list.Add(new Student() { strStudentName = "강아지", strStudentAge = "26", Point = 50 });

            return list;
        }

        private void uibtn_Selected_Click(object sender, RoutedEventArgs e)
        {
            if (uiListMain.SelectedItems != null)
                uiTb_Display.Text = $"선택된 학생은{((Student)uiListMain.SelectedItem).strStudentName} 입니다";
            else
                MessageBox.Show("학생 선택 먼저 해주세요.");
        }

        private void uiListMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (uiListMain.SelectedItem != null)
            {
                this.Title = $"선택된 학생은 {((Student)uiListMain.SelectedItem).strStudentName } 입니다 ";
            }
        }

        private void uibtn_PreStudent_Click(object sender, RoutedEventArgs e)
        {
            if (uiListMain.SelectedItem != null)
            {
                int index = uiListMain.SelectedIndex - 1;

                if (index < 0)
                {
                    index = uiListMain.Items.Count - 1;
                }

                uiListMain.SelectedIndex = index;
            }
        }

        private void uibtn_NextStudent_Click(object sender, RoutedEventArgs e)
        {
            if (uiListMain.SelectedItem != null)
            {
                int index = uiListMain.SelectedIndex + 1;

                if (index >= uiListMain.Items.Count)
                {
                    index = 0;
                }

                uiListMain.SelectedIndex = index;
            }
        }

        private void uibtn_All_Click(object sender, RoutedEventArgs e)
        {
            //uiListMain.SelectAll();

            uiTb_Display.Clear();

            foreach (Student item in uiListMain.SelectedItems)
            {
                uiTb_Display.Text += $"선택된 학생은 {item.strStudentName } 입니다. \n";
            }
        }

        public class Student
        {
            public string strStudentName { get; set; }
            public string strStudentAge { get; set; }
            public int Point { get; set; }
        }
    }
}
