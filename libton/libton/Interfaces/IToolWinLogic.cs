using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libton.Interfaces
{
    public interface IToolWinLogic
    {
        void AddButton_Click(string bookName);
        void SaveButton_Click();
        void FindButton_Click(string bookName);
    }
}
