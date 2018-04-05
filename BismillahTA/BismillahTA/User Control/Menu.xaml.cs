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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        MainWindow parent;
        SqlConnector con = new SqlConnector();

        public Menu()
        {
            InitializeComponent();
        }

        public Menu(MainWindow parent):this()
        {
            this.parent = parent;
            nameLabel.Text = "Selamat datang " + con.GetName();
            logoutLabel.Visibility = System.Windows.Visibility.Hidden;
            startLabel.Visibility = System.Windows.Visibility.Hidden;
            reportLabel.Visibility = System.Windows.Visibility.Hidden;
            profileLabel.Visibility = System.Windows.Visibility.Hidden;
            tutorialLabel.Visibility = System.Windows.Visibility.Hidden;
            GetUserControlinFrame(new User_Control.Profile());
        }

        private void GetUserControlinFrame(UserControl control)
        {
            this.menuFrame.Content = control;
        }

        private void logoutButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings.Default.user = null;
            (parent as MainWindow).getUserControl(new User_Control.FirstPage(parent));
        }

        private void startButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.Calibration(parent));
            //(parent as MainWindow).getUserControl(new User_Control.SecondStandingBalance(parent));
        }

        private void grafikReportButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetUserControlinFrame(new User_Control.Report());
        }

        private void profileButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetUserControlinFrame(new User_Control.Profile());
        }

        private void logoutButton_MouseEnter(object sender, MouseEventArgs e)
        {
            logoutLabel.Visibility = System.Windows.Visibility.Visible;
            rect1.Opacity = 0.4;
        }

        private void logoutButton_MouseLeave(object sender, MouseEventArgs e)
        {
            logoutLabel.Visibility = System.Windows.Visibility.Hidden;
            rect1.Opacity = 0.6;
        }

        private void startButton_MouseEnter(object sender, MouseEventArgs e)
        {
            startLabel.Visibility = System.Windows.Visibility.Visible;
            rect2.Opacity = 0.4;
        }

        private void startButton_MouseLeave(object sender, MouseEventArgs e)
        {
            startLabel.Visibility = System.Windows.Visibility.Hidden;
            rect2.Opacity = 0.6;
        }

        private void grafikReportButton_MouseEnter(object sender, MouseEventArgs e)
        {
            reportLabel.Visibility = System.Windows.Visibility.Visible;
            rect3.Opacity = 0.4;
        }

        private void grafikReportButton_MouseLeave(object sender, MouseEventArgs e)
        {
            reportLabel.Visibility = System.Windows.Visibility.Hidden;
            rect3.Opacity = 0.6;
        }

        private void profileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            profileLabel.Visibility = System.Windows.Visibility.Visible;
            rect4.Opacity = 0.4;
        }

        private void profileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            profileLabel.Visibility = System.Windows.Visibility.Hidden;
            rect4.Opacity = 0.6;
        }

        private void tutorialButton_MouseEnter(object sender, MouseEventArgs e)
        {
            tutorialLabel.Visibility = System.Windows.Visibility.Visible;
            rect5.Opacity = 0.4;
        }

        private void tutorialButton_MouseLeave(object sender, MouseEventArgs e)
        {
            tutorialLabel.Visibility = System.Windows.Visibility.Hidden;
            rect5.Opacity = 0.6;
        }

        private void tutorialButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetUserControlinFrame(new User_Control.videoTutorial());
        }
    }
}
