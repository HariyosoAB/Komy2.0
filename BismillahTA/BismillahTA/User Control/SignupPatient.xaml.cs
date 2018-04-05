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
    /// Interaction logic for SignupPatient.xaml
    /// </summary>
    public partial class SignupPatient : UserControl
    {
        private Window parent;

        public SignupPatient()
        {
            InitializeComponent();
        }

        public SignupPatient(Window parent):this()
        {
            this.parent = parent;
            (parent as MainWindow).setWindowSize(this.Height, this.Width);
        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //(parent as MainWindow).getUserControl(new User_Control.Home(parent));
        }

        private void loginButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (patientName.Text != "" && age.Text != "" && username.Text != "" && password.Text != "")
                MessageBox.Show("Data pasien berhasil ditambahkan");
            else
                label.Content = "* Harap isikan seluruh data";
        }

    }
}
