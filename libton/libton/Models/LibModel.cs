﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libton.Models
{
    class LibModel
    {
        private string _bookName;
        public DateTime DateTime { get; set; } = DateTime.Now;

        public string BookName {
            get { return _bookName; }
            set {  _bookName = value; }
        }
    }
}
