using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTracker
{
    /* Generic Events to use in-game. All of them are childs of
     * TrackerEvent, the base Event. Each of them have their own 
     * parameters and constructors to facilitate working with them.*/

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

    //[Serializable]
    //public sealed class GeneralEvent : TrackerEvent
    //{
    //    public GeneralEvent()
    //    {
    //        _type = eventType.NOTYPE.ToString();
    //    }

    //    public GeneralEvent(int level, string room, double xPos, double yPos, double zPos)
    //        : base(level, xPos, yPos, zPos)
    //    {
    //        _type = eventType.NOTYPE.ToString();
    //    }

    //    //public override string ToCSV()
    //    //{
    //    //    string result = base.ToCSV();

    //    //    result += ",";
    //    //    result += string.Join(",", _room);

    //    //    return result;
    //    //}

    //}

    [Serializable]
    public sealed class BugEvent : TrackerEvent
    {
        public string _room { get; private set; }
        public string _errorMsg { get; set; }
        public string _keyWord { get; set; }
        public BugEvent(int level, string room, double xPos, double yPos, double zPos, string error, string keyWord)
            : base(level, xPos, yPos, zPos)
        {
            _room = room;
            _errorMsg = error;
            _keyWord = keyWord;
            _type = eventType.BUG.ToString();
        }

        public override string ToCSV()
        {
            string result = base.ToCSV();

            result += ",\"";
            result += string.Join(",", _room, _errorMsg, _keyWord);
            result += "\"";

            return result;
        }

    }

    [Serializable]
    public sealed class MilestoneEvent : TrackerEvent
    {

        //Other methods here
        //..
        public string _achievement { get; private set; }
        public MilestoneEvent(int level)
            : this(level, string.Empty, -1, -1, -1) { }

        public MilestoneEvent(int level, string room, double xPos, double yPos, double zPos)
            : base(level, xPos, yPos, zPos)
        {
            _type = eventType.PROGRESS.ToString();
        }

        public override string ToCSV()
        {
            string result = base.ToCSV();

            result += ",\"";
            result += string.Join(",", _achievement);
            result += "\"";
            return result;
        }

    }

    [Serializable]
    public sealed class ProgressEvent : TrackerEvent
    {
        public string _room { get; private set; }

        public ProgressEvent(int level)
            : this(level, string.Empty, -1, -1, -1) { }

        public ProgressEvent(int level, string room, double xPos, double yPos, double zPos)
            : base(level, xPos, yPos, zPos)
        {
            _room = room;
            _type = eventType.PROGRESS.ToString();
        }

        public override string ToCSV()
        {
            string result = base.ToCSV();

            result += ",\"";
            result += string.Join(",", _room);
            result += "\"";

            return result;
        }

    }
}
