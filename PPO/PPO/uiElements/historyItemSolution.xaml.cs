using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PPO.uiElements
{
    /// <summary>
    /// Логика взаимодействия для historyItemSolution.xaml
    /// </summary>
    public partial class historyItemSolution : UserControl
    {
        public historyItemSolution(string data)
        {
            InitializeComponent();
            this.data.Text = data != null ? data : string.Empty;
        }
    }
}
