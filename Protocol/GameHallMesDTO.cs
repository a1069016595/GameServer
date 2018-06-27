using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    [Serializable]
    public class GameHallMesDTO
    {
        public int onlineNum;
        public int playingNum;
        public int standbyNum;
    }
}
