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

namespace BismillahTA.User_Control
{
    /// <summary>
    /// Interaction logic for coba.xaml
    /// </summary>
    public partial class coba : UserControl
    {
        Skeleton[] allSkeletons = new Skeleton[6];
        public coba()
        {
            InitializeComponent();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopKinect(kinectSensorChooser.Kinect);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("in");
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
            //ScalePosition(head, firstSkeleton.Joints[JointType.Head]);
            //ScalePosition(leftHand, firstSkeleton.Joints[JointType.HandLeft]);
            //ScalePosition(rightHand, firstSkeleton.Joints[JointType.HandRight]);

            //ScalePosition(leftFoot, firstSkeleton.Joints[JointType.FootLeft]);
            //ScalePosition(rightFoot, firstSkeleton.Joints[JointType.FootRight]);

            GetCameraPoint(firstSkeleton, e);

            //ProcessGesture(firstSkeleton.Joints[JointType.Head], firstSkeleton.Joints[JointType.HandRight]);
        }

        private void ScalePosition(FrameworkElement element, Joint joint)
        {
            //convert the value to X/Y
            //Joint scaledJoint = joint.ScaleTo(325, 330, .3f, .3f); 

            //convert & scale (.3 = means 1/3 of joint distance)
            Joint scaledJoint = joint.ScaleTo(1280, 720, .3f, .3f);

            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y);

            //if (scaledJoint.JointType == JointType.FootRight)
            //{
            //    label1.Content = "Kaki Kanan";
            //    label2.Content = scaledJoint.Position.X.ToString();
            //    label3.Content = scaledJoint.Position.Y.ToString();
            //    label4.Content = joint.Position.Z.ToString();
            //}
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

                //try
                //{
                //    ColorImagePoint headColorPoint =
                //        depth.MapToColorImagePoint(headDepth.X, headDepth.Y,
                //        ColorImageFormat.RgbResolution640x480Fps30);
                //    ColorImagePoint leftColorPoint =
                //        depth.MapToColorImagePoint(leftHandDepth.X, leftHandDepth.Y,
                //        ColorImageFormat.RgbResolution640x480Fps30);
                //    ColorImagePoint rightColorPoint =
                //        depth.MapToColorImagePoint(rightHandDepth.X, rightHandDepth.Y,
                //        ColorImageFormat.RgbResolution640x480Fps30);
                //    ColorImagePoint footLColorPoint =
                //        depth.MapToColorImagePoint(leftFootDepth.X, leftFootDepth.Y,
                //        ColorImageFormat.RgbResolution640x480Fps30);
                //    ColorImagePoint footRColorPoint =
                //        depth.MapToColorImagePoint(rightFootDepth.X, rightFootDepth.Y,
                //        ColorImageFormat.RgbResolution640x480Fps30);

                ////    CameraPosition(head, headColorPoint);
                ////    CameraPosition(leftHand, leftColorPoint);
                ////    CameraPosition(rightHand, rightColorPoint);
                ////    CameraPosition(leftFoot, footRColorPoint);
                ////    CameraPosition(rightFoot, footLColorPoint);
                ////}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}

            }

        }

        private void CameraPosition(FrameworkElement element, ColorImagePoint point)
        {
            //Divide by 2 for width and height so point is right in the middle 
            // instead of in top/left corner
            Canvas.SetLeft(element, point.X - element.Width / 2);
            Canvas.SetTop(element, point.Y - element.Height / 2);

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
                sensor.AudioSource.Stop();

                sensor.DepthStream.Disable();
                sensor.SkeletonStream.Disable();
                sensor.ColorStream.Disable();
            }
        }
    }
}
