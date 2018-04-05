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
    /// Interaction logic for HelpTask1.xaml
    /// </summary>
    public partial class HelpTask1 : Window
    {
        public HelpTask1()
        {
            InitializeComponent();

            fillLabel();
        }

        private void fillLabel()
        {
            DescriptionLabel.Content = "Level ini adalah level untuk melatih gerakan dari duduk ke berdiri. Tujuan dari level ini adalah membiasakan pasien\n"
                                        + "melakukan gerakan dari duduk ke berdiri, sebelum melakukan pelatihan lain dalam posisi berdiri ataupun berjalan.\n";

            InstructionLabel.Content = "1. Mempersiapkan posisi dalam keadaan duduk \n"
                                        + "2. Menyatukan tangan di depan dada\n"
                                        + "3. Menekan tombol MULAI\n"
                                        + "4. Melakukan gerakan berdiri perlahan-lahan untuk menyundul gambar\n"
                                        + "5. Duduk kembali perlahan-lahan, dan ulangi nomer 4 dan 5 sampai 3 kali\n";
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
