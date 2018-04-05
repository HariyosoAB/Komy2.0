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
    /// Interaction logic for Task4.xaml
    /// </summary>
    public partial class Task4 : UserControl
    {
        MainWindow parent;
        SqlConnector con = new SqlConnector();

        Skeleton[] allSkeletons = new Skeleton[6];
        private int time;
        private int time2;
        private DispatcherTimer timer;
        private DispatcherTimer timer2;
        Skeleton firstSkeleton = new Skeleton();
        Rectangle pict1 = new Rectangle();
        Rectangle pict2 = new Rectangle();
        private int taskLevel;
        private bool finish = false;
        private bool start = false;
        private bool start_ = false;
        SoundPlayer soundEffect;

        public Task4()
        {
            InitializeComponent();
            warningLabel.Visibility = System.Windows.Visibility.Hidden;
            homeLabel.Visibility = System.Windows.Visibility.Hidden;
            startLabel.Visibility = System.Windows.Visibility.Hidden;
            noteLabel.Visibility = System.Windows.Visibility.Hidden;
            saveLabel.Visibility = System.Windows.Visibility.Hidden;
            finishLabel.Visibility = System.Windows.Visibility.Hidden;
            helpLabel.Visibility = System.Windows.Visibility.Hidden;
            rectSuccess.Visibility = System.Windows.Visibility.Hidden;
            textSuccess.Visibility = System.Windows.Visibility.Hidden;
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        public Task4(MainWindow prnt, int level):this()
        {
            this.parent = prnt;
            this.taskLevel = level;
            if (Properties.Settings.Default.terapis == "--Keluarga--")
                noteButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void startButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            time = 5;
            time2 = -5;
            finish = false;
            start = true;
            start_ = true;
            scoreLabel.Text = "0";
            Properties.Settings.Default.note = null;
            rectSuccess.Visibility = textSuccess.Visibility = System.Windows.Visibility.Hidden;
            noteButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/notes.png"));
            saveButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/save.png"));

            if (timer != null)
                timer.Stop();

            if (timer2 != null)
                timer2.Stop();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();

            timer2 = new DispatcherTimer();
            timer2.Interval = new TimeSpan(0, 0, 1);
            timer2.Tick += timer2_Tick;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (time2 < 0)
                time2++;
            else if(time2 > 300)
            {
                timer2.Stop();
                Scores(time2, taskLevel);
            }
            else
            {
                if (finish == false)
                {
                    if (time2 % 60 < 10)
                    {
                        timerLabel.Text = string.Format("0{0} : 0{1}", time2 / 60, time2 % 60);
                    }
                    else
                        timerLabel.Text = string.Format("0{0} : {1}", time2 / 60, time2 % 60);

                    time2++;
                }
                else
                    timer2.Stop();
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            countdownLabel.Visibility = System.Windows.Visibility.Visible;

            if (time >= 0)
            {
                if (time <= 5)
                {
                    countdownLabel.Text = string.Format("{0}", time % 60);
                    time--;
                }
            }
            else
            {
                timer.Stop();
                countdownLabel.Visibility = System.Windows.Visibility.Collapsed;

                if (mainCanvas.Children != null)
                {
                    mainCanvas.Children.Remove(pict1);
                    mainCanvas.Children.Remove(pict2);
                }

                Draw();
            }
        }

        private void Draw()
        {
            pict1.Width = pict2.Width = 50;
            pict1.Height = pict2.Height = 100;
            pict1.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/rightStep.png")));
            pict2.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/leftStep.png")));
            Canvas.SetLeft(pict1, 500);
            Canvas.SetTop(pict1, 560);
            Canvas.SetLeft(pict2, 410);
            Canvas.SetTop(pict2, 560);
            mainCanvas.Children.Add(pict1);
            mainCanvas.Children.Add(pict2);
        }

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

        private void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            firstSkeleton = GetfirstSkeleton(e);

            if (firstSkeleton == null)
                return;

            ObjectPosition();
            
            GetCameraPoint(firstSkeleton, e);

        }

        private void ObjectPosition()
        {
            if (start == true && time2 <= 300)
            {
                if (finish == false)
                    zLabel.Text = Math.Round((Decimal)firstSkeleton.Joints[JointType.Spine].Position.Z, 2).ToString();
                else
                    zLabel.Text = "1.00";

                double Position = double.Parse(zLabel.Text.ToString());

                if (taskLevel == 1)
                {
                    // distance 1 m
                    if (Position > 2.0)
                    {
                        warningLabel.Visibility = System.Windows.Visibility.Visible;
                        warningLabel.Text = "Jarak maksimal 2m";
                        return;
                    }
                    else
                    {
                        warningLabel.Visibility = System.Windows.Visibility.Collapsed;

                        if (Position >= 1.9 && Position <= 2)
                        {
                            Canvas.SetTop(pict2, 560);
                        }
                        else if (Position >= 1.8 && Position < 1.9)
                        {
                            Canvas.SetTop(pict1, 480);
                        }
                        else if (Position >= 1.6 && Position < 1.7)
                        {
                            Canvas.SetTop(pict2, 400);
                        }
                        else if (Position >= 1.5 && Position < 1.6)
                        {
                            Canvas.SetTop(pict1, 320);
                        }
                        else if (Position >= 1.4 && Position < 1.5)
                        {
                            Canvas.SetTop(pict2, 240);
                        }
                        else if (Position >= 1.3 && Position < 1.4)
                        {
                            Canvas.SetTop(pict1, 160);
                        }
                        else if (Position >= 1.1 && Position < 1.2)
                        {
                            Canvas.SetTop(pict2, 80);
                        }
                        else if (Position > 1.0 && Position < 1.1)
                        {
                            Canvas.SetTop(pict1, 0);
                            Canvas.SetTop(pict2, 0);
                        }
                        else if (Position <= 1.0)
                        {
                            finish = true;
                            start = false;
                            zLabel.Text = "1.00";
                            Scores(time2, 1);
                            rectSuccess.Visibility = System.Windows.Visibility.Visible;
                            textSuccess.Visibility = System.Windows.Visibility.Visible;
                            PlaySound();
                        }
                    }
                }
                else if (taskLevel == 2)
                {
                    // distance 2 m
                    if (Position > 3.0)
                    {
                        warningLabel.Visibility = System.Windows.Visibility.Visible;
                        warningLabel.Text = "Jarak maksimal 3m";
                        return;
                    }
                    else
                    {
                        warningLabel.Visibility = System.Windows.Visibility.Collapsed;

                        if (Position >= 2.8 && Position <= 3)
                        {
                            Canvas.SetTop(pict2, 560);
                        }
                        else if (Position >= 2.6 && Position < 2.8)
                        {
                            Canvas.SetTop(pict1, 497.8);
                        }
                        else if (Position >= 2.4 && Position < 2.6)
                        {
                            Canvas.SetTop(pict2, 435.6);
                        }
                        else if (Position >= 2.2 && Position < 2.4)
                        {
                            Canvas.SetTop(pict1, 373.4);
                        }
                        else if (Position >= 2.0 && Position < 2.2)
                        {
                            Canvas.SetTop(pict2, 311.2);
                        }
                        else if (Position >= 1.8 && Position < 2.0)
                        {
                            Canvas.SetTop(pict1, 249);
                        }
                        else if (Position >= 1.6 && Position < 1.8)
                        {
                            Canvas.SetTop(pict2, 186.8);
                        }
                        else if (Position >= 1.4 && Position < 1.6)
                        {
                            Canvas.SetTop(pict1, 124.6);
                        }
                        else if (Position >= 1.2 && Position < 1.4)
                        {
                            Canvas.SetTop(pict2, 62.4);
                        }
                        else if (Position > 1.0 && Position < 1.2)
                        {
                            Canvas.SetTop(pict1, 0);
                            Canvas.SetTop(pict2, 0);
                        }
                        else if (Position <= 1.0)
                        {
                            finish = true;
                            start = false;
                            zLabel.Text = "1.00";
                            Scores(time2, 2);
                            rectSuccess.Visibility = System.Windows.Visibility.Visible;
                            textSuccess.Visibility = System.Windows.Visibility.Visible;
                            PlaySound();
                        }
                    }
                }
                else if (taskLevel == 3)
                {
                    if (Position > 4.0)
                    {
                        warningLabel.Visibility = System.Windows.Visibility.Visible;
                        warningLabel.Text = "Jarak maksimal 4m";
                        return;
                    }
                    else
                    {
                        warningLabel.Visibility = System.Windows.Visibility.Collapsed;

                        if (Position >= 3.75 && Position <= 4)
                        {
                            Canvas.SetTop(pict2, 560);
                        }
                        else if (Position >= 3.50 && Position < 3.75)
                        {
                            Canvas.SetTop(pict1, 509);
                        }
                        else if (Position >= 3.25 && Position < 3.50)
                        {
                            Canvas.SetTop(pict2, 458);
                        }
                        else if (Position >= 3.00 && Position < 3.25)
                        {
                            Canvas.SetTop(pict1, 407);
                        }
                        else if (Position >= 2.75 && Position < 3.00)
                        {
                            Canvas.SetTop(pict2, 356);
                        }
                        else if (Position >= 2.50 && Position < 2.75)
                        {
                            Canvas.SetTop(pict1, 305);
                        }
                        else if (Position >= 2.25 && Position < 2.50)
                        {
                            Canvas.SetTop(pict2, 254);
                        }
                        else if (Position >= 2.00 && Position < 2.25)
                        {
                            Canvas.SetTop(pict1, 203);
                        }
                        else if (Position >= 1.75 && Position < 2.00)
                        {
                            Canvas.SetTop(pict2, 152);
                        }
                        else if (Position >= 1.50 && Position < 1.75)
                        {
                            Canvas.SetTop(pict1, 101);
                        }
                        else if (Position >= 1.25 && Position < 1.50)
                        {
                            Canvas.SetTop(pict2, 50);
                        }
                        else if (Position > 1.00 && Position < 1.25)
                        {
                            Canvas.SetTop(pict1, 0);
                            Canvas.SetTop(pict2, 0);
                        }
                        else if (Position <= 1.0)
                        {
                            finish = true;
                            start = false;
                            zLabel.Text = "1.00";
                            Scores(time2, 3);
                            rectSuccess.Visibility = System.Windows.Visibility.Visible;
                            textSuccess.Visibility = System.Windows.Visibility.Visible;
                            PlaySound();
                        }
                    }
                }

            }
            else
                return;
        }

        //unfinishe
        private void Scores(int time, int stage)
        {
            if(stage == 1)
            {
                if (103 - time <= 10)
                    scoreLabel.Text = "10";
                else
                    scoreLabel.Text = (103 - time).ToString();
            }
            else if (stage == 2)
            {
                if (104 - time <= 10)
                    scoreLabel.Text = "10";
                else
                    scoreLabel.Text = (104 - time).ToString();
            }
            else if(stage == 3)
            {
                if (105 - time <= 10)
                    scoreLabel.Text = "10";
                else
                    scoreLabel.Text = (105 - time).ToString();
            }
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

                    //CameraPosition(head, headColorPoint);
                    //CameraPosition(leftHand, leftColorPoint);
                    //CameraPosition(rightHand, rightColorPoint);
                    //CameraPosition(leftFoot, footRColorPoint);
                    //CameraPosition(rightFoot, footLColorPoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private Skeleton GetfirstSkeleton(AllFramesReadyEventArgs e)
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

        private void StopKinect(KinectSensor sensor)
        {
            if(sensor != null)
            {
                sensor.Stop();
                sensor.SkeletonStream.Disable();
                sensor.DepthStream.Disable();
                sensor.ColorStream.Disable();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            timer2.Stop();
            if(mainCanvas.Children.Contains(pict1) && mainCanvas.Children.Contains(pict2))
            {
                mainCanvas.Children.Remove(pict1);
                mainCanvas.Children.Remove(pict2);
            }
        }

        private void helpButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rectSuccess.Visibility = System.Windows.Visibility.Hidden;
            textSuccess.Visibility = System.Windows.Visibility.Hidden;

            HelpTask4 help = new HelpTask4();

            help.ShowDialog();
        }

        private void noteButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (start_ == true)
            {
                rectSuccess.Visibility = System.Windows.Visibility.Hidden;
                textSuccess.Visibility = System.Windows.Visibility.Hidden;

                note note = new note();

                note.ShowDialog();
            }
            else
                return;
        }

        private void saveButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (start_ == true)
            {
                rectSuccess.Visibility = System.Windows.Visibility.Hidden;
                textSuccess.Visibility = System.Windows.Visibility.Hidden;

                if (Properties.Settings.Default.terapis != "--Keluarga--" && string.IsNullOrWhiteSpace(Properties.Settings.Default.note))
                {
                    if ((MessageBox.Show("Apakah yakin tidak ingin memberikan catatan?", "Perhatian", MessageBoxButton.YesNo)) == MessageBoxResult.Yes)
                    {
                        bool save = con.saveScore(4, taskLevel, int.Parse(scoreLabel.Text));

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
                    bool save = con.saveScore(3, taskLevel, int.Parse(scoreLabel.Text));

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

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            (parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));
        }

        #region Label
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

        private void finishButton_MouseEnter(object sender, MouseEventArgs e)
        {
            finishLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void finishButton_MouseLeave(object sender, MouseEventArgs e)
        {
            finishLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void helpButton_MouseEnter(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void helpButton_MouseLeave(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

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

        private void PlaySound()
        {
            soundEffect = new SoundPlayer(@"Sound\Applause.wav");

            soundEffect.Play();
        }

        private void backButton_MouseEnter(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void backButton_MouseLeave(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
