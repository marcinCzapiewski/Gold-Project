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
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Logika interakcji dla klasy secondSetup.xaml
    /// </summary>
    /// 



    public partial class secondSetup : UserControl
    {
        public event EventHandler playTwo;
        public string namePlayerTwo;

        public secondSetup()
        {
            InitializeComponent();
        }

        private void buttonNextPage_Click(object sender, RoutedEventArgs e)
        {
            namePlayerTwo = txtboxName.Text;
            if (namePlayerTwo == "")
            {
                MessageBox.Show("You must enter a name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                playTwo(this, e);
            }
        }
    }
}
