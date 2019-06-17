using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTracker
{
    //Context Class
    class Persistence
    {
        private IPersistence _type;

        public void SetType(IPersistence type)
        {
            this._type = type;
        }

        public void Send<T>(T str)
        {
            _type.Send(str);
        }
    }
}
