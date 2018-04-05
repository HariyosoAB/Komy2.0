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
    /// Interaction logic for HelpTask2.xaml
    /// </summary>
    public partial class HelpTask2 : Window
    {
        public HelpTask2()
        {
            InitializeComponent();
            fillLabel();
        }

        private void fillLabel()
        {
            DescriptionLabel.Content = "Level ini adalah level untuk melatih pergerakan kaki ke samping dalam posisi berdiri. Tujuan dari \n"
                                        + "pelatihan ini adalah untuk membiasakan melakukan gerakan kaki dan melatih keseimbangan saat berdiri.";

            InstructionLabel.Content = "1. Mempersiapkan posisi dalam keadaan berdiri \n"
                                        + "2. Menekan tombol MULAI\n"
                                        + "3. Menggerakan kaki kanan kesamping kanan untuk menendang bola yang ada di samping kanan\n"
                                        + "4. Menggerakan kaki kiri ke samping kiri untuk menendang bola yang ada di samping kiri\n"
                                        + "5. Lakukan langkah 3 dan 4 sebanyak mungkin dalam waktu 1 menit";
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
