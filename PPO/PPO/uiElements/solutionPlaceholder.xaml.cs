using PPO.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PPO.uiElements
{
    /// <summary>
    /// Логика взаимодействия для solutionPlaceholder.xaml
    /// </summary>
    public partial class solutionPlaceholder : UserControl
    {

        private string link;
        private string imgPath;
        private MainWindow mainWindow;

        public solutionPlaceholder(Smartphone sm)
        {
            InitializeComponent();
            animation(null, AnimationTypeEnum.Show);
            cost.Text = sm.cost.ToString() + " RUB";
            descr.Text = sm.descr;
            link = sm.link;
            var arrayLink = sm.image.Split('/');
            var imgName = arrayLink[arrayLink.Length - 1];

            var imgDir = Environment.CurrentDirectory + "/img";
            imgPath = imgDir + "/ " + imgName;

            if (!Directory.Exists(imgDir))
                Directory.CreateDirectory(imgDir);

            if (File.Exists(imgPath))
                setImage(imgPath);
            else
                downloadImage(sm.image, imgPath);

        }

        public void animation(MainWindow mainWindow, AnimationTypeEnum animType)
        {
            var animatorMargin = new ThicknessAnimation();
            var animatorOpacity = new DoubleAnimation();

            this.mainWindow = mainWindow;

            switch (animType)
            {
                case AnimationTypeEnum.Show:
                    solPanel.Opacity = 0.0;
                    animatorOpacity.From = 0;
                    animatorOpacity.To = 1.0;
                    animatorMargin.From = new Thickness(0, 80, 0, 0);
                    animatorMargin.To = new Thickness(0, 8, 0, 0);
                    break;

                case AnimationTypeEnum.Hide:
                    solPanel.Opacity = 1.0;
                    animatorOpacity.From = 1.0;
                    animatorOpacity.To = 0.0;
                    animatorMargin.From = new Thickness(0, 8, 0, 0);
                    animatorMargin.To = new Thickness(0, 80, 0, 0);
                    animatorMargin.Completed += Animator_Completed;
                    break;
            }

            animatorMargin.AccelerationRatio = 0.1;
            animatorMargin.Duration = TimeSpan.FromSeconds(2);
            solPanel.BeginAnimation(StackPanel.OpacityProperty, animatorOpacity);
            animatorMargin.AccelerationRatio = 0.5;
            animatorMargin.Duration = TimeSpan.FromSeconds(1);
            solPanel.BeginAnimation(StackPanel.MarginProperty, animatorMargin);

        }

        private void Animator_Completed(object sender, EventArgs e)
        {
            if (mainWindow != null)
                mainWindow.solutionStackPanel.Children.Remove(this);
        }

        private void setImage(string filepath)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filepath);
            bitmapImage.EndInit();
            
                
            image.Source = bitmapImage;
        }

        private void downloadImage(string link, string filepath)
        {
            var client = new WebClient();
            client.DownloadFileAsync(new Uri(link), filepath);
            client.DownloadFileCompleted += Client_DownloadFileCompleted;

        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Изображение смартфона не загружено. \n \n 1) Проверьте подключение к интернету \n 3) Повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                if (File.Exists(imgPath))
                    File.Delete(imgPath);
            }

            else
            {
                setImage(imgPath);
            }
        }

        private void openWebLink(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}
