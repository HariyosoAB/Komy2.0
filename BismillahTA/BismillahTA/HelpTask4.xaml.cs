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
    /// Interaction logic for HelpTask4.xaml
    /// </summary>
    public partial class HelpTask4 : Window
    {
        public HelpTask4()
        {
            InitializeComponent();

            fillLabel();
        }

        private void fillLabel()
        {
            DescriptionLabel.Content = "Level ini adalah level untuk melatih pasien brjalan. Tujuan dari pelatihan ini adalah membiasakan \n"
                                        + "untuk berjalan maju.";

            InstructionLabel.Content = "1. Mempersiapkan posisi dalam keadaan berdiri pada jarak +- 2m stage 1, +- 3m stage 2, dan +- 4m stage 3\n"
                                        + "2. Menekan tombol MULAI\n"
                                        + "3. Berjalan maju dari garis MULAI sampai garis SELESAI\n"
                                        + "4. Pergerkan pasien dapat dilihat dari bola yang bergerak pada layar monitor\n";
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

