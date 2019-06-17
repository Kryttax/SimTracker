using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTracker
{
    public struct PlayerPosition
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public PlayerPosition(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    [Serializable]
    public sealed class GeneralEvent : TrackerEvent
    {
        //Test Event
        public GeneralEvent()
        {
            _type = eventType.NOTYPE.ToString();
        }

        public GeneralEvent(int level, string room, double xPos, double yPos, double zPos)
            : base(level, xPos, yPos, zPos)
        {
            _type = eventType.NOTYPE.ToString();
        }

    }

    [Serializable]
    public sealed class BugEvent : TrackerEvent
    {
        public string _room { get; set; }
        public string _errorMsg;

        public BugEvent(int level, string room, double xPos, double yPos, double zPos, string error)
            : base(level, xPos, yPos, zPos)
        {
            _room = room;
            _errorMsg = error;
            _type = eventType.BUG.ToString();
        }

    }

    [Serializable]
    public sealed class MilestoneEvent : TrackerEvent
    {

        //Other methods here
        //..

        public MilestoneEvent(int level)
            : this(level, string.Empty, -1, -1, -1) { }

        public MilestoneEvent(int level, string room, double xPos, double yPos, double zPos)
            : base(level, xPos, yPos, zPos)
        {
            _type = eventType.PROGRESS.ToString();
        }

    }

    [Serializable]
    public sealed class ProgressEvent : TrackerEvent
    {
        public string _room { get; set; }

        public ProgressEvent(int level)
            : this(level, string.Empty, -1, -1, -1) { }

        public ProgressEvent(int level, string room, double xPos, double yPos, double zPos)
            : base(level, xPos, yPos, zPos)
        {
            _room = room;
            _type = eventType.PROGRESS.ToString();
        }

    }
}
