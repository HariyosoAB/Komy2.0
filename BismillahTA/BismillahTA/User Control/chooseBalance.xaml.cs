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
    /// Interaction logic for chooseBalance.xaml
    /// </summary>
    public partial class chooseBalance : UserControl
    {
        MainWindow parent;

        public chooseBalance()
        {
            InitializeComponent();
        }
        public chooseBalance(MainWindow parent): this()
        {
            this.parent = parent;
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void task1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.TrunkControlTask(parent, 1));
        }
        private void task2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.FirstStandingBalance(parent, 1));

        }

        private void backButton_MouseUp(object sender, MouseEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));

        }

        private void backButton_MouseEnter(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void backButton_MouseLeave(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
