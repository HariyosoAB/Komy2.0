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

namespace BismillahTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Properties.Settings.Default.xCoordHead = 0;
            Properties.Settings.Default.yCoordHead = 0;
            Properties.Settings.Default.xCoordRightFoot = 0;
            Properties.Settings.Default.yCoordRightFoot = 0;
            Properties.Settings.Default.xCoordLeftFoot = 0;
            Properties.Settings.Default.yCoordLeftFoot = 0;
            Properties.Settings.Default.yCoordFoot = 0;
            Properties.Settings.Default.note = null;
            Properties.Settings.Default.animal = null;
            Properties.Settings.Default.fruit = null;
            Properties.Settings.Default.ball = null;

            getUserControl(new User_Control.FirstPage(this));
        }

        public void getUserControl(UserControl control)
        {
            this.mainframe.Content = control;
        }

        public void setWindowSize(double height, double width)
        {
            this.Width = width + 8;
            this.Height = height + 31;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
