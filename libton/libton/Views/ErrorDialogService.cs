using Libton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Libton.Views
{
    public class ErrorDialogService
    {
        private static TextBlock _errorTextBlock;

        public static void InitializeErrorDisplay(TextBlock ErrorTextBlock)
        {
            _errorTextBlock = ErrorTextBlock; 
        }

        public static void ShowErrorMessage(string errorText) 
        {
            if(_errorTextBlock != null)
                _errorTextBlock.Text = errorText;
        }

        public static void ShowDialogError(string errorText)
        {
            if (string.IsNullOrEmpty(errorText))
            {
                Console.WriteLine("Error text is null or empty");
                return;
            }

            DialogWindow dialogWindow = new DialogWindow(errorText);
            dialogWindow.Show();
        }
    }
}