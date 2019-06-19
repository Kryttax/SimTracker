using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
namespace SimTracker
{
    [Serializable]
    public class TrackerEvent : IEvent
    {
        public enum eventType { NOTYPE, BUG, PROGRESS, LEVEL_AREA, COMPLETABLE }

        public string _userId { get; set; }
        public string _dateStamp { get; set; }
        public string _timeStamp { get; set; }
        public string _type { get; set; }
        public int _level { get; set; }
        public string _playerPos { get; set; }

        private Serializer ser;

        public TrackerEvent()
        {
            _userId = SimTracker.Instance.user;
            _dateStamp = DateTime.Now.ToShortDateString().ToString();
            _timeStamp = DateTime.Now.ToLongTimeString();
            _type = eventType.NOTYPE.ToString();
            _level = -1;
            _playerPos = "-1,-1,-1";
        }

        public TrackerEvent(int level) : this()
        {
            _level = level;
        }

        public TrackerEvent(int level, double xPos, double yPos, double zPos) : this()
        {
            _level = level;
            PlayerPosition pos = new PlayerPosition(xPos, yPos, zPos);
            _playerPos = string.Join(",", pos.X, pos.Y, pos.Z);
        }

        public string ToCSV()
        {
            ser = SimTracker.serializaionObjct;
            ser.SetType(new CSVSerializer());
            return ser.Serialize(this);
        }

        public string ToJson()
        {
            ser = SimTracker.serializaionObjct;
            ser.SetType(new JSONSerializer());
            return ser.Serialize(this);
        }
    }
    
}
