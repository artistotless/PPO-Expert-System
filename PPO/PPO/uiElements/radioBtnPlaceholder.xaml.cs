using System.Windows;
using System.Windows.Controls;


namespace PPO.uiElements
{
    /// <summary>
    /// Логика взаимодействия для radioBtnPlaceholder.xaml
    /// </summary>
    public partial class radioBtnPlaceholder : UserControl
    {
        public int initialItemIndex { get; set; }
        public int nextItemIndex { get; set; }
        private MainWindow mw = null;

        public radioBtnPlaceholder(MainWindow mw, Transition tr)
        {
            InitializeComponent();
            rd.Content = tr.text;
            this.initialItemIndex = tr.initialItemIndex;
            this.nextItemIndex = tr.nextItemIndex;
            this.mw = mw;
            rd.Checked += Rd_Checked;
            

        }

        private void Rd_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            mw.selectedAnswer = this;
        }
    }
}
