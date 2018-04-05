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
    public partial class FirstPage : UserControl {

        MainWindow parent;
        SqlConnector con = new SqlConnector();

        public FirstPage()
        {
            InitializeComponent();
        }

        public FirstPage(MainWindow parent):this()
        {
            this.parent = parent;
            loginLabel.Visibility = System.Windows.Visibility.Hidden;
            signupLabel.Visibility = System.Windows.Visibility.Hidden;
            aboutUsLabel.Visibility = System.Windows.Visibility.Hidden;
            GetUserControlinFrame(new User_Control.aboutUs());
        }

        private void GetUserControlinFrame(UserControl control)
        {
            this.childrenFrame.Content = control;
        }

        private void loginButton_MouseEnter(object sender, MouseEventArgs e) {
            loginLabel.Visibility = System.Windows.Visibility.Visible;
            rect1.Opacity = 0.4;
        }
        private void loginButton_MouseLeave(object sender, MouseEventArgs e)
        {
            loginLabel.Visibility = System.Windows.Visibility.Hidden;
            rect1.Opacity = 0.6;
        }
        private void loginButton_MouseUp(object sender, MouseEventArgs e)
        {
            GetUserControlinFrame(new User_Control.login2(this.parent));

        }
        private void signupButton_MouseEnter(object sender, MouseEventArgs e)
        {
            signupLabel.Visibility = System.Windows.Visibility.Visible;
            rect2.Opacity = 0.4;

        }
        private void signupButton_MouseLeave(object sender, MouseEventArgs e)
        {
            signupLabel.Visibility = System.Windows.Visibility.Hidden;
            rect2.Opacity = 0.6;

        }
        private void signupButton_MouseUp(object sender, MouseEventArgs e)
        {
            GetUserControlinFrame(new User_Control.Signup());

        }
        private void aboutUsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            aboutUsLabel.Visibility = System.Windows.Visibility.Visible;
            rect3.Opacity = 0.4;

        }
        private void aboutUsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutUsLabel.Visibility = System.Windows.Visibility.Hidden;
            rect3.Opacity = 0.6;
        }
        private void aboutUsButton_MouseUp(object sender, MouseEventArgs e)
        {
            GetUserControlinFrame(new User_Control.aboutUs());
        }
    }
}