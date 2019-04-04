using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SimTracker
{
    class SimTracker
    {
        public static SimTracker instance = null;

        public List<ISerializer> serializaionObjct = new List<ISerializer>();

        public List<IPersistence> persistenceObject = new List<IPersistence>();

        Queue<TrackerEvent> assetTrackerObject = new Queue<TrackerEvent>();
        bool alive, flag;
        Thread QueueCleaner;

        private SimTracker()
        {
            serializaionObjct.Add(new CSVSerializer());
            persistenceObject.Add(new FilePersistence());
            alive = true;
            flag = false;
            QueueCleaner = new Thread(runnable);
            QueueCleaner.Start();
        }

        private void runnable()
        {
            DateTime dt = DateTime.Now;
            DateTime dtnow;
            TimeSpan result;
            int seconds = 0;
            while (alive)
            {
                dtnow = DateTime.Now;

                result = dtnow.Subtract(dt);
                seconds = Convert.ToInt32(result.TotalSeconds);

                if (seconds > 15)
                {
                    flag = !flag;
                    seconds = 0;
                    dt = DateTime.Now;
                }

                while (flag && assetTrackerObject.Any())
                {
                    System.Console.WriteLine("Traza creada");
                    TrackerEvent obj = assetTrackerObject.Dequeue();

                    persistenceObject[0].Send(obj.ToCSV());


                }

                flag = false;

            }

            while (assetTrackerObject.Any())
            {
                System.Console.WriteLine("Traza creada");
                TrackerEvent obj = assetTrackerObject.Dequeue();

                persistenceObject[0].Send(obj.ToCSV());
            }
        }

        public void StopCleaning()
        {
            alive = false;
        }
        public static SimTracker Instance()
        {
            if (instance == null)
                instance = new SimTracker();
            return instance;
        }

        public void PushEvent(TrackerEvent evnt)
        {
            assetTrackerObject.Enqueue(evnt);
        }

        public void WriteInFile()
        {
            persistenceObject[0].Send(assetTrackerObject.Dequeue().ToCSV());
            //persistenceObject[0].Send(serializaionObjct[0].Serialize(assetTrackerObject.Dequeue()));
        }
    }
}
