using Microsoft.Win32;
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

    public partial class MainWindow : Window
    {
        Grid grid = new Grid();

        private Setup setup;
        private ShipPlacement shipPlacement;
        private PlayVSComp playVSComp;
        private secondSetup secondSetup;
        private ShipPlacement shipPlacementTwo;
        private ShipPlacement shipPlacementOne;
        private windowPlayerOne winPlayOne;
        private windowPlayerTwo winPlayTwo;
        public playerVSPlayer playerOne;
        public playerVSPlayer playerTwo;
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            playMusic();
            InitializeGame();
        }
  
        private void InitializeGame()
        {
          
            Content = grid;

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;
          
            setup = new Setup();
            grid.Children.Add(setup);           
            setup.play += new EventHandler(shipSetup);
            setup.playMulti += new EventHandler(secondSetupShip);
        }

        private void shipSetup(object sender, EventArgs e)
        {
            grid.Children.Clear();
            
            this.MinWidth = 460;
            this.MinHeight = 530;
            this.Width = 460;
            this.Height = 530;

            shipPlacement = new ShipPlacement();
            grid.Children.Add(shipPlacement);
            shipPlacement.play += new EventHandler(playGame);
            
        }

        private void playGame(object sender, EventArgs e)
        {

            grid.Children.Clear();

            this.MinWidth = 953.286;
            this.MinHeight = 480;
            this.Width = 953.286;
            this.Height = 480;

            playVSComp = new PlayVSComp(setup.difficulty, shipPlacement.playerGrid, setup.name);
            grid.Children.Add(playVSComp);
            playVSComp.replay += new EventHandler(replayGame);

        }

        private void replayGame(object sender, EventArgs e)
        {
            grid.Children.Clear();
            InitializeGame();
        }

        private void secondSetupShip(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;
          
            secondSetup = new secondSetup(setup.name);
            grid.Children.Add(secondSetup);
            secondSetup.playTwo += new EventHandler(winTwotoOne); 
        }
        private void winTwotoOne(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;

            winPlayOne = new windowPlayerOne();
            grid.Children.Add(winPlayOne);
            winPlayOne.playOne += new EventHandler(shipSetupOne);

        }
        private void shipSetupOne(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinWidth = 460;
            this.MinHeight = 530;
            this.Width = 460;
            this.Height = 530;

            shipPlacementOne = new ShipPlacement();
            grid.Children.Add(shipPlacementOne);
            shipPlacementOne.play += new EventHandler(winOnetoTwo);

        }
        private void winOnetoTwo(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;

            winPlayTwo = new windowPlayerTwo();
            grid.Children.Add(winPlayTwo);
            winPlayTwo.playTwo += new EventHandler(shipSetupTwo);
        }
        private void shipSetupTwo(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinWidth = 460;
            this.MinHeight = 530;
            this.Width = 460;
            this.Height = 530;

            shipPlacementTwo = new ShipPlacement();
            grid.Children.Add(shipPlacementTwo);
            shipPlacementTwo.play += new EventHandler(gameTwotoOne);

        }
        private void gameTwotoOne(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;

            winPlayOne = new windowPlayerOne();
            grid.Children.Add(winPlayOne);
            winPlayOne.playOne += new EventHandler(playGameMultiOne);

        }
        private void playGameMultiOne(object sender, EventArgs e)
        {

            grid.Children.Clear();

            this.MinWidth = 953.286;
            this.MinHeight = 480;
            this.Width = 953.286;
            this.Height = 480;

            playerOne = new playerVSPlayer(secondSetup.namePlayerOne, shipPlacementOne.playerGrid, shipPlacementTwo.playerGrid);      
            grid.Children.Add(playerOne);
            playerOne.play += new EventHandler(gameOnetoTwo);

        }
        private void gameOnetoTwo(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;

            winPlayTwo = new windowPlayerTwo();
            grid.Children.Add(winPlayTwo);
            winPlayTwo.playTwo += new EventHandler(playGameMultiTwo);
        }
        private void playGameMultiTwo(object sender, EventArgs e)
        {

            grid.Children.Clear();

            this.MinWidth = 953.286;
            this.MinHeight = 480;
            this.Width = 953.286;
            this.Height = 480;

            playerTwo = new playerVSPlayer(secondSetup.namePlayerTwo, shipPlacementTwo.playerGrid, shipPlacementOne.playerGrid);
            grid.Children.Add(playerTwo);
            playerTwo.play += new EventHandler(gameTwotoOneFinal);

        }
        private void gameTwotoOneFinal(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;

            winPlayOne = new windowPlayerOne();
            grid.Children.Add(winPlayOne);
            winPlayOne.playOne += new EventHandler(playGameMultiOneFinal);

        }
        private void playGameMultiOneFinal(object sender, EventArgs e)
        {

            grid.Children.Clear();

            this.MinWidth = 953.286;
            this.MinHeight = 480;
            this.Width = 953.286;
            this.Height = 480;

            MessageBox.Show("Your life is " + playerOne.lifePlayer.ToString());
            grid.Children.Add(playerOne);
            playerOne.play += new EventHandler(gameOnetoTwoFinal);

        }
        private void gameOnetoTwoFinal(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.MinHeight = 300;
            this.MinWidth = 330;
            this.Height = 300;
            this.Width = 330;

            winPlayTwo = new windowPlayerTwo();
            grid.Children.Add(winPlayTwo);
            winPlayTwo.playTwo += new EventHandler(playGameMultiTwoFinal);
        }
        private void playGameMultiTwoFinal(object sender, EventArgs e)
        {

            grid.Children.Clear();

            this.MinWidth = 953.286;
            this.MinHeight = 480;
            this.Width = 953.286;
            this.Height = 480;

            MessageBox.Show("Your life is " + playerTwo.lifePlayer.ToString());
            grid.Children.Add(playerTwo);
            playerTwo.play += new EventHandler(gameTwotoOneFinal);

        }
        private void playMusic()
        {
            mediaPlayer.Open(new Uri(Directory.GetCurrentDirectory() + "\\Sounds\\music.mp3"));
            mediaPlayer.Volume = 0.02;
            mediaPlayer.Play();
            mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
        }


        private void Media_Ended(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
