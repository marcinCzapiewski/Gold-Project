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
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Logika interakcji dla klasy playerVSPlayer.xaml
    /// </summary>
    public partial class playerVSPlayer : UserControl
    {
        
        public event EventHandler play;
        private string namePlayer;
        public int highScore;
        public Grid[] playerGrid;
        public Grid[] compGrid;
        public int lifePlayer = 20;
       
        public playerVSPlayer(string name, Grid[] playerGrid,Grid[] enemyGrid)
        {
            InitializeComponent();
            this.namePlayer = name;
             initiateSetup(playerGrid,enemyGrid);
            displayHighScores(loadHighScores());
        }

        private void initiateSetup(Grid[] userGrid, Grid[] enemyGrid)
        {
            compGrid = new Grid[100];
            CompGrid.Children.CopyTo(compGrid, 0);
            for (int i = 0; i < 100; i++)
            {
                compGrid[i].Tag = enemyGrid[i].Tag;
            }
            

            playerGrid = new Grid[100];
            PlayerGrid.Children.CopyTo(playerGrid, 0);

            for (int i = 0; i < 100; i++)
            {
                playerGrid[i].Background = userGrid[i].Background;
                playerGrid[i].Tag = userGrid[i].Tag;
            }
            btnAttack.IsEnabled = true;
        }

        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Grid square = (Grid)sender;
         
            switch (square.Tag.ToString()) 
            {
                case "water":
                    square.Tag = "miss";
                    square.Background = new SolidColorBrush(Colors.LightGray);
                    play(this, e);
                    return;
                case "miss":
                case "hit":
                    return;

            }
            square.Tag = "hit";
            lifePlayer--;
            square.Background = new SolidColorBrush(Colors.Red);
                      
            if (checkWin(lifePlayer) == true)
            {
                MessageBox.Show(namePlayer+" you win!");
                disableGrids();
                displayHighScores(saveHighScores(true));
               
            }
            play(this, e);

        }

        private bool checkWin(int life)
        {
            if (life == 0)
            {
                return true;
            }
            else
                return false;
        }

        private void disableGrids()
        {       
            foreach (var element in playerGrid)
            {
                if (element.Tag.Equals("water"))
                {
                    element.Background = new SolidColorBrush(Colors.LightGray);
                }
                element.IsEnabled = false;
            }
            clearTextBoxes();
            btnAttack.IsEnabled = false;
        }

        private string validateXCoordinate(string X)
        {
            if (X.Length != 1)
            {
                return "";
            }

            if (Char.IsLetter(X[0]))
            {
                return X;
            }
            return "";
        }

        private string validateYCoordinate(string Y)
        {
            if (Y.Length > 2 || Y == "")
            {
                return "";
            }

            if (int.Parse(Y) > 0 || int.Parse(Y) <= 10)
            {
                return Y;
            }
            return "";
        }

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            string X = validateXCoordinate(txtBoxX.Text);
            string Y = validateYCoordinate(txtBoxY.Text);
            int index = 0;

            if (X == "" || Y == "")
            {
                MessageBox.Show("Invalid value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            switch (X)
            {
                case "A":
                    index = 0;
                    break;
                case "B":
                    index = 10;
                    break;
                case "C":
                    index = 20;
                    break;
                case "D":
                    index = 30;
                    break;
                case "E":
                    index = 40;
                    break;
                case "F":
                    index = 50;
                    break;
                case "G":
                    index = 60;
                    break;
                case "H":
                    index = 70;
                    break;
                case "I":
                    index = 80;
                    break;
                case "J":
                    index = 90;
                    break;
            }
            index += int.Parse(Y) - 1;
            clearTextBoxes();
            gridMouseDown(playerGrid[index], null);

        }

        private void clearTextBoxes()
        {
            txtBoxX.Text = "";
            txtBoxY.Text = "";
        }
      
        private void btnLetter_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            txtBoxX.Text = button.Content.ToString();
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            txtBoxY.Text = button.Content.ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            string path = @"../../scores.txt";
            File.Delete(path);
            FileStream stream = File.Create(path);
            stream.Close();

            txtBlockNames.Text = "NAME";
            txtBlockWins.Text = "WINS";
            txtBlockLosses.Text = "LOSSES";
        }

        private List<string> saveHighScores(bool playerWins)
        {
            String filename = @"../../scores.txt";
            string[] user = { namePlayer, "0", "0" };
            string[] playerNames;
            int index;
            int wins = 0;
            int losses = 0;

            if (!File.Exists(filename))
            {
                FileStream stream = File.Create(filename);
                stream.Close();
            }

            List<string> players = new List<string>(File.ReadAllLines(filename));

            playerNames = new string[players.Count];

            for (index = 0; index < players.Count; index++)
            {
                playerNames[index] = players[index].Split(' ')[0];
            }
            index = binarySearch(playerNames, namePlayer);

            if (index > -1)
            {
                user = players[index].Split();
                players.RemoveAt(index);
            }
            else
            {
                index = -(index + 1);
            }
            if (playerWins == true)
            {
                wins = int.Parse(user[1]) + 1;
            }
            else
            {
                losses = int.Parse(user[2]) + 1;
            }
            players.Insert(index, namePlayer + " " + wins + " " + losses);

            File.WriteAllLines(filename, players);
            return players;
        }
        private int binarySearch(string[] players, string value)
        {

            int low = 0;
            int high = players.Length - 1;

            while (high >= low)
            {
                int middle = (low + high) / 2;

                if (players[middle].CompareTo(value) == 0)
                {
                    return middle;
                }
                if (players[middle].CompareTo(value) < 0)
                {
                    low = middle + 1;
                }
                if (players[middle].CompareTo(value) > 0)
                {
                    high = middle - 1;
                }
            }
            return -(low + 1);
        }

        private List<string> loadHighScores()
        {
            String filename = @"../../scores.txt";
            string[] playerNames;
            int index;

            if (!File.Exists(filename))
            {
                FileStream stream = File.Create(filename);
                stream.Close();
            }

            List<string> players = new List<string>(File.ReadAllLines(filename));

            playerNames = new string[players.Count];

            for (index = 0; index < players.Count; index++)
            {
                playerNames[index] = players[index].Split(' ')[0];
            }

            File.WriteAllLines(filename, players);
            return players;
        }

        private void displayHighScores(List<string> players)
        {
            string[] player;
            string names = "Name" + Environment.NewLine;
            string wins = "Wins" + Environment.NewLine;
            string losses = "Losses" + Environment.NewLine;

            for (int i = 0; i < players.Count; i++)
            {
                player = players[i].Split(' ');
                names += player[0] + Environment.NewLine;
                wins += player[1] + Environment.NewLine;
                losses += player[2] + Environment.NewLine;
            }
            txtBlockNames.Text = names;
            txtBlockWins.Text = wins;
            txtBlockLosses.Text = losses;
        }
    }
}
