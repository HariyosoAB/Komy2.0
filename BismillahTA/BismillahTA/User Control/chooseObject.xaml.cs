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

namespace BismillahTA.User_Control
{
    /// <summary>
    /// Interaction logic for chooseObject.xaml
    /// </summary>
    public partial class chooseObject : UserControl
    {
        MainWindow parent;

        public chooseObject()
        {
            InitializeComponent();
        }

        public chooseObject(MainWindow prnt):this()
        {
            this.parent = prnt;
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void strawberry_MouseUp(object sender, MouseButtonEventArgs e)
        {
            fruitLabel.Content = "Stroberi";
            Properties.Settings.Default.fruit = "strawberry.png";
        }

        private void apple_MouseUp(object sender, MouseButtonEventArgs e)
        {
            fruitLabel.Content = "Apel";
            Properties.Settings.Default.fruit = "apple.png";
        }

        private void banana_MouseUp(object sender, MouseButtonEventArgs e)
        {
            fruitLabel.Content = "Pisang";
            Properties.Settings.Default.fruit = "banana.png";
        }
       
        private void orange_MouseUp(object sender, MouseButtonEventArgs e)
        {
            fruitLabel.Content = "Jeruk";
            Properties.Settings.Default.fruit = "orange.png";
        }

        private void butterfly_MouseUp(object sender, MouseButtonEventArgs e)
        {
            animalLabel.Content = "Kupu-Kupu";
            Properties.Settings.Default.animal = "butterfly.png";
        }

        private void elephant_MouseUp(object sender, MouseButtonEventArgs e)
        {
            animalLabel.Content = "Gajah";
            Properties.Settings.Default.animal = "elephant.png";
        }

        private void duck_MouseUp(object sender, MouseButtonEventArgs e)
        {
            animalLabel.Content = "Bebek";
            Properties.Settings.Default.animal = "duck.png";
        }

        private void dolphin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            animalLabel.Content = "Lumba-lumba";
            Properties.Settings.Default.animal = "dolphin.png";
        }
        
        private void basket_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ballonLabel.Content = "Basket";
            Properties.Settings.Default.ball = "basket.png";
        }
        
        private void nextButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.fruit == null || Properties.Settings.Default.animal == null || Properties.Settings.Default.ball == null)
            {
                MessageBox.Show("Setiap jenis objek harus dipilih");
            }
            else
            {
                Properties.Settings.Default.Save();
                (parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));
            }
        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.Calibration(parent));
        }

        private void volley_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ballonLabel.Content = "Voli";
            Properties.Settings.Default.ball = "volley.png";
        }

        private void football_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ballonLabel.Content = "Sepak";
            Properties.Settings.Default.ball = "football.png";
        }

        private void tennis_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ballonLabel.Content = "Tenis";
            Properties.Settings.Default.ball = "tennis.png";
        }

        private void backButton_MouseEnter(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void backButton_MouseLeave(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void nextButton_MouseEnter(object sender, MouseEventArgs e)
        {
            nextLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void nextButton_MouseLeave(object sender, MouseEventArgs e)
        {
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
