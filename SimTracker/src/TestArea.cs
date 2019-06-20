using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;

namespace SimTracker
{
    class TestArea
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments:
            System.Console.WriteLine("Testing. . .\n");

            SimTracker tracker = new SimTracker();
            
            Thread.Sleep(1000);
            SimTracker.Instance.PushEvent(new TrackerEvent());
            SimTracker.Instance.PushEvent(new BugEvent(0, "bugRoom", 4, 2, -8, "This is a testing error", "My_ERROR"));

            Thread.Sleep(1000);
            SimTracker.Instance.PushEvent(new TrackerEvent(1, 2, 4, 2));
            SimTracker.Instance.PushEvent(new ProgressEvent(0, "first room", 20, 50, 70.5));

            SimTracker.Instance.StopCleaning();
        }
    }
}

