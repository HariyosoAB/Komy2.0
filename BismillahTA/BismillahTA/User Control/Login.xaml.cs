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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private Window parent;
        public Login()
        {
            InitializeComponent();
        }

        public Login(Window parent):this()
        {
            this.parent = parent;
            (parent as MainWindow).setWindowSize(this.Height, this.Width);
        }

        private void loginButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bool count = SqlConnector.Login(username.Text, password.Text);
            if (count == true)
            {
                Properties.Settings.Default.user = username.Text;
                (parent as MainWindow).getUserControl(new User_Control.Menu(parent as MainWindow));
            }
            else
                label.Content = "Username dan password tidak sesuai";
        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //(parent as MainWindow).getUserControl(new User_Control.Home(parent));
        }
    }
}
