using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        SqlConnector con = new SqlConnector();

        public Profile()
        {
            InitializeComponent();
            savePatientButton.Visibility = System.Windows.Visibility.Hidden;
            saveTherapistButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dtP = con.getPatientProfile();
            DataTable dtT = con.getTherapistProfile();
            string genderP;
            string genderT;

            patientName.IsReadOnly = true;
            patientAge.IsReadOnly = true;
            patientGender.IsReadOnly = true;
            patientAddress.IsReadOnly = true;
            therapistName.IsReadOnly = true;
            therapistAge.IsReadOnly = true;
            therapistGender.IsReadOnly = true;
            therapistAddress.IsReadOnly = true;
            therapistContact.IsReadOnly = true;

            patientName.Text = dtP.Rows[0].ItemArray[0].ToString();
            patientAge.Text = dtP.Rows[0].ItemArray[1].ToString();
            genderP = dtP.Rows[0].ItemArray[2].ToString();
            patientAddress.Text = dtP.Rows[0].ItemArray[3].ToString();

            if (genderP == "True")
                patientGender.Text = "Laki-Laki";
            else
                patientGender.Text = "Perempuan";

            if (Properties.Settings.Default.terapis == "--Keluarga--")
            {
                labelNameTherapist.Content = "KELUARGA";
                labelGenderTherapist.Visibility = System.Windows.Visibility.Hidden;
                labelAgeTherapist.Visibility = System.Windows.Visibility.Hidden;
                labelContactTherapist.Visibility = System.Windows.Visibility.Hidden;
                labelAddressTherapist.Visibility = System.Windows.Visibility.Hidden;
                therapistAddress.Visibility = System.Windows.Visibility.Hidden;
                therapistAge.Visibility = System.Windows.Visibility.Hidden;
                therapistContact.Visibility = System.Windows.Visibility.Hidden;
                therapistGender.Visibility = System.Windows.Visibility.Hidden;
                therapistName.Visibility = System.Windows.Visibility.Hidden;
                changeTherapistButton.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                therapistName.Text = dtT.Rows[0].ItemArray[0].ToString();
                therapistAge.Text = dtT.Rows[0].ItemArray[1].ToString();
                genderT = dtT.Rows[0].ItemArray[2].ToString();
                therapistAddress.Text = dtT.Rows[0].ItemArray[3].ToString();
                therapistContact.Text = dtT.Rows[0].ItemArray[4].ToString();

                if (genderT == "True")
                    therapistGender.Text = "Laki-laki";
                else
                    therapistGender.Text = "Perempuan";
            }

        }

        private void changePatientButton_Click(object sender, RoutedEventArgs e)
        {
            patientAddress.IsReadOnly = false;
            patientName.IsReadOnly = false;
            patientAge.IsReadOnly = false;
            savePatientButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void changeTherapistButton_Click(object sender, RoutedEventArgs e)
        {
            therapistAddress.IsReadOnly = false;
            therapistName.IsReadOnly = false;
            therapistAge.IsReadOnly = false;
            therapistContact.IsReadOnly = false;
            saveTherapistButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void savePatientButton_Click(object sender, RoutedEventArgs e)
        {
            bool status = con.updatePatient(patientName.Text, int.Parse(patientAge.Text), patientAddress.Text);

            if (status == true)
                MessageBox.Show("Data Pasien Berhasil Diubah");
            else
                MessageBox.Show("ERROR!!");
        }

        private void saveTherapistButton_Click(object sender, RoutedEventArgs e)
        {
            bool status = con.updateTherapist(therapistName.Text, int.Parse(therapistAge.Text), therapistAddress.Text, therapistContact.Text);

            if (status == true)
                MessageBox.Show("Data Pasien Berhasil Diubah");
            else
                MessageBox.Show("ERROR!!");
        }
    }
}
