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
    public partial class EditSmartphonesWindow : Window
    {
        private DBWrapper db;
        private List<Smartphone> changedTr;
        private List<Smartphone> addedTr;

        public EditSmartphonesWindow(Window owner)
        {
            InitializeComponent();

            changedTr = new List<Smartphone>();
            addedTr = new List<Smartphone>();

            this.Owner = owner;
            this.Owner.Effect = new BlurEffect();
            this.Owner.IsEnabled = false;
            this.Closed += SmartphonesWindow_Closed;
            db = new DBWrapper();

            table.InitializingNewItem += Table_InitializingNewItem;
            table.CellEditEnding += Table_CellEditEnding;
            table.ItemsSource = db.Smartphones.ToList();

        }





        private void Table_CurrentCellChanged(object sender, EventArgs e)
        {
            var SelectedItem = ((DataGrid)sender).SelectedItem;
            var CurrentItem = ((DataGrid)sender).CurrentItem;
            Console.Write(0);
        }

        private void Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Console.Write(0);

            if (e.EditAction == DataGridEditAction.Commit)
            {
                changedTr.Add(((Smartphone)e.Row.Item));
            }
        }


        private void Table_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            addedTr.Add((Smartphone)e.NewItem);
        }


        private void SmartphonesWindow_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
            this.Owner.Effect = null;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveChanges(object sender, RoutedEventArgs e)
        {
            if (db != null)
            {
                foreach (var item in addedTr)
                    db.Smartphones.Add(item);
                foreach (var item in changedTr)
                {
                    var newItem = db.Smartphones.Find(item.id);
                    newItem.id = item.id;
                    newItem.name = item.name;
                    newItem.cost = item.cost;
                    newItem.descr = item.descr;
                    newItem.image = item.image;
                    newItem.link = item.link;
                }

                if (table.Items.Count < db.Smartphones.ToList().Count)
                {
                    foreach (var item in db.Smartphones)
                    {
                        if (!(table.Items.Contains(item)))
                        {
                            db.Smartphones.Remove(item);
                        }
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены.", "База данных", MessageBoxButton.OK, MessageBoxImage.Information);


            }

        }
    }
}
