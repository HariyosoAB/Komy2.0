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
    /// Interaction logic for Task3.xaml
    /// </summary>
    public partial class Task3 : UserControl
    {
        MainWindow parent;
        private int taskLevel;

        Skeleton[] allSkeletons = new Skeleton[6];
        private int time;
        private int time2;
        private DispatcherTimer countdown;
        private DispatcherTimer countdown2;
        private Rectangle pict1, pict2, pict3;
        private int gotIt = 0;
        private int intersect;
        private int scores;
        private bool start = false;

        SqlConnector con = new SqlConnector();

        public Task3()
        {
            InitializeComponent();
            score.Text = "0";
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

        public Task3(MainWindow prnt, int level) :this()
        {
            this.parent = prnt;
            this.taskLevel = level;
            if (Properties.Settings.Default.terapis == "--Keluarga--")
                noteButton.Visibility = System.Windows.Visibility.Hidden;
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

        private void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
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

            ProcessGesture();
        }

        private void ProcessGesture()
        {
            if (mainCanvas.Children.Contains(pict1))
            {
                Console.WriteLine("mainCanvas contains pict");

                var xR = Canvas.GetLeft(rightFoot);
                var yR = Canvas.GetTop(rightFoot);
                var xL = Canvas.GetLeft(leftFoot);
                var yL = Canvas.GetTop(leftFoot);
                Rect rR = new Rect(xR, yR, rightFoot.ActualWidth, rightFoot.ActualHeight);
                Rect rL = new Rect(xL, yL, leftFoot.ActualWidth, leftFoot.ActualHeight);

                Rect r1 = new Rect(Canvas.GetLeft(pict1), Canvas.GetTop(pict1), pict1.ActualWidth, pict1.Height);
                Rect r2 = new Rect(Canvas.GetLeft(pict2), Canvas.GetTop(pict2), pict2.ActualWidth, pict2.Height);
                Rect r3 = new Rect(Canvas.GetLeft(pict3), Canvas.GetTop(pict3), pict3.ActualWidth, pict3.Height);

                if (rL.IntersectsWith(r1) || rR.IntersectsWith(r1))
                {
                    mainCanvas.Children.Remove(pict1);
                    mainCanvas.Children.Remove(pict2);
                    mainCanvas.Children.Remove(pict3);

                    gotIt++;
                    scores += 5;
                    score.Text = scores.ToString();
                    intersect = time;

                    PlaySound(1);
                }
                else if (rL.IntersectsWith(r2) || rR.IntersectsWith(r2))
                {
                    mainCanvas.Children.Remove(pict1);
                    mainCanvas.Children.Remove(pict2);
                    mainCanvas.Children.Remove(pict3);

                    gotIt++;

                    if (scores != 0)
                        scores -= 5;
                    else
                        scores = 0;

                    score.Text = scores.ToString();
                    intersect = time;

                    PlaySound(2);
                }
                else if (rL.IntersectsWith(r3) || rR.IntersectsWith(r3))
                {
                    mainCanvas.Children.Remove(pict1);
                    mainCanvas.Children.Remove(pict2);
                    mainCanvas.Children.Remove(pict3);

                    gotIt++;

                    if (scores != 0)
                        scores -= 5;
                    else
                        scores = 0;

                    score.Text = scores.ToString();
                    intersect = time;

                    PlaySound(2);
                }
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

        private void ScalePosition(FrameworkElement element, Joint joint)
        {
            Joint scaledJoint = joint.ScaleTo(980, 720, .9f, .9f);

            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y);
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

                sensor.DepthStream.Disable();
                sensor.SkeletonStream.Disable();
                sensor.ColorStream.Disable();
            }
        }

        private void startButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            time = 126;
            time2 = 5;
            gotIt = 0;
            score.Text = "0";
            start = true;
            Properties.Settings.Default.note = null;
            rectSuccess.Visibility = textSuccess.Visibility = System.Windows.Visibility.Hidden;
            noteButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/notes.png"));
            saveButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/save.png"));

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

        private void countdown2_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {
                if (time == 120)
                {
                    timer.Text = string.Format("0{0} : 0{1}", time / 60, time % 60);
                    time--;
                }
                else if (time < 120)
                {
                    timer.Text = string.Format("0{0} : {1}", time / 60, time % 60);

                    if(gotIt != 0)
                    {
                        if(taskLevel == 1)
                        {
                            if (time == intersect - 10)
                                Draw();
                        }
                        else if(taskLevel == 2)
                        {
                            if(time == intersect - 7)
                                Draw();
                        }
                        else if(taskLevel == 3)
                        {
                            if (time == intersect - 3)
                                Draw();
                        }
                    }

                    time--;
                }
                else
                    time--;
            }
            else
            {
                countdown2.Stop();
                instructionLabel.Visibility = System.Windows.Visibility.Hidden;

                if (gotIt == 0)
                    PlaySound(2);
                else
                {
                    rectSuccess.Visibility = System.Windows.Visibility.Visible;
                    textSuccess.Visibility = System.Windows.Visibility.Visible;
                    PlaySound(3);
                }
            } 
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            countdownLabel.Visibility = System.Windows.Visibility.Visible;

            if (time2 >= 0)
            {
                if (time2 <= 5)
                {
                    countdownLabel.Text = string.Format("{0}", time2 % 60);
                    time2--;
                }
            }
            else
            {
                countdown.Stop();
                countdownLabel.Visibility = System.Windows.Visibility.Collapsed;
                Draw();
                instructionLabel.Content = "Angkat kaki menyentuh gambar bintang";
            }
        }

        private void Draw()
        {
            pict1 = new Rectangle();
            pict2 = new Rectangle();
            pict3 = new Rectangle();
            pict1.Width = pict2.Width = pict3.Width = 80;
            pict1.Height = pict2.Height = pict3.Height = 80;

            pict1.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/star.png")));
            pict2.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/circle.png")));
            pict3.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/circle.png")));

            //Canvas.SetLeft(pict1, 50);
            //Canvas.SetTop(pict1, 600);

            //Canvas.SetLeft(pict2, 185);
            //Canvas.SetTop(pict2, 600);

            //Canvas.SetLeft(pict3, 320);
            //Canvas.SetTop(pict3, 600);

            Random rand = new Random();
            int position = rand.Next(1, 3);

            if (position == 1)
            {
                Canvas.SetLeft(pict1, Properties.Settings.Default.xCoordHead - 100 - 35);
                Canvas.SetTop(pict1, Properties.Settings.Default.yCoordLeftFoot - 100);

                Canvas.SetLeft(pict2, Properties.Settings.Default.xCoordHead - 35);
                Canvas.SetTop(pict2, Properties.Settings.Default.yCoordLeftFoot - 100);

                Canvas.SetLeft(pict3, Properties.Settings.Default.xCoordHead + 100 - 35);
                Canvas.SetTop(pict3, Properties.Settings.Default.yCoordLeftFoot - 100);
            }
            else if (position == 2)
            {
                Canvas.SetLeft(pict2, Properties.Settings.Default.xCoordHead - 100 - 35);
                Canvas.SetTop(pict2, Properties.Settings.Default.yCoordLeftFoot - 100);

                Canvas.SetLeft(pict1, Properties.Settings.Default.xCoordHead - 35);
                Canvas.SetTop(pict1, Properties.Settings.Default.yCoordLeftFoot - 100);

                Canvas.SetLeft(pict3, Properties.Settings.Default.xCoordHead + 100 - 35);
                Canvas.SetTop(pict3, Properties.Settings.Default.yCoordLeftFoot - 100);
            }
            else if (position == 3)
            {
                Canvas.SetLeft(pict3, Properties.Settings.Default.xCoordHead - 100 - 35);
                Canvas.SetTop(pict3, Properties.Settings.Default.yCoordLeftFoot - 100);

                Canvas.SetLeft(pict2, Properties.Settings.Default.xCoordHead - 35);
                Canvas.SetTop(pict2, Properties.Settings.Default.yCoordLeftFoot - 100);

                Canvas.SetLeft(pict1, Properties.Settings.Default.xCoordHead + 100 - 35);
                Canvas.SetTop(pict1, Properties.Settings.Default.yCoordLeftFoot - 100);
            }

            mainCanvas.Children.Add(pict1); 
            mainCanvas.Children.Add(pict2); 
            mainCanvas.Children.Add(pict3);
            
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
                        bool save = con.saveScore(3, taskLevel, int.Parse(score.Text));

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
                    bool save = con.saveScore(4, taskLevel, int.Parse(score.Text));

                    if (save == true)
                        MessageBox.Show("Nilai berhasil disimpan");
                    else
                        MessageBox.Show("Error!!!");
                }
            }
            else
                return;
        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            (parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));
        }

        private void homeButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            (parent as MainWindow).getUserControl(new User_Control.Menu(parent));
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
            finishButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void finishButton_MouseLeave(object sender, MouseEventArgs e)
        {
            finishButton.Visibility = System.Windows.Visibility.Hidden;
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

        private void helpButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rectSuccess.Visibility = System.Windows.Visibility.Hidden;
            textSuccess.Visibility = System.Windows.Visibility.Hidden;

            HelpTask3 help = new HelpTask3();

            help.ShowDialog();
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

        private void PlaySound(int sound)
        {
            SoundPlayer soundEffect;

            if (sound == 1)
            {
                soundEffect = new SoundPlayer(@"Sound\win.wav");
            }
            else if (sound == 2)
            {
                soundEffect = new SoundPlayer(@"Sound\lose.wav");
            }
            else
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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            countdown2.Stop();
            if (mainCanvas.Children.Contains(pict1) && mainCanvas.Children.Contains(pict2) && mainCanvas.Children.Contains(pict3))
            {
                mainCanvas.Children.Remove(pict1);
                mainCanvas.Children.Remove(pict2);
                mainCanvas.Children.Remove(pict3);
            }
        }
    }
}
