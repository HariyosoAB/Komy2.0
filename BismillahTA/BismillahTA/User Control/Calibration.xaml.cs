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

namespace BismillahTA.User_Control
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class Calibration : UserControl
    {
        MainWindow parent;
        Skeleton[] allSkeletons = new Skeleton[6];
        private int time2;
        private DispatcherTimer countdown;
        int start = 0;
        double rightArmLength;
        double leftArmLength;
        double rightArmLengthMeter;
        double leftArmLengthMeter;


        public Calibration()
        {
            InitializeComponent();
        }

        public Calibration(MainWindow parent):this()
        {
            this.parent = parent;
            startLabel.Visibility = System.Windows.Visibility.Hidden;
            captureLabel.Visibility = System.Windows.Visibility.Hidden;
            restartLabel.Visibility = System.Windows.Visibility.Hidden;
            helpLabel.Visibility = System.Windows.Visibility.Hidden;
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
            backLabel.Visibility = System.Windows.Visibility.Hidden;

            xCoordinate.Text = "1";
            yCoordinate.Text = "0";
            yCoordinateFoot.Text = "0";
        }

        private void backButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.Menu(parent));
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
            calculateArmsLength(e);
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

        private void CameraPosition(Ellipse element, ColorImagePoint point)
        {
            // Divide by 2 for width and height so point is right in the middle 
            // instead of in top/left corner
            Canvas.SetLeft(element, point.X * 1.5 - element.Width / 2);
            Canvas.SetTop(element, point.Y * 1.5 - element.Height / 2);
        }

        private void ScalePosition(Ellipse element, Joint joint)
        {
            Joint scaledJoint = joint.ScaleTo(980, 720, .9f, .9f);

            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y);

            if (start == 1)
            {
                if (scaledJoint.JointType == JointType.Head)
                {
                    xCoordinate.Text = Math.Round((Decimal)scaledJoint.Position.X, 2).ToString();
                    yCoordinate.Text = Math.Round((Decimal)scaledJoint.Position.Y, 2).ToString();
                }

                if(scaledJoint.JointType == JointType.FootRight)
                {
                    yCoordinateFoot.Text = Math.Round((Decimal)scaledJoint.Position.Y, 2).ToString();
                }
            }
            else if (start == 2)
            {
                if (scaledJoint.JointType == JointType.FootRight)
                {
                    xCoordinate.Text = Math.Round((Decimal)scaledJoint.Position.X, 2).ToString();
                    yCoordinate.Text = Math.Round((Decimal)scaledJoint.Position.Y, 2).ToString();
                }
            }
            else if (start == 3)
            {
                if (scaledJoint.JointType == JointType.FootLeft)
                {
                    xCoordinate.Text = Math.Round((Decimal)scaledJoint.Position.X, 2).ToString();
                    yCoordinate.Text = Math.Round((Decimal)scaledJoint.Position.Y, 2).ToString();
                }
            }
            else
                return;
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
            if (sensor != null)
            {
                sensor.Stop();

                sensor.DepthStream.Disable();
                sensor.SkeletonStream.Disable();
                sensor.ColorStream.Disable();
            }
        }

        private void startButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (start < 3)
            {
                time2 = 5;
                countdown = new DispatcherTimer();
                countdown.Interval = new TimeSpan(0, 0, 1);
                countdown.Tick += new EventHandler(timer_Tick);
                countdown.Start();


                if (start >= 0)
                {
                    captureButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/capture.png"));
                }

                if (start == 0)
                    instruction.Text = "Mengambil posisi kepala dan kaki, dengan posisi berdiri tegak";
                else if (start == 1)
                {
                    instruction.Text = "Mengambil posisi kaki kanan, gerakan kaki kanan ke samping";
                    yFootBackground.Visibility = System.Windows.Visibility.Hidden;
                    yCoordinateFoot.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (start == 2)
                {
                    instruction.Text = "Mengambil posisi kaki kiri, gerakan kaki kiri ke samping";
                    yFootBackground.Visibility = System.Windows.Visibility.Hidden;
                    yCoordinateFoot.Visibility = System.Windows.Visibility.Hidden;
                }

                start++;
                if(start == 3)
                    startButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/startUnactive.png"));
                
            }
            else
            {
                captureButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/captureUnactive.png"));
                return;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (time2 >= 0)
            {

                timer2.Visibility = System.Windows.Visibility.Visible;
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
                instructionLabel.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void captureButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (start == 1)
            {
                Properties.Settings.Default.xCoordHead = float.Parse(xCoordinate.Text);
                Properties.Settings.Default.yCoordHead = float.Parse(yCoordinate.Text);
                Properties.Settings.Default.yCoordFoot = float.Parse(yCoordinateFoot.Text);
                Properties.Settings.Default.rightArmLength = this.rightArmLength;
                Properties.Settings.Default.leftArmLength = this.leftArmLength;
                Properties.Settings.Default.rightArmLengthMeter = this.rightArmLengthMeter;
                Properties.Settings.Default.leftArmLengthMeter = this.leftArmLengthMeter;

                Properties.Settings.Default.Save();
                MessageBox.Show("Koordinat KEPALA dan KAKI dalam posisi tegak berhasil disimpan");
            }
            else if (start == 2)
            {
                Properties.Settings.Default.xCoordRightFoot = float.Parse(xCoordinate.Text);
                Properties.Settings.Default.yCoordRightFoot = float.Parse(yCoordinate.Text);

                Properties.Settings.Default.Save();
                MessageBox.Show("Koordinat KAKI KANAN berhasil disimpan");
            }
            else if (start == 3)
            {
                Properties.Settings.Default.xCoordLeftFoot = float.Parse(xCoordinate.Text);
                Properties.Settings.Default.yCoordLeftFoot = float.Parse(yCoordinate.Text);

                Properties.Settings.Default.Save();
                MessageBox.Show("Koordinat KAKI KIRI berhasil disimpan");

            }
            else
                return;
        }

        private void nextButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.xCoordHead == 0 && Properties.Settings.Default.yCoordHead == 0 && Properties.Settings.Default.xCoordRightFoot == 0
                && Properties.Settings.Default.yCoordRightFoot == 0 && Properties.Settings.Default.xCoordLeftFoot == 0 && Properties.Settings.Default.yCoordLeftFoot == 0)
            {
                MessageBox.Show("Lakukan KALIBRASI sebelum menuju langkah selanjutnya");
            }
            else
            {
                StopKinect(kinectSensorChooser.Kinect);
                (parent as MainWindow).getUserControl(new User_Control.chooseObject(parent));
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
        }

        private void restartButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings.Default.xCoordHead = 0;
            Properties.Settings.Default.yCoordHead = 0;
            Properties.Settings.Default.xCoordLeftFoot = 0;
            Properties.Settings.Default.yCoordLeftFoot = 0;
            Properties.Settings.Default.xCoordRightFoot = 0;
            Properties.Settings.Default.yCoordRightFoot = 0;
            Properties.Settings.Default.rightArmLength = 0;
            Properties.Settings.Default.leftArmLength = 0;
            Properties.Settings.Default.rightArmLengthMeter = 0;
            Properties.Settings.Default.leftArmLengthMeter = 0;
            start = 0;
            instruction.Text = "";
            captureButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/captureUnactive.png"));
            startButton.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/start.png"));

            yFootBackground.Visibility = System.Windows.Visibility.Visible;
            yCoordinateFoot.Visibility = System.Windows.Visibility.Visible;
        }
        public static double length(Joint p1, Joint p2)
        {
            return Math.Sqrt(
                Math.Pow(p1.Position.X - p2.Position.X, 2) +
                Math.Pow(p1.Position.Y - p2.Position.Y, 2) +
                Math.Pow(p1.Position.Z - p2.Position.Z, 2));
        }

        private void calculateArmsLength(AllFramesReadyEventArgs e)
        {
            Skeleton firstSkeleton = GetfirstSkeleton(e);
            if (firstSkeleton == null) return;
            Joint rightShoulder = firstSkeleton.Joints[JointType.ShoulderRight];
            Joint rightElbow = firstSkeleton.Joints[JointType.ElbowRight];
            Joint rightWrist = firstSkeleton.Joints[JointType.WristRight];
            Joint rightHand = firstSkeleton.Joints[JointType.HandRight];
            Joint leftShoulder = firstSkeleton.Joints[JointType.ShoulderLeft];
            Joint leftElbow = firstSkeleton.Joints[JointType.ElbowLeft];
            Joint leftWrist = firstSkeleton.Joints[JointType.WristLeft];
            Joint leftHand = firstSkeleton.Joints[JointType.HandLeft];

            this.rightArmLengthMeter = length(rightShoulder, rightElbow) + length(rightElbow, rightWrist) + length(rightWrist, rightHand);
            this.leftArmLengthMeter = length(leftShoulder, leftElbow) + length(leftElbow, leftWrist) + length(leftWrist, leftHand);


            rightShoulder = rightShoulder.ScaleTo(980, 720, .9f, .9f);
            rightElbow = rightElbow.ScaleTo(980, 720, .9f, .9f);
            rightWrist = rightWrist.ScaleTo(980, 720, .9f, .9f);
            rightHand = rightHand.ScaleTo(980, 720, .9f, .9f);


            leftShoulder = leftShoulder.ScaleTo(980, 720, .9f, .9f);
            leftElbow = leftElbow.ScaleTo(980, 720, .9f, .9f);
            leftWrist = leftWrist.ScaleTo(980, 720, .9f, .9f);
            leftHand = leftHand.ScaleTo(980, 720, .9f, .9f);

            this.rightArmLength = length(rightShoulder, rightElbow) + length(rightElbow, rightWrist) + length(rightWrist, rightHand);
            this.leftArmLength = length(rightShoulder, rightElbow) + length(rightElbow, rightWrist) + length(rightWrist, rightHand);

        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
            (parent as MainWindow).getUserControl(new User_Control.Menu(parent));
        }

        private void helpButton_MouseEnter(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void helpButton_MouseLeave(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void restartButton_MouseEnter(object sender, MouseEventArgs e)
        {
            restartLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void restartButton_MouseLeave(object sender, MouseEventArgs e)
        {
            restartLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void captureButton_MouseEnter(object sender, MouseEventArgs e)
        {
            captureLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void captureButton_MouseLeave(object sender, MouseEventArgs e)
        {
            captureLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void startButton_MouseEnter(object sender, MouseEventArgs e)
        {
            startLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void startButton_MouseLeave(object sender, MouseEventArgs e)
        {
            startLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void helpButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HelpKalibrasi help = new HelpKalibrasi();

            help.ShowDialog();
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

 
    }
}
