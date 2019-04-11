using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTracker
{
    [Serializable]
    class TrackerEvent
    {
        public enum eventType { BUG, PROGRESS, LEVEL_AREA, COMPLETABLE }

        public eventType type;
        public int level { get; set; }  //0 == TUTORIAL, 1 == FIRST LEVEL
        public string timeStamp { get; set; }
        public string content { get; set; }


        public TrackerEvent()
        {
            level = 0;
            timeStamp = DateTime.Now.ToString();
        }

        public string ToCSV()
        {
            return SimTracker.instance.serializaionObjct[0].Serialize(this);
        }

        public string ToJson()
        {
            return SimTracker.instance.serializaionObjct[1].Serialize(this);
        }
    }
}
