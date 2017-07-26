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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship
{

    public partial class ShipPlacement : UserControl
    {
        public event EventHandler play;
        
        enum Orientation { VERTICAL, HORIZONTAL};
        enum Marker {Water, Alive};
        Orientation orientation = Orientation.HORIZONTAL;
        SolidColorBrush unselected = new SolidColorBrush(Colors.Black);
        SolidColorBrush selected = new SolidColorBrush(Colors.Green);
        String ship = "";
        int size;
        int numShipsPlaced;
        Path lastShip;
        Path[] ships;
        Polygon lastArrow;
        public Grid[] playerGrid;


        SolidColorBrush[] shipColors = new SolidColorBrush[] {(SolidColorBrush)(new BrushConverter().ConvertFrom("#7FFFD4")), (SolidColorBrush)(new BrushConverter().ConvertFrom("#8A2BE2")),
                                                                  (SolidColorBrush)(new BrushConverter().ConvertFrom("#7FFF00")),(SolidColorBrush)(new BrushConverter().ConvertFrom("#FF7F50")),
                                                                  (SolidColorBrush)(new BrushConverter().ConvertFrom("#6495ED")),(SolidColorBrush)(new BrushConverter().ConvertFrom("#00008B")),
                                                                  (SolidColorBrush)(new BrushConverter().ConvertFrom("#006400")),
                                                                  (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1493")),(SolidColorBrush)(new BrushConverter().ConvertFrom("#FFD700")),
                                                                  (SolidColorBrush)(new BrushConverter().ConvertFrom("#D8BFD8"))};
        public ShipPlacement()
        {
            InitializeComponent();
            playerGrid = new Grid[] { gridA1, gridA2, gridA3, gridA4, gridA5, gridA6, gridA7,gridA8,gridA9,gridA10,
                                gridB1, gridB2, gridB3, gridB4, gridB5, gridB6, gridB7,gridB8,gridB9,gridB10,
                                gridC1, gridC2, gridC3, gridC4, gridC5, gridC6, gridC7,gridC8,gridC9,gridC10,
                                gridD1, gridD2, gridD3, gridD4, gridD5, gridD6, gridD7,gridD8,gridD9,gridD10,
                                gridE1, gridE2, gridE3, gridE4, gridE5, gridE6, gridE7,gridE8,gridE9,gridE10,
                                gridF1, gridF2, gridF3, gridF4, gridF5, gridF6, gridF7,gridF8,gridF9,gridF10,
                                gridG1, gridG2, gridG3, gridG4, gridG5, gridG6, gridG7,gridG8,gridG9,gridG10,
                                gridH1, gridH2, gridH3, gridH4, gridH5, gridH6, gridH7,gridH8,gridH9,gridH10,
                                gridI1, gridI2, gridI3, gridI4, gridI5, gridI6, gridI7,gridI8,gridI9,gridI10,
                                gridJ1, gridJ2, gridJ3, gridJ4, gridJ5, gridJ6, gridJ7,gridJ8,gridJ9,gridJ10 };
            ships = new Path[] {  };
            reset();
            
        }

  
        private void reset()
        {
            if (lastArrow != null)
            {
                lastArrow.Stroke = unselected;
            }
    

            foreach (var element in playerGrid)
            {
                element.Tag = "water";
                element.Background = new SolidColorBrush(Colors.White);
            }

            foreach (var element in ships)
            {
                element.IsEnabled = true;
                element.Opacity = 100;
                if (element.Stroke != unselected)
                {
                    element.Stroke = unselected;
                }
            }
            numShipsPlaced = 0;
            lastShip = null;
        }

   
     
       
      
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (numShipsPlaced != 10)
            {
                return;
            }
             play(this,e);
            //Window w = new Window();
            //w.Content = new Setup();
            //w.Show();


        }

      
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

   
        private void btnSetup_Click(object sender, RoutedEventArgs e)
        {
            reset();
            Random random = new Random();
            int[] shipSizes = new int[] {1,1,1,1,2,2,2,3,3,4};
            string[] shipNames = new string[] {"Firstdestroyer","Seconddestroyer", "Thirddestroyer", "Fourthdestroyer",
                "Firstcruiser", "Secondcruiser", "Thirdcruiser", "Firstsubmarine", "Secondsubmarine", "battleship" };
            int size, index;
            string ship;
            Orientation orientation;
            bool unavailableIndex = true;
            

            for (int i = 0; i < shipSizes.Length; i++)
            {
                
                size = shipSizes[i];
                ship = shipNames[i];
                unavailableIndex = true;

                if (random.Next(0, 2) == 0)
                    orientation = Orientation.HORIZONTAL;
                else
                    orientation = Orientation.VERTICAL;

          
                if (orientation.Equals(Orientation.HORIZONTAL))
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
                            if (index + j > 99 || !playerGrid[index + j].Tag.Equals("water"))
                            {
                                index = random.Next(0, 100);
                                unavailableIndex = true;
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < size; j++)
                    {
                        playerGrid[index + j].Tag = ship;
                        playerGrid[index + j].Background = shipColors[i];
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
                            if (index + j > 99 || !playerGrid[index + j].Tag.Equals("water"))
                            {
                                index = random.Next(0, 100);
                                unavailableIndex = true;
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < size * 10; j += 10)
                    {
                        playerGrid[index + j].Tag = ship;
                        playerGrid[index + j].Tag = Marker.Alive; // dodane
                        playerGrid[index + j].Background = shipColors[i];
                    }
                }

            }
            numShipsPlaced = 10;
            foreach (var element in ships)
            {
                element.IsEnabled = false;
                element.Opacity = .10;
                if (element.Stroke != unselected)
                {
                    element.Stroke = unselected;
                }

            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private bool checkShips(int index)
        {
            Grid[,]tab = new Grid[13, 13];
            

            foreach (var element in tab)
            {
                element.Tag = "water";
                element.Background = new SolidColorBrush(Colors.White);
            }
            // tu powinno byc podmienienie zeby tab = playerGrid, tylko ze 
            //trzba zamienic playerGrid tablice jednowymiarowa na tablice wielowymiarowa 10x10
            // i zeby to bylo w

            for (int i = 3; i < 13; i++)
                for (int j = 3; j < 13; j++)
                {
                    tab[i, j] = playerGrid[i];
                }
            for (int i = 3; i < 13; i++)
                for (int j = 3; j < 13; j++)
                {
                    if ((string)tab[i - 1, j - 1].Tag && (string)tab[i - 1, j + 1].Tag && (string)tab[i + 1, j - 1].Tag && (string)tab[i + 1, j + 1].Tag == "water";//playerGrid[index].Tag.Equals("water")) ; // funkcja sprawdzajaca rogi obiektu
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
        }



    }
}
