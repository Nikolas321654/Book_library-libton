using libton;
using Libton.Models;
using Libton.Views;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Libton
{
    public partial class MainWindow : Window
    {
        private BindingList<LibModel> _libraryBooks;

        public MainWindow()
        {
            InitializeComponent();
            _libraryBooks = new BindingList<LibModel>();
            LibTable.ItemsSource = _libraryBooks;
            ErrorDialogService.InitializeErrorDisplay(ErrorTextBlock);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string bookName = (textBoxInput.Text).Trim();
           
            if(string.IsNullOrEmpty(bookName))
            {
                ErrorDialogService.ShowErrorMessage("Please enter a book name");
                return;
            }
            _libraryBooks.Add(new LibModel() { BookName = bookName });
            textBoxInput.Text = string.Empty;
            ErrorTextBlock.Text = string.Empty;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = LibTable.SelectedItem as LibModel;

                if (selectedRow == null)
                {
                    ErrorDialogService.ShowDialogError("No items selected for deletion");
                    return;
                }
                _libraryBooks.Remove(selectedRow);
            }
            catch (InvalidOperationException ex)
            {
                ErrorDialogService.ShowDialogError($"Can`t delete {ex.Message}");
                Console.Write(ex.Message);
            }
            catch (Exception ex) 
            {
                ErrorDialogService.ShowDialogError($"unknown error {ex.Message}");
                Console.Write("unknown error" + ex.Message);
            }
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
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Show();
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
