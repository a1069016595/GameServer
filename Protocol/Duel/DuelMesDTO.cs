using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    [Serializable]
    public class DuelMesDTO
    {
        public Deck deck;
        public string account;
        public bool isReady;
    }

    [Serializable]
    public class RoomAccountDTO
    {
        public string account1;
        public string account2;
    }
}
