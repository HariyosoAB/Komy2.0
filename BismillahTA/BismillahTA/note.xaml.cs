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
    /// Interaction logic for note.xaml
    /// </summary>
    public partial class note : Window
    {
        public note()
        {
            InitializeComponent();
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            if (noteText.Text == null || noteText.Text == "")
            {
                MessageBox.Show("Anda belum memasukan catatan");
            }
            else
            {
                MessageBox.Show("Catatan berhasil ditambahkan");
                Properties.Settings.Default.note = noteText.Text;
                this.Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            noteText.Text = null;
            this.Close();
        }

    }
}
