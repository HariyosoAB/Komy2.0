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
    /// <summary>
    /// Interaction logic for BaseUserControl.xaml
    /// </summary>
    public partial class BaseUserControl : UserControl
    {

        MainWindow parent;
        protected int taskLevel;

        protected Skeleton[] allSkeletons = new Skeleton[6];
        protected int time;
        protected int time2;
        protected DispatcherTimer countdown;
        protected DispatcherTimer countdown2;
        protected Rectangle pict;
        protected int gotIt;
        protected int intersect;
        protected SoundPlayer soundEffect;

        protected int startTime = 165;
        protected int goalPoints = 1;

        protected bool start = false;
        protected bool severalTries = false;
        protected bool independent = true;
        protected bool supervised = false;



        public BaseUserControl()
        {
            InitializeComponent();
            score.Text = "0";
            homeLabel.Visibility = System.Windows.Visibility.Hidden;
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
            startLabel.Visibility = System.Windows.Visibility.Hidden;
            noteLabel.Visibility = System.Windows.Visibility.Hidden;
            saveLabel.Visibility = System.Windows.Visibility.Hidden;
            helpLabel.Visibility = System.Windows.Visibility.Hidden;
            rectSuccess.Visibility = System.Windows.Visibility.Hidden;
            textSuccess.Visibility = System.Windows.Visibility.Hidden;
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        public BaseUserControl(MainWindow prnt) : this()
        {
            this.parent = prnt;
            this.taskLevel = 1;
            if (Properties.Settings.Default.terapis == "--Keluarga--")
                noteButton.Visibility = System.Windows.Visibility.Hidden;
        }

        SqlConnector con = new SqlConnector();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            kinectSensorChooser.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser_KinectSensorChanged);
        }

        private void kinectSensorChooser_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor oldSensor = (KinectSensor)e.OldValue;
            KinectSensor newSensor = (KinectSensor)e.NewValue;

            StopKinect(oldSensor);

            newSensor.SkeletonStream.Enable();
            newSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(newSensor_AllFramesReady);

            newSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            newSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);


            Console.WriteLine("Sensor status: " + KinectSensor.KinectSensors[0].Status);
            try
            {
                newSensor.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                kinectSensorChooser.AppConflictOccurred();
            }

        }



        protected virtual void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton firstSkeleton = GetfirstSkeleton(e);
            if (firstSkeleton == null)
                return;
            ScalePosition(head, firstSkeleton.Joints[JointType.Head]);
            ScalePosition(leftHand, firstSkeleton.Joints[JointType.HandLeft]);
            ScalePosition(rightHand, firstSkeleton.Joints[JointType.HandRight]);
            ScalePosition(leftFoot, firstSkeleton.Joints[JointType.FootLeft]);
            ScalePosition(rightFoot, firstSkeleton.Joints[JointType.FootRight]);

            ScalePosition(head_, firstSkeleton.Joints[JointType.Head]);
            ScalePosition(leftHand_, firstSkeleton.Joints[JointType.HandLeft]);
            ScalePosition(rightHand_, firstSkeleton.Joints[JointType.HandRight]);
            ScalePosition(leftFoot_, firstSkeleton.Joints[JointType.FootLeft]);
            ScalePosition(rightFoot_, firstSkeleton.Joints[JointType.FootRight]);

            GetCameraPoint(firstSkeleton, e);

            //ProcessGesture();

        }

        protected virtual void ProcessGesture()
        {
            //to be overridden
        }

        private void ScalePosition(FrameworkElement element, Joint joint)
        {
            Joint scaledJoint = joint.ScaleTo(980, 720, .9f, .9f);
            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y);
        }

        private void GetCameraPoint(Skeleton firstSkeleton, AllFramesReadyEventArgs e)
        {
            using (DepthImageFrame depth = e.OpenDepthImageFrame())
            {
                if (depth == null || kinectSensorChooser.Kinect == null)
                    return;

                DepthImagePoint headDepth = depth.MapFromSkeletonPoint(firstSkeleton.Joints[JointType.Head].Position);
                DepthImagePoint leftHandDepth = depth.MapFromSkeletonPoint(firstSkeleton.Joints[JointType.HandLeft].Position);
                DepthImagePoint rightHandDepth = depth.MapFromSkeletonPoint(firstSkeleton.Joints[JointType.HandRight].Position);
                DepthImagePoint leftFootDepth = depth.MapFromSkeletonPoint(firstSkeleton.Joints[JointType.FootLeft].Position);
                DepthImagePoint rightFootDepth = depth.MapFromSkeletonPoint(firstSkeleton.Joints[JointType.FootRight].Position);

                try
                {
                    ColorImagePoint headColorPoint =
                        depth.MapToColorImagePoint(headDepth.X, headDepth.Y,
                        ColorImageFormat.RgbResolution640x480Fps30);
                    ColorImagePoint leftColorPoint =
                        depth.MapToColorImagePoint(leftHandDepth.X, leftHandDepth.Y,
                        ColorImageFormat.RgbResolution640x480Fps30);
                    ColorImagePoint rightColorPoint =
                        depth.MapToColorImagePoint(rightHandDepth.X, rightHandDepth.Y,
                        ColorImageFormat.RgbResolution640x480Fps30);
                    ColorImagePoint footLColorPoint =
                        depth.MapToColorImagePoint(leftFootDepth.X, leftFootDepth.Y,
                        ColorImageFormat.RgbResolution640x480Fps30);
                    ColorImagePoint footRColorPoint =
                        depth.MapToColorImagePoint(rightFootDepth.X, rightFootDepth.Y,
                        ColorImageFormat.RgbResolution640x480Fps30);

                    // circle position
                    CameraPosition(head, headColorPoint);
                    CameraPosition(leftHand, leftColorPoint);
                    CameraPosition(rightHand, rightColorPoint);
                    CameraPosition(leftFoot, footLColorPoint);
                    CameraPosition(rightFoot, footRColorPoint);

                    CameraPosition(head_, headColorPoint);
                    CameraPosition(leftHand_, leftColorPoint);
                    CameraPosition(rightHand_, rightColorPoint);
                    CameraPosition(leftFoot_, footLColorPoint);
                    CameraPosition(rightFoot_, footRColorPoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CameraPosition(FrameworkElement element, ColorImagePoint point)
        {
            // Divide by 2 for width and height so point is right in the middle 
            // instead of in top/left corner
            Canvas.SetLeft(element, point.X * 1.5 - element.Width / 2);
            Canvas.SetTop(element, point.Y * 1.5 - element.Height / 2);
        }

        protected Skeleton GetfirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                    return null;

                // memperoleh sekeleton
                skeletonFrameData.CopySkeletonDataTo(allSkeletons);
                Skeleton first = (from s in allSkeletons
                                  where s.TrackingState == SkeletonTrackingState.Tracked
                                  select s).FirstOrDefault();
                return first;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            if (countdown2 != null)
            {
                countdown2.Stop();

            }
            if (mainCanvas.Children.Contains(pict))
                mainCanvas.Children.Remove(pict);
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                sensor.Stop();

                sensor.DepthStream.Disable();
                sensor.SkeletonStream.Disable();
                sensor.ColorStream.Disable();
            }
        }

        protected virtual void Draw()
        {
            //to be overridden
        }

        protected virtual void countdown2_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {
                if (time == startTime)
                {
                    timer.Text = string.Format("0{0} : 0{1}", time / 60, time % 60);
                    time--;
                }
                else if (time < startTime)
                {
                    timer.Text = string.Format("0{0} : {1}", time / 60, time % 60);

                    if (gotIt == goalPoints)
                    {
                        countdown2.Stop();
                        rectSuccess.Visibility = System.Windows.Visibility.Visible;
                        textSuccess.Visibility = System.Windows.Visibility.Visible;
                        PlaySound(3);
                        instructionLabel.Visibility = System.Windows.Visibility.Hidden;
                        return;
                    }
                    else
                        time--;

                }
                else
                    time--;
            }
            else
            {
                if (gotIt < goalPoints)
                    PlaySound(2);

                countdown2.Stop();
                instructionLabel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        protected virtual void timer_Tick(object sender, EventArgs e)
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
                instructionLabel.Content = "Raih benda-benda yang ditampilkan";
            }
        }

        protected virtual void startButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            time = startTime;
            time2 = 5;
            gotIt = 0;
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

        private void finishButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((MessageBox.Show("Apakah yakin sudah selesai melakukan pelatihan?", "Perhatian", MessageBoxButton.YesNo)) == MessageBoxResult.Yes)
            {
                StopKinect(kinectSensorChooser.Kinect);
                (parent as MainWindow).getUserControl(new User_Control.Menu(parent));

                Properties.Settings.Default.note = null;
            }
            else
                return;
        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            (parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));

        }

        protected virtual void nextButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //StopKinect(kinectSensorChooser.Kinect);
            //(parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));
        }

        protected void PlaySound(int sound)
        {
            if (sound == 1)
            {
                soundEffect = new SoundPlayer(@"Sound\gotIt.wav");
            }
            else if (sound == 2)
            {
                soundEffect = new SoundPlayer(@"Sound\lose.wav");
            }
            else
                soundEffect = new SoundPlayer(@"Sound\Applause.wav");


            soundEffect.Play();
        }



        private void saveButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (start == true)
            {
                rectSuccess.Visibility = System.Windows.Visibility.Hidden;
                textSuccess.Visibility = System.Windows.Visibility.Hidden;

                if (Properties.Settings.Default.terapis != "--Keluarga--" && string.IsNullOrWhiteSpace(Properties.Settings.Default.note))
                {
                    if ((MessageBox.Show("Apakah yakin tidak ingin memberikan catatan?", "Perhatian", MessageBoxButton.YesNo)) == MessageBoxResult.Yes)
                    {
                        bool save = con.saveScore(1, taskLevel, int.Parse(score.Text));

                        if (save == true)
                            MessageBox.Show("Nilai berhasil disimpan");
                        else
                            MessageBox.Show("Error!!!");
                    }
                    else
                    {
                        note note = new note();
                        note.ShowDialog();
                    }
                }
                else
                {
                    bool save = con.saveScore(1, taskLevel, int.Parse(score.Text));

                    if (save == true)
                        MessageBox.Show("Nilai berhasil disimpan");
                    else
                        MessageBox.Show("Error!!!");
                }
            }
            else
                return;
        }

        private void homeButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            (parent as MainWindow).getUserControl(new User_Control.Menu(parent));
        }

        private void noteButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (start == true)
            {
                rectSuccess.Visibility = System.Windows.Visibility.Hidden;
                textSuccess.Visibility = System.Windows.Visibility.Hidden;

                note note = new note();

                note.ShowDialog();
            }
            else
                return;
        }

        private void helpButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rectSuccess.Visibility = System.Windows.Visibility.Hidden;
            textSuccess.Visibility = System.Windows.Visibility.Hidden;

            //HelpTask1 help = new HelpTask1();

            // help.ShowDialog();
        }

        #region label
        private void homeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            homeLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void homeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            homeLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void startButton_MouseEnter(object sender, MouseEventArgs e)
        {
            startLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void startButton_MouseLeave(object sender, MouseEventArgs e)
        {
            startLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void noteButton_MouseEnter(object sender, MouseEventArgs e)
        {
            noteLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void noteButton_MouseLeave(object sender, MouseEventArgs e)
        {
            noteLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void saveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            saveLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void saveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            saveLabel.Visibility = System.Windows.Visibility.Hidden;
        }



        private void helpButton_MouseEnter(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void helpButton_MouseLeave(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void backButton_MouseEnter(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void backButton_MouseLeave(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        private void nextButton_MouseEnter(object sender, MouseEventArgs e)
        {
            nextLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void nextButton_MouseLeave(object sender, MouseEventArgs e)
        {
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion
    }

}

