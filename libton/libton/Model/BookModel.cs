using System;

namespace Libton.Models
{
    public class BookModel
    {
        private string _bookName;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; }
        }
    }
}