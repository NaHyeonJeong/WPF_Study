using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace MultiCommandEx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region StringObjectCommand
        /// <summary>
        /// StringObjectCommand
        /// </summary>
        public ICommand StringObjectCommand
        {
            get
            {
                if (stringObjectCommand == null)
                {
                    stringObjectCommand = new DelegateCommand<StringObject>((stringObject) =>
                    {
                        string stringData = stringObject.StringData;
                        object objectData = stringObject.ObjectData;

                    });
                }
                return stringObjectCommand;
            }
        }

        private DelegateCommand<StringObject> stringObjectCommand;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
