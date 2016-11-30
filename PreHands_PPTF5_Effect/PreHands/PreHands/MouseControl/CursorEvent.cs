/* @Auther Jaeung Choi(darker826, darker826@gmail.com)
* 마우스 커서, 클릭 등을 처리하는 클래스 입니다.
* 객체를 생성 후 함수를 부르기만 하면 사용이 가능함.
* 마우스의 이동에는 Skeleton을 사용하고, 클릭이벤트 등에는 MainWindow에서 등록한 InteractionFrame을 받아와 Hand State를 읽습니다.
*/

namespace PreHands.MouseControl
{
    using Coding4Fun.Kinect.Wpf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using System.Runtime.InteropServices;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
    using Microsoft.Kinect.Toolkit.Interaction;


    public class CursorEvent
    {
        [DllImport("user32.dll")] // 마우스 포인트 제어(위치, 클릭 등의 이벤트)
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void keybd_event(int vk, int scan, int flags, int extraInfo);

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
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800
        }
       
        private static int rightCoordinateX = 0;
        private static int rightCoordinateY = 0;
        private static double rightCoordinateZ = 0;
        private static bool clickBool = false;
        private static bool doubleClickBool = false;
        private static Timer doubleClickTimer;
        private static int leftCoordinateX = 0;
        private static int leftCoordinateY = 0;
        private static int beforeMoveX = 0;
        private static int beforeMoveY = 0;
        private static bool leftGripBool = false;
        private static bool rightGripBool = false;
        private static int mouseWheel = 0;

        private static UserInfo[] userInfos = null;

        public static void cursorMove(Skeleton skeleton)
        {
            //화면의 해상도를 불러온다.
            int deskWidth = Screen.PrimaryScreen.Bounds.Width;
            int deskHeight = Screen.PrimaryScreen.Bounds.Height;

            Joint rightJoint = skeleton.Joints[JointType.HandRight].ScaleTo(deskWidth, deskHeight, .3f, .3f);
            Joint leftJoint = skeleton.Joints[JointType.HandLeft].ScaleTo(deskWidth, deskHeight, .3f, .3f);

            SkeletonPoint left = leftJoint.Position;
            SkeletonPoint right = rightJoint.Position;
            if (right.X != 0 || right.Y != 0)
            {
                SetCursorPos((int)right.X, (int)right.Y);
                rightCoordinateX = (int)right.X;
                rightCoordinateY = (int)right.Y;
                rightCoordinateZ = right.Z;
            }
            if (left.X != 0 || left.Y != 0)
            {
                SetCursorPos((int)right.X, (int)right.Y);
                leftCoordinateX = (int)right.X;
                leftCoordinateY = (int)right.Y;
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

            //타이머 셋팅
            if (doubleClickTimer == null)
            {
                doubleClickTimer = new Timer();
                doubleClickTimer.Enabled = true;
                doubleClickTimer.Interval = 2000;
                doubleClickTimer.Tick += new EventHandler(timerEvent);
            }

            foreach (UserInfo userInfo in userInfos)
            {
                foreach (InteractionHandPointer handPointer in userInfo.HandPointers)
                {
                    //왼손처리
                    if (handPointer.HandType == InteractionHandType.Left)
                    {
                        if(handPointer.HandEventType == InteractionHandEventType.Grip)
                        {
                            leftGripBool = true;
                            keybd_event((byte)Keys.ControlKey, 0x00, 0x00, 0);
                            System.Diagnostics.Debug.WriteLine("그립!!===========================");
                
                        }
                        else if(handPointer.HandEventType == InteractionHandEventType.GripRelease)
                        {
                            leftGripBool = false;
                            keybd_event((byte)Keys.ControlKey, 0x00, 0x02, 0);
                            mouseWheel = 0;
                            System.Diagnostics.Debug.WriteLine("그립릴리즈으!!===========================");

                        }
                    }
                    //오른쪽 처리
                    if (handPointer.HandType == InteractionHandType.Right)
                    {
                        
                        if (rightGripBool)
                        {
                            if (leftGripBool)
                            {
                                if (beforeMoveX != 0 && beforeMoveY != 0)
                                {
                                    System.Diagnostics.Debug.WriteLine((rightCoordinateX + rightCoordinateY) + "랑 " + (beforeMoveX + beforeMoveY));

                                    if (rightCoordinateX + rightCoordinateY > beforeMoveX + beforeMoveY)
                                    {
                                        mouse_event((int)MouseEventFlags.WHEEL, 30, 30, 0, 0);
                                        mouse_event((int)MouseEventFlags.WHEEL, 30, 30, 10, 0);
                                        System.Diagnostics.Debug.WriteLine("확대애애애애애애애애애애애애애애애");

                                    }
                                    else if (rightCoordinateX + rightCoordinateY < beforeMoveX + beforeMoveY)
                                    {
                                        mouse_event((int)MouseEventFlags.WHEEL, 30, 30, 0, 0);
                                        mouse_event((int)MouseEventFlags.WHEEL, 30, 30, (-10), 0);
                                        System.Diagnostics.Debug.WriteLine("축소오오오오오오오오오오오오");

                                    }
                                }
                                beforeMoveX = rightCoordinateX;
                                beforeMoveY = rightCoordinateY;
                            }
                        } 
                        switch (handPointer.HandEventType)
                        {
                            //모니터 왼쪽 위 끝이 0, 0
                            case InteractionHandEventType.Grip:
                                rightGripBool = true;
                                if (!leftGripBool)
                                {
                                    if (doubleClickBool) //더블클릭
                                    {
                                        int tmpX, tmpY;
                                        tmpX = rightCoordinateX;
                                        tmpY = rightCoordinateY;
                                        mouse_event((int)MouseEventFlags.LEFTDOWN, tmpX, tmpY, 0, 0);
                                        mouse_event((int)MouseEventFlags.LEFTUP, tmpX, tmpY, 0, 0);
                                        mouse_event((int)MouseEventFlags.LEFTDOWN, tmpX, tmpY, 0, 0);
                                        mouse_event((int)MouseEventFlags.LEFTUP, tmpX, tmpY, 0, 0);
                                    }
                                    mouse_event((int)MouseEventFlags.LEFTDOWN, rightCoordinateX, rightCoordinateY, 0, 0);
                                    clickBool = true;
                                    doubleClickBool = true;
                                    doubleClickTimer.Start();
                                }
                                break;

                            case InteractionHandEventType.GripRelease:
                                rightGripBool = false;
                                if (clickBool)
                                {
                                    mouse_event((int)MouseEventFlags.LEFTUP, rightCoordinateX, rightCoordinateY, 0, 0);
                                    clickBool = false;
                                }
                                break;
                        }
                    }
                }
            }
        }

        //타이머 작동시 불리는 함수
        public static void timerEvent(object sender, EventArgs e)
        {
            doubleClickBool = false;
            doubleClickTimer.Stop();
        }
    }
}