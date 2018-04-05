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
    /// Interaction logic for FirstStandingBalance.xaml
    /// </summary>
    public partial class FirstStandingBalance : UserControl
    {

        MainWindow parent;
        int taskLevel;

        Skeleton[] allSkeletons = new Skeleton[6];
        private int time;
        private int time2;
        private int spawnTime;
        private DispatcherTimer countdown;
        private DispatcherTimer countdown2;
        Rectangle pict;
        private int gotIt;
        private int tempScore;
        private int intersect;
        SoundPlayer soundEffect;
        private bool start = false;
        private double headInitX, headInitY;
        private double lHandInitX, lHandInitY;
        private double rHandInitX, rHandInitY;
        bool alreadyStand = false;
        bool severalTries = false;
        bool usesHands = false;

        public FirstStandingBalance()
        {
            InitializeComponent();
            score.Text = "0";
            spawnTime = -1;
            tempScore = 0;
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


        public FirstStandingBalance(MainWindow prnt, int level) : this()
        {
            this.parent = prnt;
            this.taskLevel = level;
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

        private void setInitialPosition()
        {
            headInitX = Canvas.GetLeft(head);
            headInitY = Canvas.GetTop(head);
            lHandInitX = Canvas.GetLeft(leftHand);
            lHandInitY = Canvas.GetTop(leftHand);
            rHandInitX = Canvas.GetLeft(rightHand);
            rHandInitY = Canvas.GetTop(rightHand);
        }

        private void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton firstSkeleton = GetfirstSkeleton(e);
            if (firstSkeleton == null)
                return;

            if (time2 == 0)
            {
                setInitialPosition();
            }

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
            if (mainCanvas.Children.Contains(pict))
            {
                int fullScore = 4;
   
                var xR = Canvas.GetLeft(rightHand);
                var yR = Canvas.GetTop(rightHand);
                var xL = Canvas.GetLeft(leftHand);
                var yL = Canvas.GetTop(leftHand);
                var yH = Canvas.GetBottom(head);
                var yHInv = Canvas.GetTop(head);
                var xH = Canvas.GetLeft(head);

                Rect rH = new Rect(xH, yHInv, head.ActualWidth, head.ActualHeight);
                Rect rR = new Rect(xR, yR, rightHand.ActualWidth, rightHand.ActualHeight);
                Rect rL = new Rect(xL, yL, leftHand.ActualWidth, leftHand.ActualHeight);
                Rect r2 = new Rect(Canvas.GetLeft(pict), Canvas.GetTop(pict), pict.ActualWidth, pict.Height);

                if(yHInv <= Properties.Settings.Default.yCoordHead + (Math.Abs(Properties.Settings.Default.yCoordHead - headInitY)/2) +40 && alreadyStand == false)
                {
                    // PlaySound(1);
                    Console.WriteLine("mulai berdiri");
                    alreadyStand = true;
                }
                if (yHInv >= (headInitY-10) && alreadyStand == true && severalTries == false)
                {
                    // PlaySound(2);
                    Console.WriteLine("Kembali duduk");
                    severalTries = true;
                }
                if (yHInv >= (headInitY-10) && (Math.Abs(yL - lHandInitY) >= 20 || Math.Abs(yR - rHandInitY) >= 20))
                {
                   // PlaySound(2);
                    usesHands = true;
                }

                if (rH.IntersectsWith(r2))
                {
                    Console.WriteLine("r intercests with r2");
                    mainCanvas.Children.Remove(pict);
                    PlaySound(1);
                    gotIt++;
                    intersect = time;
                    if (usesHands == false)
                    {
                        fullScore = 4;
                    }
                    else if (usesHands == true && severalTries == false)
                    {
                        fullScore = 3;
                    }
                    else if (usesHands == true && severalTries == true)
                    {
                        fullScore = 2;
                    }
                    Score(fullScore);
                    score.Text = tempScore.ToString();
                }

            }
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

        private void Draw()
        {
            /*Rectangle pictTest = new Rectangle();
            pictTest.Width = 100;
            pictTest.Height = 100;
            pictTest.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/" + Properties.Settings.Default.fruit)));
            Canvas.SetTop(pictTest, Properties.Settings.Default.yCoordHead + (Math.Abs(Properties.Settings.Default.yCoordHead - headInitY) / 2) + 40);
            Canvas.SetLeft(pictTest, headInitX);
            mainCanvas.Children.Add(pictTest);*/

            pict = new Rectangle();
            pict.Width = 100;
            pict.Height = 100;
            pict.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/" + Properties.Settings.Default.animal)));
            float height = Properties.Settings.Default.yCoordHead +20;
            spawnTime = time;
            Canvas.SetTop(pict, height);
            Canvas.SetLeft(pict, headInitX);
            mainCanvas.Children.Add(pict);
        }

        private void countdown2_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {
                if (time == 165)
                {
                    timer.Text = string.Format("0{0} : 0{1}", time / 60, time % 60);
                    time--;
                }
                else if (time < 165)
                {
                    timer.Text = string.Format("0{0} : {1}", time / 60, time % 60);

                    if (gotIt == 1)
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
                if (gotIt < 1)
                    PlaySound(2);

                countdown2.Stop();
                instructionLabel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
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

        private void startButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            time = 165;
            time2 = 5;
            gotIt = 0;
            score.Text = "0";
            tempScore = 0;
            alreadyStand = false;
            severalTries = false;
            usesHands = false;
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

        private void nextButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //StopKinect(kinectSensorChooser.Kinect);
            //(parent as MainWindow).getUserControl(new User_Control.levelTherapy(parent));
        }

        private void PlaySound(int sound)
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

        private void Score(int fullScore)
        {
            tempScore = 25 * fullScore;
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

