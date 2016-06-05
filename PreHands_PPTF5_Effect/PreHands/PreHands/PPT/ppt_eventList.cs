using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;

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

            return (Math.Abs(r_shoulder.Z - l_shoulder.Z) >= 0.1) ? false : true;
        }

        public static void startPPT(object sender, EventArgs e)
        {
            if (!isGestureReady(sender, e)) return;
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;
            SkeletonPoint head = eventarg.skeleton.Joints[JointType.Head].Position;
            SkeletonPoint compare = eventarg.skeleton.Joints[JointType.HandRight].Position;
            float shoulder = eventarg.skeleton.Joints[JointType.ShoulderRight].Position.Y;

            if (head.Y < compare.Y && head.Z < compare.Z)
            {
                if (!startFlag)
                {
                    //SendKeys.SendWait("{F5}");
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

                if (leftCount > 6 && spine.X > curHandPoint.X)
                {
                    goPreviousSlide();
                    leftCount = 0;
                }

                lastHandPoint = curHandPoint;
            }
        }

        private static void goPreviousSlide()
        {
            //SendKeys.SendWait("{left}");
            System.Diagnostics.Debug.WriteLine("left");
        }

        private static void goNextSlide()
        {
            //SendKeys.SendWait("{right}");
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
                    //SendKeys.SendWait("{esc}");
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
