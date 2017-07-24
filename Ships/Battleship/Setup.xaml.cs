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

    public enum Difficulty { Simple }

    public partial class Setup : UserControl
    {

        public event EventHandler play;
        public event EventHandler playMulti;
        public string name;
        public Difficulty difficulty = Difficulty.Simple;

        public Setup()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            name = txtboxName.Text;
            if (name == "")
            {
                MessageBox.Show("You must enter a name", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                play(this,e);
            }
        }


        private void buttonStartMultiplayer_Click(object sender, RoutedEventArgs e)
        {
            Setup setup = new Setup();
            name = txtboxName.Text;
            if (name == "")
            {
                MessageBox.Show("You must enter a name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                playMulti(this, e);
                //playMulti(this, new RoutedEventArgs());


              //  NavigationController.NavigateTo(new ShipPlacement());

                //ShipPlacement p = new ShipPlacement();
                //var np = new ShipPlacement();
               


                //Content = p.Content;
                //this.gridMain.Children.Add(p);

                //Window w = new Window();
                //w.Content = new ShipPlacement();
                //w.Show();
                //NavigationService.GetNavigationService(this).Navigate(p);
            }
        }

       
    }
  
}
