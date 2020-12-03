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
    /// Логика взаимодействия для DiagramWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        public DiagramWindow(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
            this.Owner.Effect = new BlurEffect();
            this.Owner.IsEnabled = false;

            this.Closed += DiagramWindow_Closed;

            //var bitmapImage = new BitmapImage();
            //bitmapImage.BeginInit();
            //bitmapImage.UriSource = new Uri(Environment.CurrentDirectory+"/");
            //bitmapImage.EndInit();
            //image.Source = bitmapImage;

            image.Source = new BitmapImage(new Uri("/Resources/TreeDiagram.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache }; ;
        }

        private void DiagramWindow_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
            this.Owner.Effect = null;
        }

      
    }
}
