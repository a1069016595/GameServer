using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol
{

    [Serializable]
    public class RoomInfoDTO
    {
       public string roomState;
        public int roomID;
        public string roomOwner;
        public string roomName;
    }
}
