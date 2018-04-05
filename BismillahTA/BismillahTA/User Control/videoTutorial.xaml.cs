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
    /// Interaction logic for videoTutorial.xaml
    /// </summary>
    public partial class videoTutorial : UserControl
    {
        public videoTutorial()
        {
            InitializeComponent();
            List<string> dataTaskCombo = new List<string>();
            dataTaskCombo.Add("Daftar dan Masuk");
            dataTaskCombo.Add("Halaman Pengguna");
            dataTaskCombo.Add("Pra Pelatihan");
            dataTaskCombo.Add("Pelatihan 1");
            dataTaskCombo.Add("Pelatihan 2");
            dataTaskCombo.Add("Pelatihan 3");
            dataTaskCombo.Add("Pelatihan 4");
            videoCombo.ItemsSource = dataTaskCombo;
            videoCombo.SelectedIndex = 1;

            videoElement.Volume = 100;
            videoElement.Width = 700;
            videoElement.Height = 440;
            videoElement.Source = new Uri(@"Video\HalamanPengguna-v1.0.mpeg", UriKind.Relative);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            videoElement.Play();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            videoElement.Pause();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            videoElement.Stop();
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            if (videoCombo.SelectedIndex == 0)
            {
                videoElement.Source = new Uri(@"Video\daftarDanMasuk-v1.0.mpeg", UriKind.Relative);
            }
            else if (videoCombo.SelectedIndex == 1)
            {
                videoElement.Source = new Uri(@"Video\HalamanPengguna-v1.0.mpeg", UriKind.Relative);
            }
            else if (videoCombo.SelectedIndex == 2)
            {
                videoElement.Source = new Uri(@"Video\praPelatihan-v1.0.mpeg", UriKind.Relative);
            }
            else if (videoCombo.SelectedIndex == 3)
            {
                videoElement.Source = new Uri(@"Video\level1-v1.0.mpeg", UriKind.Relative);
            }
            else if (videoCombo.SelectedIndex == 4)
            {
                videoElement.Source = new Uri(@"Video\level2-v1.0.mpeg", UriKind.Relative);
            }
            else if (videoCombo.SelectedIndex == 5)
            {
                videoElement.Source = new Uri(@"Video\level3-v1.0.mpeg", UriKind.Relative);
            }
            else if (videoCombo.SelectedIndex == 6)
            {
                videoElement.Source = new Uri(@"Video\level4-v1.0.mpeg", UriKind.Relative);
            }
        }

    }
}
