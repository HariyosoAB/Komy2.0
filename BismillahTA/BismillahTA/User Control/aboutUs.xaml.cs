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
    /// Interaction logic for aboutUs.xaml
    /// </summary>
    public partial class aboutUs : UserControl
    {
        public aboutUs()
        {
            InitializeComponent();
            fillLabel();
        }

        private void fillLabel()
        {
            aboutUsText.Text = "Komy (Kinect for Post-stroke Motoric Therapy)\n"
                                    + "adalah sebuah aplikasi yang digunakan sebagai alternatif "
                                    + "terapi mandiri yang bisa dilakukan di rumah. "
                                    + "Aplikasi ini ditujukan untuk penderita pasca stroke ringan "
                                    + "yang sudah dapat berdiri dengan atau tanpa alat bantu.";
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Video videoTutorial = new Video();

            videoTutorial.ShowDialog();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}
