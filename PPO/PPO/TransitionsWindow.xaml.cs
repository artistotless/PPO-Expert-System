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
    /// Логика взаимодействия для TransitionsWindow.xaml
    /// </summary>
    public partial class TransitionsWindow : Window
    {
        private DBWrapper db;
        private List<Transition> changedTr;
        private List<Transition> addedTr;

        public TransitionsWindow(Window owner)
        {
            InitializeComponent();

            changedTr = new List<Transition>();
            addedTr = new List<Transition>();

            this.Owner = owner;
            this.Owner.Effect = new BlurEffect();
            this.Owner.IsEnabled = false;
            this.Closed += TransitionsWindow_Closed;
            db = new DBWrapper();

            table.InitializingNewItem += Table_InitializingNewItem;
            table.CellEditEnding += Table_CellEditEnding;
            table.ItemsSource = db.Transitions.ToList();

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
                changedTr.Add(((Transition)e.Row.Item));
            }
        }


        private void Table_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            addedTr.Add((Transition)e.NewItem);
        }


        private void TransitionsWindow_Closed(object sender, EventArgs e)
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
                    db.Transitions.Add(item);
                foreach (var item in changedTr)
                {
                    var newItem = db.Transitions.Find(item.id);
                    newItem.id = item.id;
                    newItem.initialItemIndex = item.initialItemIndex;
                    newItem.nextItemIndex = item.nextItemIndex;
                    newItem.text = item.text;
                }

                if (table.Items.Count < db.Transitions.ToList().Count)
                {
                    foreach (var item in db.Transitions)
                    {
                        if (!(table.Items.Contains(item)))
                        {
                            db.Transitions.Remove(item);
                        }
                    }
                }
                var tablen = table.Items;
                var tablenCount = tablen.Count;

                var dbn = db.Transitions;
                var dbnCount = dbn.ToList().Count;


                db.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены.", "База данных", MessageBoxButton.OK, MessageBoxImage.Information);


            }

        }
    }
}
