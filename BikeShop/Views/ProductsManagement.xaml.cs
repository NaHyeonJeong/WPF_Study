using System.Windows.Controls;

namespace BikeShop
{
    /// <summary>
    /// ProductsManagement.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProductsManagement : Page
    {
        ProductsFactory factory = new ProductsFactory();

        public ProductsManagement()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = factory.FindProducts(textBox.Text);
        }
    }
}
