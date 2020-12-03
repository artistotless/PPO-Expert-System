using PPO.uiElements;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PPO
{
    /// <summary>
    /// Логика взаимодействия для SmartphonesWindow.xaml
    /// </summary>
    public partial class SmartphonesWindow : Window
    {
        private DBWrapper db;
        private List<Smartphone> smartphones;

        public SmartphonesWindow(Window owner)
        {
            InitializeComponent();
            db = new DBWrapper();
            smartphones = db.Smartphones.ToList();

            foreach (var item in smartphones)
            {
                panel.Children.Add(new smartphonePlaceholder(item));
            }

            this.Owner = owner;
            this.Owner.Effect = new BlurEffect();
            this.Owner.IsEnabled = false;

            this.Closed += SmartphonesWindow_Closed; ;
        }



        private void openWebLink(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SmartphonesWindow_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
            this.Owner.Effect = null;
        }

        private void showEditSmartphonesWindow(object sender, RoutedEventArgs e)
        {
            new EditSmartphonesWindow(this).Show();
        }
    }

}

