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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private Window parent;
        public Home()
        {
            InitializeComponent();
        }

        public Home(Window parent):this()
        {
            this.parent = parent;
            (parent as MainWindow).setWindowSize(this.Height, this.Width);
        }

        private void login_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.Login(parent));
        }

        private void register_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.SignupPatient(parent));
        }
    }
}
