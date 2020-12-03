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
    /// Логика взаимодействия для GraphsWindow.xaml
    /// </summary>
    public partial class GraphsWindow : Window
    {
        private DBWrapper db;
        private List<Graph> changedTr;
        private List<Graph> addedTr;

        public GraphsWindow(Window owner)
        {
            InitializeComponent();

            changedTr = new List<Graph>();
            addedTr = new List<Graph>();

            this.Owner = owner;
            this.Owner.Effect = new BlurEffect();
            this.Owner.IsEnabled = false;
            this.Closed += GraphsWindow_Closed;
            db = new DBWrapper();

            table.InitializingNewItem += Table_InitializingNewItem;
            table.CellEditEnding += Table_CellEditEnding;
            table.ItemsSource = db.Graphs.ToList();

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
                changedTr.Add(((Graph)e.Row.Item));
            }
        }


        private void Table_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            addedTr.Add((Graph)e.NewItem);
        }


        private void GraphsWindow_Closed(object sender, EventArgs e)
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
                    db.Graphs.Add(item);
                foreach (var item in changedTr)
                {
                    var newItem = db.Graphs.Find(item.id);
                    newItem.id = item.id;
                    newItem.question = item.question;
                    newItem.result = item.result;
                    newItem.explanations = item.explanations;
                }

                if (table.Items.Count < db.Graphs.ToList().Count)
                {
                    foreach (var item in db.Graphs)
                    {
                        if (!(table.Items.Contains(item)))
                        {
                            db.Graphs.Remove(item);
                        }
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены.","База данных" , MessageBoxButton.OK, MessageBoxImage.Information);


            }

        }
    }
}
