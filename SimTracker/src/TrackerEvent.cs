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

        [Newtonsoft.Json.JsonProperty(Order = -7)]
        public string _userId { get; set; }
        [Newtonsoft.Json.JsonProperty(Order = -6)]
        public string _dateStamp { get; set; }
        [Newtonsoft.Json.JsonProperty(Order = -5)]
        public string _timeStamp { get; set; }
        [Newtonsoft.Json.JsonProperty(Order = -4)]
        public string _type { get; set; }
        [Newtonsoft.Json.JsonProperty(Order = -3)]
        public int _level { get; set; }
        [Newtonsoft.Json.JsonProperty(Order = -2)]
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

        public virtual string ToCSV()
        {

            //Serializar aquí
            //cadena texto
            //ser = SimTracker.serializaionObjct;
            //ser.SetType(new CSVSerializer());

            string result = string.Join(",", _userId, _dateStamp, _timeStamp, _type, _level, _playerPos);
            return result;
        }

        public virtual string ToJson()
        {

            //ser = SimTracker.serializaionObjct;
            //ser.SetType(new JSONSerializer());
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
    }
    
}
