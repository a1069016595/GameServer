using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Protocol
{
    [Serializable]
    public class RoomListDTO
    {
        public RoomInfoDTO[] roomList;
        public int roomCount;
    }
}
