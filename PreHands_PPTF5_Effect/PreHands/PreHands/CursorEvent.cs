
namespace PreHands
{
    using Coding4Fun.Kinect.Wpf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
    using Microsoft.Kinect.Toolkit.Interaction;

    class CursorEvent
    {
        [DllImport("user32.dll")] // 마우스 포인트 제어(위치, 클릭 등의 이벤트)
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private static int coordinateX = 0;
        private static int coordinateY = 0;
        private static bool clickBool = false;

        private static UserInfo[] userInfos = null;

        public static void cursorMove(Skeleton skeleton)
        {
            Joint rightJoint = skeleton.Joints[JointType.HandRight].ScaleTo(1600, 900, .3f, .3f);
            SkeletonPoint right = rightJoint.Position;
            if (right.X != 0 || right.Y != 0)
            {
                //System.Diagnostics.Debug.WriteLine("x: " + right.X + "   y: " + right.Y);
                SetCursorPos((int)right.X, (int)right.Y);
                coordinateX = (int)right.X;
                coordinateY = (int)right.Y;
            }
        }

        public static void cursorAction(InteractionFrameReadyEventArgs e)
        {
            using (InteractionFrame frame = e.OpenInteractionFrame())
            {
                if (frame != null)
                {
                    if (userInfos == null)
                    {
                        userInfos = new UserInfo[InteractionFrame.UserInfoArrayLength];
                    }

                    frame.CopyInteractionDataTo(userInfos);
                }
                else
                {
                    return;
                }
            }

            foreach (UserInfo userInfo in userInfos)
            {
                foreach (InteractionHandPointer handPointer in userInfo.HandPointers)
                {
                    if (handPointer.HandType == InteractionHandType.Left)
                    {
                        switch (handPointer.HandEventType)
                        {
                            case InteractionHandEventType.Grip:
                                System.Diagnostics.Debug.WriteLine("그립 실행");
                                mouse_event((int)MouseEventFlags.LEFTDOWN, coordinateX, coordinateY, 0, 0);
                                clickBool = true;
                                break;
                            case InteractionHandEventType.GripRelease:
                                if (clickBool)
                                {
                                    mouse_event((int)MouseEventFlags.LEFTUP, coordinateX, coordinateY, 0, 0);
                                    clickBool = false;
                                }

                                break;
                        }
                    } 
                }
            }
        }
    }
}