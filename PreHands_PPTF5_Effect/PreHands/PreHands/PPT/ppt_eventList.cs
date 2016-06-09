using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using PPt = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;


/* @Auther Somin Lee(makersm, sayyo1120@gmail.com)
*  This is ppt eventlisteners list.
*  You have to write conditions in each of functions.
*  PPT eventlists' conditions are using skeleton event.
*/

namespace PreHands.PPT
{
    public class ppt_eventLists
    {
        private static SkeletonPoint lastHandPoint;
        private static SkeletonPoint curHandPoint;
        private static int rightCount = 0;
        private static int leftCount = 0;
        static bool startFlag = false;
        static bool endFlag = false;
        private static Timer timer;
        private static int controlChecker = 0;
        
        // Define PowerPoint Application object
        private static PPt.Application pptApplication;
        // Define Presentation object
        private static PPt.Presentation presentation;
        // Define Slide collection
        private static PPt.Slides slides;
        private static PPt.Slide slide;

        // Slide count
        private static int slidescount;
        // slide index
        private static int slideIndex;

        public static void PPTSearch()
        {
            try
            {
                // Get Running PowerPoint Application object
                pptApplication = Marshal.GetActiveObject("PowerPoint.Application") as PPt.Application;

                // Get PowerPoint application successfully, then set control button enable
            }
            catch
            {
                MessageBox.Show("Please Run PowerPoint Firstly", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            if (pptApplication != null)
            {
                // Get Presentation Object
                presentation = pptApplication.ActivePresentation;
                // Get Slide collection object
                slides = presentation.Slides;
                // Get Slide count
                slidescount = slides.Count;
                // Get current selected slide 
                try
                {
                    // Get selected slide object in normal view
                    slide = slides[pptApplication.ActiveWindow.Selection.SlideRange.SlideNumber];
                }
                catch
                {
                    // Get selected slide object in reading view
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
        }

        public static void nextPage()
        {
            slideIndex = slide.SlideIndex + 1;
            if (slideIndex > slidescount)
            {
                MessageBox.Show("It is already last page");
            }
            else
            {
                try
                {
                    slide = slides[slideIndex];
                    slides[slideIndex].Select();
                }
                catch
                {
                    pptApplication.SlideShowWindows[1].View.Next();
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
        }

        public static void previousPage()
        {
            slideIndex = slide.SlideIndex - 1;
            if (slideIndex >= 1)
            {
                try
                {
                    slide = slides[slideIndex];
                    slides[slideIndex].Select();
                }
                catch
                {
                    pptApplication.SlideShowWindows[1].View.Previous();
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
            else
            {
                MessageBox.Show("It is already Fist Page");
            }
        }
    

        public static void startPPTControl(object sender, EventArgs e)
        {
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;

            SkeletonPoint r_hand = eventarg.skeleton.Joints[JointType.HandRight].Position;
            SkeletonPoint head = eventarg.skeleton.Joints[JointType.Head].Position;
            SkeletonPoint r_shoulder = eventarg.skeleton.Joints[JointType.ShoulderRight].Position;

            if (controlChecker == 90)
            {
                //MainWindow.isPPTActive = MainWindow.isPPTActive == true ? false : true;
                System.Diagnostics.Debug.WriteLine("active");
            }

            if (head.Y >= r_hand.Y && r_shoulder.X + 0.1 >= r_hand.X)
            {
                controlChecker++;
            }
            else
            {
                controlChecker = 0;
            }
        }


        private static bool isSlideGestureReady(SkeletonPoint hand, SkeletonPoint shoulder)
        {
            return (Math.Abs(shoulder.Y - hand.Y) < 0.15 && !(hand.X == shoulder.X)) ? true : false;
        }

        private static bool isGestureReady(object sender, EventArgs e)
        {
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;

            SkeletonPoint r_shoulder = eventarg.skeleton.Joints[JointType.ShoulderRight].Position;
            SkeletonPoint l_shoulder = eventarg.skeleton.Joints[JointType.ShoulderLeft].Position;

            //System.Diagnostics.Debug.WriteLine("r_shoulder z : " + r_shoulder.Z);
            //System.Diagnostics.Debug.WriteLine("l_shoulder z : " + l_shoulder.Z);
            //System.Diagnostics.Debug.WriteLine("spine z : " + spine.Z);

            return (Math.Abs(r_shoulder.Z - l_shoulder.Z) >= 0.085) ? false : true;
        }

        public static void startPPT(object sender, EventArgs e)
        {
            if (!isGestureReady(sender, e)) return;
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;
            SkeletonPoint head = eventarg.skeleton.Joints[JointType.Head].Position;
            SkeletonPoint compare = eventarg.skeleton.Joints[JointType.HandRight].Position;
            float shoulder = eventarg.skeleton.Joints[JointType.ShoulderRight].Position.Y;

            if (head.Y <= compare.Y && head.Z < compare.Z)
            {
                if (!startFlag)
                {
                    //                    SendKeys.SendWait("{F5}");
                    PPTSearch();
                    presentation.SlideShowSettings.Run();
                    System.Diagnostics.Debug.WriteLine("f5");
                    startFlag = true;
                }
            }
            else
            {
                startFlag = false;
            }
        }

        private static void SlideTimer(Object sender, EventArgs eventarg)
        {
            timer.Enabled = false;
        }

        public static void SlideChecker(object sender, EventArgs e)
        {
            if (!isGestureReady(sender, e)) return;
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;

            SkeletonPoint hand = eventarg.skeleton.Joints[JointType.HandRight].Position;
            SkeletonPoint shoulder = eventarg.skeleton.Joints[JointType.ShoulderRight].Position;
            SkeletonPoint spine = eventarg.skeleton.Joints[JointType.Spine].Position;



            if (isSlideGestureReady(hand, shoulder))
            {
                if (lastHandPoint == null) lastHandPoint = hand;
                curHandPoint = hand;

                if (curHandPoint.X - lastHandPoint.X > 0.01)
                {
                    if (curHandPoint.X < shoulder.X) return;

                    rightCount++;
                    leftCount = 0;
                }
                else if (curHandPoint.X - lastHandPoint.X < -0.01)
                {
                    leftCount++;
                    rightCount = 0;
                }

                if (rightCount > 6 && spine.X < curHandPoint.X)
                {
                    goNextSlide();
                    rightCount = 0;
                }

                if (leftCount > 4 && spine.X > curHandPoint.X)
                {
                    goPreviousSlide();
                    leftCount = 0;
                }

                lastHandPoint = curHandPoint;
            }
        }

        private static void goPreviousSlide()
        {
            SendKeys.SendWait("{left}");
            System.Diagnostics.Debug.WriteLine("left");
        }

        private static void goNextSlide()
        {
            SendKeys.SendWait("{right}");
            System.Diagnostics.Debug.WriteLine("right");
        }

        public static void endPPT(object sender, EventArgs e)
        {
            if (!isGestureReady(sender, e)) return;
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;

            SkeletonPoint hand = eventarg.skeleton.Joints[JointType.HandRight].Position;
            SkeletonPoint hip = eventarg.skeleton.Joints[JointType.HipRight].Position;
            if (hand.Y < hip.Y && hand.Z > hip.Z)
            {
                if (!endFlag)
                {
                    SendKeys.SendWait("{esc}");
                    System.Diagnostics.Debug.WriteLine("esc");
                    endFlag = true;
                    //MainWindow.isPPTActive = false;
                }

            }
            else
            {
                endFlag = false;
            }
        }
    }
}
