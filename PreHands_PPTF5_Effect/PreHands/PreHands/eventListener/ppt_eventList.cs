using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PreHands.eventHandler;
using System.Windows.Forms;
using Microsoft.Kinect; 

/* @Auther Somin Lee(makersm, sayyo1120@gmail.com)
*  This is ppt eventlisteners list.
*  You have to write conditions in each of functions.
*  PPT eventlists' conditions are using skeleton event.
*  MAYBE cursor events are using hand state event (or not...).
*/

namespace PreHands.eventListener
{
    public class ppt_eventLists
    {
        public static void startPPT(object sender, EventArgs e)
        {
            Skeleton_eventArgs eventarg = e as Skeleton_eventArgs;
            SkeletonPoint basement = eventarg.skeleton.Joints[JointType.Spine].Position;
            SkeletonPoint compare = eventarg.skeleton.Joints[JointType.HandRight].Position;
            float u_height = eventarg.skeleton.Joints[JointType.ShoulderRight].Position.Y;
            float d_height = eventarg.skeleton.Joints[JointType.HipRight].Position.Y;
            if (basement.X > compare.X && u_height > compare.Y && compare.Y > d_height)
            {
                SendKeys.SendWait("{F5}");
            }
        }
    }
}
