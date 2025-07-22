using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Libton.Models;
using Libton.Views;

namespace Libton
{

    public partial class MainWindow : Window
    {
        private BindingList<LibModel> _libData;

        public MainWindow()
        {
            InitializeComponent();
            ErrorDialogService.InitializeErrorDisplay(ErrorTextBlock);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string book = textBoxInput.Text;
           
            if(string.IsNullOrEmpty(book))
            {
                ErrorDialogService.ShowErrorMessage("Please enter a book name");
                return;
            }
            _libData.Add(new LibModel() { BookName = book });
            textBoxInput.Text = string.Empty;
            ErrorTextBlock.Text = string.Empty;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _libData = new BindingList<LibModel>();
            LibTable.ItemsSource = _libData;
        }
    }
}
