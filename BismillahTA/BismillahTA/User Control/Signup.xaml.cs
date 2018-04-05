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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : UserControl
    {
        SqlConnector con = new SqlConnector();
        bool gender;

        public Signup()
        {
            InitializeComponent();
        }

        private void userCombo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> dataCombo = new List<string>();
            dataCombo.Add("Pasien");
            dataCombo.Add("Terapis");

            userCombo.ItemsSource = dataCombo;
            userCombo.SelectedIndex = 0;
        }

        private void userCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(userCombo.SelectedIndex == 1)
            {
                passwordLabel.Content = "Telepon";
            }
            else if(userCombo.SelectedIndex == 0)
            {
                passwordLabel.Content = "Password";
            }
        }

        private void male_Checked(object sender, RoutedEventArgs e)
        {
            gender = true;
        }

        private void female_Checked(object sender, RoutedEventArgs e)
        {
            gender = false;
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            int success;

            if (userCombo.SelectedIndex == 0)
            {
                if (username.Text == "" || ages.Text == "" || address.Text == "" || username.Text == "" || password.Text == "" || (male.IsChecked == false && female.IsChecked == false))
                    errorLabel.Content = "*Data yang dimasukan tidak lengkap";
                else
                {
                    int result;
                    int.TryParse(ages.Text, out result);

                    if (result == 0)
                    {
                        errorLabel.Content = "*Isi usia dengan angka";
                    }
                    else
                    {
                        success = con.SignUp(nameUser.Text, int.Parse(ages.Text), gender, address.Text, username.Text, password.Text);

                        if (success == 1)
                        {
                            errorLabel.Content = "";
                            MessageBox.Show("Data Berhasil Ditambahkan");
                            nameUser.Clear();
                            ages.Clear();
                            address.Clear();
                            username.Clear();
                            password.Clear();
                            male.IsChecked = false;
                            female.IsChecked = false;
                        }
                        else if (success == 2)
                            errorLabel.Content = "*Username sudah terpakai, pilih username lain";
                        else
                            errorLabel.Content = "*ERROR !!";
                    }
                }
            }
            else
            {
                if (username.Text == "" || ages.Text == "" || address.Text == "" || username.Text == "" || (male.IsChecked == false && female.IsChecked == false))
                    errorLabel.Content = "*Data yang dimasukan tidak lengkap";
                else
                {
                    int result;
                    int.TryParse(ages.Text, out result);
                    if (result == 0)
                    {
                        errorLabel.Content = "*Isi usia dengan angka";
                    }
                    else
                    {
                        success = con.SignUpTherapist(nameUser.Text, username.Text, int.Parse(ages.Text), gender, address.Text, password.Text);

                        if (success == 1)
                        {
                            errorLabel.Content = "";
                            MessageBox.Show("Data Berhasil Ditambahkan");
                            nameUser.Clear();
                            ages.Clear();
                            address.Clear();
                            username.Clear();
                            password.Clear();
                            male.IsChecked = false;
                            female.IsChecked = false;
                        }
                        else if (success == 2)
                            errorLabel.Content = "*Username sudah terpakai, pilih username lain";
                        else
                            errorLabel.Content = "*ERROR !!";
                    }
                }
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            nameUser.Clear();
            ages.Clear();
            address.Clear();
            username.Clear();
            password.Clear();
            male.IsChecked = false;
            female.IsChecked = false;
            nameUser.Focus();
        }


    }
}
