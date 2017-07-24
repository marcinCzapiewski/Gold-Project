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

namespace Battleship
{

    public partial class PlayVSComp : UserControl
    {
        public event EventHandler replay;

        public Difficulty difficulty;
        public string playerName;
        public int highScore;
        public Grid[] playerGrid;
        public Grid[] compGrid;
        int turnCount = 0;
        public Random random = new Random();

        int counterPlayer = 20;
        int counterComp = 20;




        public PlayVSComp(Difficulty difficulty, Grid[] playerGrid, string playerName)
        {
            InitializeComponent();

            this.playerName = playerName;
            this.difficulty = difficulty;
            initiateSetup(playerGrid);
            displayHighScores(loadHighScores());

        }


        private void initiateSetup(Grid[] userGrid)
        {
            compGrid = new Grid[100];
            CompGrid.Children.CopyTo(compGrid, 0);
            for (int i = 0; i < 100; i++)
            {
                compGrid[i].Tag = "water";
            }
            setupCompGrid();
            playerGrid = new Grid[100];
            PlayerGrid.Children.CopyTo(playerGrid, 0);

            for (int i = 0; i < 100; i++)
            {
                playerGrid[i].Background = userGrid[i].Background;
                playerGrid[i].Tag = userGrid[i].Tag;
            }
            btnAttack.IsEnabled = true;
        }


        private void setupCompGrid()
        {
            Random random = new Random();
            int[] shipSizes = new int[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
            string[] ships = new string[]   { "Firstdestroyer","Seconddestroyer", "Thirddestroyer", "Fourthdestroyer",
                "Firstcruiser", "Secondcruiser", "Thirdcruiser", "Firstsubmarine", "Secondsubmarine", "battleship" };
            int size, index;
            string ship;
            Orientation orientation;
            bool unavailableIndex = true;

            for (int i = 0; i < shipSizes.Length; i++)
            {
                size = shipSizes[i];
                ship = ships[i];
                unavailableIndex = true;

                if (random.Next(0, 2) == 0)
                    orientation = Orientation.Horizontal;
                else
                    orientation = Orientation.Vertical;

                if (orientation.Equals(Orientation.Horizontal))
                {
                    index = random.Next(0, 100);
                    while (unavailableIndex == true)
                    {
                        unavailableIndex = false;

                        while ((index + size - 1) % 10 < size - 1)
                        {
                            index = random.Next(0, 100);
                        }

                        for (int j = 0; j < size; j++)
                        {
                            if (index + j > 99 || !compGrid[index + j].Tag.Equals("water"))
                            {
                                index = random.Next(0, 100);
                                unavailableIndex = true;
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < size; j++)
                    {
                        compGrid[index + j].Tag = ship;

                    }
                }
                else
                {
                    index = random.Next(0, 100);
                    while (unavailableIndex == true)
                    {
                        unavailableIndex = false;

                        while (index / 10 + size * 10 > 100)
                        {
                            index = random.Next(0, 100);
                        }

                        for (int j = 0; j < size * 10; j += 10)
                        {
                            if (index + j > 99 || !compGrid[index + j].Tag.Equals("water"))
                            {
                                index = random.Next(0, 100);
                                unavailableIndex = true;
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < size * 10; j += 10)
                    {
                        compGrid[index + j].Tag = ship;
                    }
                }

            }


        }


        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {

            Grid square = (Grid)sender;

            if (turnCount % 2 != 0)
            {
                return;
            }

            switch (square.Tag.ToString())
            {
                case "water":
                    square.Tag = "miss";
                    square.Background = new SolidColorBrush(Colors.LightGray);
                    turnCount++;
                    compTurn();
                    return;
                case "miss":
                case "hit":
                    Console.WriteLine("User hit a miss/hit");
                    return;

            }
            square.Tag = "hit";
            counterComp--;
            square.Background = new SolidColorBrush(Colors.Red);
            turnCount++;
            checkPlayerWin();
            compTurn();

        }





        private void compTurn()
        {

            hunterMode();
            turnCount++;
            checkComputerWin();
        }
        private void checkPlayerWin()
        {
            if (counterComp == 0)
            {
                MessageBox.Show("You win!");
                disableGrids();
                displayHighScores(saveHighScores(true));
            }
        }



        private void checkComputerWin()
        {
            if (counterPlayer == 0)
            {
                MessageBox.Show("You lose!");
                disableGrids();
                displayHighScores(saveHighScores(false));
            }
        }
        private void disableGrids()
        {
            foreach (var element in compGrid)
            {
                if (element.Tag.Equals("water"))
                {
                    element.Background = new SolidColorBrush(Colors.LightGray);
                }

                element.IsEnabled = false;
            }
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
            gridMouseDown(compGrid[index], null);

        }

        private void clearTextBoxes()
        {
            txtBoxX.Text = "";
            txtBoxY.Text = "";
        }
        private void btnStartOver_Click(object sender, RoutedEventArgs e)
        {
            replay(this, e);
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





        private void hunterMode()
        {
            int position;
            do
            {
                position = random.Next(100);
                Console.WriteLine(playerGrid[position].Tag);
                Console.WriteLine("Randomizing position");
            } while ((playerGrid[position].Tag.Equals("miss")) || (playerGrid[position].Tag.Equals("hit")));

            simpleMode(position);

        }


        private void simpleMode(int position)
        {
            if (!(playerGrid[position].Tag.Equals("water")))
            {

                counterPlayer--;
                playerGrid[position].Tag = "hit";
                playerGrid[position].Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                playerGrid[position].Tag = "miss";
                playerGrid[position].Background = new SolidColorBrush(Colors.LightGray);
            }
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
            string[] user = { playerName, "0", "0" };
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
            index = binarySearch(playerNames, playerName);

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
            players.Insert(index, playerName + " " + wins + " " + losses);

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
