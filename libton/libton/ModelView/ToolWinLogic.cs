using Libton.Interfaces;
using Libton.Models;
using Libton.Servises;
using System.ComponentModel;

namespace Libton.ModelView
{
    public class ToolWinLogic : IToolWinLogic
    {
        private BindingList<BookModel> _libraryBooks;
        private FileIOService _fileIOService;

        public ToolWinLogic(BindingList<BookModel> libraryBooks, FileIOService fileIOService)
        {
            _libraryBooks = libraryBooks;
            _fileIOService = fileIOService;
        }

        public void AddButton_Click(string bookName)
        {
            if (string.IsNullOrEmpty(bookName))
            {
                new DialogWindow("String is empty").ShowDialog();
                return;
            }

            _libraryBooks.Add(new BookModel() { BookName = bookName });
        }

        public void SaveButton_Click()
        {
           _fileIOService.SaveData(_libraryBooks);
        }

        public void FindButton_Click(string bookName)
        {
            var foundBooks = _libraryBooks.Where(book => book.BookName == bookName).Take(2).ToList();
            
            if (foundBooks.Count == 0)
            {
                new DialogWindow("No books found with the given name").ShowDialog();
            }
            else
            {
                string result = "Found Books:\n" + string.Join("\n", foundBooks.Select(b => b.BookName));
                new DialogWindow(result).ShowDialog();
            }
        }
    }
}
