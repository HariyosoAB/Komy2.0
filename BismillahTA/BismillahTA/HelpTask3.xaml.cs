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
using System.Windows.Shapes;

namespace BismillahTA
{
    /// <summary>
    /// Interaction logic for HelpTask3.xaml
    /// </summary>
    public partial class HelpTask3 : Window
    {
        public HelpTask3()
        {
            InitializeComponent();
            fillLabel();
        }

        private void fillLabel()
        {
            DescriptionLabel.Content = "Level ini adalah level untuk melatih gerakan kaki, seperti jalan ditempat. Tujuan dari level ini adalah membiasakan pasien\n"
                                        + "melakukan gerakan kaki secara bebas ditempat.\n";

            InstructionLabel.Content = "1. Mempersiapkan posisi dalam keadaan berdiri pada jarak +- 2m dari kinect \n"
                                        + "2. Menekan tombol MULAI\n"
                                        + "3. Menghilangkan objek berwarna hijau dengan kaki kanan atau kaki kiri\n"
                                        + "4. Melakukan gerakan ini hingga waktu selesai\n";
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
