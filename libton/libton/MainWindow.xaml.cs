using libton;
using Libton.Models;
using Libton.Servises;
using Libton.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
        private readonly string PATH = $"{Environment.CurrentDirectory}\\bookLibraryList.json";
        private BindingList<LibModel> _libraryBooks;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
            ErrorDialogService.InitializeErrorDisplay(ErrorTextBlock);
        }

        private bool TryReadBookName(out string bookName)
        {
            bookName = (textBoxInput.Text).Trim();
            if (string.IsNullOrEmpty(bookName))
            {
                ErrorDialogService.ShowErrorMessage("Please enter a book name");
                return false;
            }

            return true;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryReadBookName(out string bookName)) return;

            _libraryBooks.Add(new LibModel() { BookName =  bookName});
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

        private void SaveButton_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                _fileIOService.SaveData(_libraryBooks);
            }
            catch (Exception ex)
            {
                ErrorDialogService.ShowDialogError($"Save error {ex.Message}");
                Console.Write("Save error" + ex.Message);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryReadBookName(out string bookName))
            {
                ErrorDialogService.ShowDialogError($"Can`t find the book");
                return;
            }

            try
            {
                var findBook = _libraryBooks.First((books) => books.BookName == bookName);
                LibTable.SelectedItem = findBook;
                LibTable.ScrollIntoView(findBook);
            }
            catch (InvalidOperationException ex)
            {
                ErrorDialogService.ShowDialogError($"Book not found {ex.Message}");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // maybe in the future
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Show();
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _libraryBooks = _fileIOService.LoadData();
                LibTable.ItemsSource = _libraryBooks;
            }
            catch (Exception ex)
            {
                ErrorDialogService.ShowDialogError(ex.Message);
                Console.Write(ex.Message);
            }
        }
    }
}
