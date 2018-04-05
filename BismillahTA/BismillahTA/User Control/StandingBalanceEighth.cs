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
    public class StandingBalanceEighth : BaseUserControl
    {
        private int tempScore = 0;
        private Joint item;
        private Joint shoulderRightJoint;

        public StandingBalanceEighth() : base()
        {
            goalPoints = 3;
        }

        public StandingBalanceEighth(MainWindow prnt) : base(prnt)
        {
            instructionLabel.Content = "Posisi berdiri dengan pada jarak +- 3m dari Kinect";
        }


        protected override void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            base.newSensor_AllFramesReady(sender, e);

            Skeleton detectedSkeleton = GetfirstSkeleton(e);
            if (detectedSkeleton == null)
                return;

            shoulderRightJoint = detectedSkeleton.Joints[JointType.ShoulderRight];

            this.ProcessGesture();

        }

        protected override void nextButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        protected override void ProcessGesture()
        {

            Score();
        }

        private void Score()
        {
            intersect = time;
            if (supervised == false)
            {
                tempScore += 25;
            }
            else
            {
                tempScore = 25;
            }
            score.Text = tempScore.ToString();
        }

        protected override void countdown2_Tick(object sender, EventArgs e)
        {
            base.countdown2_Tick(sender, e);
            if(time > 0)
            {
                if (gotIt >= 1 && gotIt < goalPoints)
                {
                    if (time == intersect - 3)
                        Draw();

                    time--;
                }
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
                instructionLabel.Content = "Menghadap dan raih objek yang ada";
            }
        }

        protected override void Draw()
        {
            double distance;
            distance = 0;
            switch (gotIt)
            {
                case 0:
                    distance = 0.05;
                    break;
                case 1:
                    distance = 0.12;
                    break;
                case 2:
                    distance = 0.25;
                    break;
                default:
                    break;
            }
            if(shoulderRightJoint != null)
            {
                var movedPosition = new SkeletonPoint
                {
                    X = (float)(shoulderRightJoint.Position.X + Properties.Settings.Default.rightArmLengthMeter + distance),
                    Y = (float)(shoulderRightJoint.Position.Y)
                };

                item = new Joint
                {
                    Position = movedPosition
                };

                item = item.ScaleTo(980, 720, .9f, .9f);

            }
            
        }

        protected override void startButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            time = 125;
            time2 = 5;
            gotIt = 0;
            tempScore = 0;
            severalTries = false;
            independent = true;
            supervised = false;
            score.Text = "0";
            start = true;
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
