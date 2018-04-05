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
    /// Interaction logic for login2.xaml
    /// </summary>
    public partial class login2 : UserControl
    {
        MainWindow parent;

        SqlConnector con = new SqlConnector();

        public login2()
        {
            InitializeComponent();
        }

        public login2(MainWindow parent):this()
        {
            this.parent = parent;
            con.getItemComboBox(comboTherapist);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool count = con.Login(username.Text, password.Password);
            if (count == true)
            {
                if (comboTherapist.Text != "")
                {
                    Properties.Settings.Default.user = username.Text;
                    Properties.Settings.Default.terapis = comboTherapist.Text;
                    Properties.Settings.Default.Save();

                    (parent as MainWindow).getUserControl(new User_Control.Menu(parent as MainWindow));
                }
                else
                    errorLabel.Content = "*Isikan nama terapis";
            }
            else
                errorLabel.Content = "*Username dan password tidak sesuai";
        }

    }
}
