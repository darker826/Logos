using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Kinect;


/* @Auther Somin Lee(makersm, sayyo1120@gmail.com)
*  This is Skeleton event handler that can be handle about skeleton event.
*  First, you can registEventListener using delegate type.
*  When you regist event listener, you can start event listeners through start function. 
*/

namespace PreHands.eventHandler
{
    using System.Collections.ObjectModel;
    public class Skeleton_eventHandler
    {
        public delegate void customEvent(object sender, EventArgs args);
        public event customEvent registEventListener;
        public void start(Skeleton_eventArgs e)
        {
            if (registEventListener != null)
            {
                registEventListener(this, e);
            }
        }
    }

    public class Skeleton_eventArgs : EventArgs
    {
        public Skeleton_eventArgs(Skeleton s)
        {
            skeleton = s;
        }
        public Skeleton skeleton { get; set; }
    }
}
