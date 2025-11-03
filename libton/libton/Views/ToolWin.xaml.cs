using Libton.Interfaces;
using Libton.Models;
using Libton.ModelView;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Libton
{
    public partial class ToolWin : Window
    {
        private MainWindow _mainWindow;
        public IToolWinLogic ToolWinLogic { get; private set; }
        public ToolWin(MainWindow mainWindow, IToolWinLogic logic)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            this.ToolWinLogic = logic;
        }
    }
}