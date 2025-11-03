using Libton.Models;
using Libton.ModelView;
using Libton.Servises;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Libton
{
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\bookLibraryList.json";
        private BindingList<BookModel> _libraryBooks;
        private FileIOService _fileIOService;
        private ToolWin _toolWin;
        private ToolWinLogic _toolWinLogic;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void SetToolWin()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            TextBlock title = new TextBlock()
            {
                Text = "Libton",
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5)
            };

            TextBlock enterBookInfo = new TextBlock()
            {
                Text = "Enter Book Name:",
                FontSize = 14,
                Margin = new Thickness(5)
            };

            TextBox textBlock = new TextBox()
            {
                Width = 200,
                Margin = new Thickness(5)
            };

            Button addButton = new Button()
            {
                Content = "Add Book",
                Margin = new Thickness(5),
            };

            Button findButton = new Button()
            {
                Content = "Find Book",
                Margin = new Thickness(5),
            };

            Button saveButton = new Button()
            {
                Content = "Save Library",
                Margin = new Thickness(5),
            };

            if (_toolWin == null)
            {
                _toolWin = new ToolWin(this, _toolWinLogic);
            }

            addButton.Click += (s, e) =>
            {
                _toolWinLogic.AddButton_Click(textBlock.Text);
                textBlock.Text = string.Empty;
            };

            saveButton.Click += (s, e) =>
            {
                _toolWinLogic.SaveButton_Click();
            };
            
            findButton.Click += (s, e) =>
            {
                _toolWinLogic.FindButton_Click(textBlock.Text);
            };


            Grid.SetRow(title, 0);
            Grid.SetRow(enterBookInfo, 1);
            Grid.SetRow(textBlock, 2);
            Grid.SetRow(addButton, 3);
            Grid.SetRow(saveButton, 4);
            Grid.SetRow(findButton, 5);

            grid.Children.Add(title);
            grid.Children.Add(enterBookInfo);
            grid.Children.Add(textBlock);
            grid.Children.Add(addButton);
            grid.Children.Add(saveButton);
            grid.Children.Add(findButton);
           
            if (_toolWin.Content is Grid toolWinGrid)
            {
                toolWinGrid.Children.Clear();
                toolWinGrid.Children.Add(grid);
            }
            else
            {
                _toolWin.Content = grid;
            }

            _toolWin.Owner = this;
            _toolWin.Left = this.Left + this.ActualWidth + 5;
            _toolWin.Top = this.Top;
            _toolWin.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = LibTable.SelectedItem as BookModel;

                if (selectedRow == null)
                {
                    new DialogWindow("Can not delete").ShowDialog();
                    return;
                }
                _libraryBooks.Remove(selectedRow);
            }
            catch (InvalidOperationException ex)
            {
                new DialogWindow(ex.Message).ShowDialog();
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                new DialogWindow(ex.Message).ShowDialog();
                Console.Write("Unknown error" + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _fileIOService = new FileIOService(PATH);
                _libraryBooks = _fileIOService.LoadData();
                LibTable.ItemsSource = _libraryBooks;
                _toolWinLogic = new ToolWinLogic(_libraryBooks, _fileIOService);
                SetToolWin();
            }
            catch (Exception ex)
            {
                new DialogWindow(ex.Message).ShowDialog();
                Console.Write(ex.Message);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _toolWin?.Close();
            base.OnClosing(e);
        }

        public void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
        }

        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}