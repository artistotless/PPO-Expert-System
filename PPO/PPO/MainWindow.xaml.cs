using PPO.uiElements;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PPO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Graph currentGraph;
        private solutionPlaceholder currentSol;
        private DBWrapper db;

        public radioBtnPlaceholder selectedAnswer { get; set; }

        private List<Graph> graphList = null;
        private List<Transition> transitionList = null;
        private List<Smartphone> smartphoneList = null;

        public MainWindow()
        {
            InitializeComponent();
            db = new DBWrapper();
            graphList = db.Graphs.ToList<Graph>();
            transitionList = db.Transitions.ToList<Transition>();
            smartphoneList = db.Smartphones.ToList<Smartphone>();
            Restart();
        }

        void Restart()
        {

            currentGraph = GetGraphById(0);
            mainBtn.IsEnabled = true;
            mainBtn.Opacity = 100.0;
            containerAnswerList.Opacity = 100.0;
            Draw();
        }

        void Draw()
        {
            if (currentSol != null)
            {
                currentSol.animation(this, helpers.AnimationTypeEnum.Hide);

            }

            //Если граф без вопроса
            if (currentGraph.result != 0)
            {
                // создать новое окно со смартфоном
                var smartphone = GetSmartphone(currentGraph.result);
                questionText.Text = "Вам подойдет смартфон:  " + smartphone.name;
                answerList.Children.RemoveRange(0, answerList.Children.Count);
                mainBtn.IsEnabled = false;
                var historyItem = new historyItemSolution(">> " + smartphone.name + " <<");

                historyList.Children.Add(historyItem);
                containerAnswerList.Opacity = 0.0;
                mainBtn.Opacity = 0.0;

                currentSol = new solutionPlaceholder(smartphone);
                solutionStackPanel.Children.Insert(1, currentSol);
                return;
            }
            answerList.Children.RemoveRange(0, answerList.Children.Count);
            questionText.Text = "Вопрос:  " + currentGraph.question;
            foreach (Transition tr in GetTransitionsByIndex(currentGraph.id))
            {
                var newRadioBtn = new radioBtnPlaceholder(this, tr);
                answerList.Children.Add(newRadioBtn);

            }

        }


        Graph GetGraphById(int index)
        {
            return graphList.Find((a) => { return a.id == index; });
        }

        Smartphone GetSmartphone(int index)
        {
            return smartphoneList.Find((a) => { return a.id == index; });
        }

        List<Transition> GetTransitionsByIndex(int index)
        {
            return transitionList.FindAll((a) => { return a.initialItemIndex == index; });
        }



        private void clearHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            historyList.Children.RemoveRange(0, historyList.Children.Count);

        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(currentGraph.explanations, "Окно объяснения");

        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void exitProgram(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAnswer != null)
            {
                historyList.Children.Add(new historyItem(currentGraph.question + "->" + selectedAnswer.rd.Content.ToString()));
                currentGraph = GetGraphById((int)selectedAnswer.nextItemIndex);
                Draw();
                return;
            }
            MessageBox.Show("Выделите ответ на вопрос!", "Внимание");
        }

        private void showDiagramWindow(object sender, RoutedEventArgs e)
        {
            new DiagramWindow(this).Show();

        }

        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        private void showSmartphonesWindow(object sender, RoutedEventArgs e)
        {
            new SmartphonesWindow(this).Show();
        }

        private void showTransitionsWindow(object sender, RoutedEventArgs e)
        {
            new TransitionsWindow(this).Show();
        }

        private void showGraphsWindow(object sender, RoutedEventArgs e)
        {
            new GraphsWindow(this).Show();
        }
    }
}
