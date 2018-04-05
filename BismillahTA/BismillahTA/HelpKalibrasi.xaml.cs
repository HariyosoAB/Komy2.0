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
    /// Interaction logic for HelpKalibrasi.xaml
    /// </summary>
    public partial class HelpKalibrasi : Window
    {
        public HelpKalibrasi()
        {
            InitializeComponent();
            fillLabel();
        }

        private void fillLabel()
        {
            DescriptionLabel.Content = "Kalibrasi bertujuan untuk mengambil koordinat kepala, kaki kanan, dan kaki kiri\n"
                                        + "untuk digunakan pada pelatihan pasien\n";

            InstructionLabel.Content = "1. Mempersiapkan posisi dalam keadaan berdiri pada jarak 3 - 4 m dari kinect \n"
                                        + "2. Menekan tombol MULAI\n"
                                        + "3. Membaca petunjuk yang tertera pada layar\n"
                                        + "4. Menekan tombol CAPTURE untuk menyimpan koordinat\n"
                                        + "5. Mengulangi langkah 2, 3, dan 4";
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
