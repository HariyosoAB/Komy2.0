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

using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;
using System.Windows.Threading;
using System.Media;


namespace BismillahTA.User_Control
{
    public class StandingBalanceSecond : BaseUserControl
    {
        int initTime = 120;
        bool fall = false;
        public StandingBalanceSecond() : base()
        {
             
        }

        public StandingBalanceSecond(MainWindow prnt) : base(prnt)
        {
            instructionLabel.Content = "Posisi berdiri pada jarak +- 3m dari Kinect";
        }
   

 
        protected override void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            base.newSensor_AllFramesReady(sender, e);
            this.ProcessGesture();
        }

        protected override void nextButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        protected override void ProcessGesture()
        {
            double headY = Canvas.GetTop(head);
            double fallDetect = (Properties.Settings.Default.yCoordHead + Math.Abs(Properties.Settings.Default.yCoordHead - Properties.Settings.Default.yCoordFoot) * 0.25);

            if (headY > fallDetect)
            {
                instructionLabel.Content = "Jatuh terdeteksi";
                severalTries = true;
                fall = true;
            }
            else
            {
                if (fall == true)
                {
                    time = 120;
                    fall = false;
                }
            }

            Score();
        }

        private void Score()
        {
            switch (initTime - time)
            {
                case 30:
                    if (severalTries == true)
                    {
                        score.Text = "25";
                    }
                    else
                    {
                        score.Text = "50";
                    }
                    PlaySound(1);
                    break;
                case 120:
                    score.Text = "100";
                    countdown2.Stop();
                    rectSuccess.Visibility = System.Windows.Visibility.Visible;
                    textSuccess.Visibility = System.Windows.Visibility.Visible;
                    PlaySound(3);
                    instructionLabel.Visibility = System.Windows.Visibility.Hidden;
                    break;
                default:
                    break;
            }

        }
        protected override void countdown2_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {
                timer.Text = string.Format("0{0} : 0{1}", time / 60, time % 60);
                time--;
            }
            else
            {
                countdown2.Stop();
                instructionLabel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        protected override void timer_Tick(object sender, EventArgs e)
        {
            timer2.Visibility = System.Windows.Visibility.Visible;
            if (time2 >= 0)
            {
                if (time2 <= 5)
                {
                    timer2.Text = string.Format("{0}", time2 % 60);
                    time2--;
                }
            }
            else
            {
                countdown.Stop();
                timer2.Visibility = System.Windows.Visibility.Collapsed;
                Draw();
                instructionLabel.Content = "Tahan posisi berdiri selama 2 menit";
            }
        }

        protected override void startButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            time = 125;
            time2 = 5;
            score.Text = "0";
            start = true;
            severalTries = false;
            independent = true;
            supervised = false;
            Properties.Settings.Default.note = null;
            rectSuccess.Visibility = textSuccess.Visibility = System.Windows.Visibility.Hidden;
            noteButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/notes.png"));
            saveButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/save.png"));

            if (mainCanvas.Children != null)
                mainCanvas.Children.Remove(pict);

            if (countdown != null)
                countdown.Stop();

            if (countdown2 != null)
                countdown2.Stop();

            countdown = new DispatcherTimer();
            countdown.Interval = new TimeSpan(0, 0, 1);
            countdown.Tick += new EventHandler(timer_Tick);
            countdown.Start();

            countdown2 = new DispatcherTimer();
            countdown2.Interval = new TimeSpan(0, 0, 1);
            countdown2.Tick += new EventHandler(countdown2_Tick);
            countdown2.Start();
        }
    }
}
