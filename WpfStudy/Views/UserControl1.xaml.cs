using System.Windows.Controls;

namespace WpfStudy.Views
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Row_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            if (row != null && row.IsSelected)
            {
                DetailWindow1 ad = new DetailWindow1();
                ad.Title = "(임시) 병원 대기자 정보 페이지";
                ad.ShowDialog();
            }
        }
    }
}
