using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol
{
    [Serializable]
    public class GameHallProtocol
    {
        public const int GAMEHALL_ROOMLIST_CREQ = 0;
        public const int GAMEHALL_ROOMLIST_BRQ = 1;

        public const int GAMEHALL_CHAT_CREQ = 2;
        public const int GAMEHALL_CHAT_BRQ = 3;

        public const int GAMEHALL_CREATEROOM_CREQ = 4;
        public const int GAMEHALL_CREATEROOM_BRQ = 5;

        public const int GAMEHALL_ENTERROOM_CREQ = 6;
        public const int GAMEHALL_ENTERROOM_BRQ = 7;

        public const int GAMEHALL_LEAVEROOM_CREQ = 8;
        public const int GAMEHALL_LEAVEROOM_BRQ = 9;

        public const int GAMEHALL_READY_CREQ = 10;
        public const int GAMEHALL_READY_BRQ = 11;


        public const int GAMEHALL_ENTERGAMEHALL_CREQ = 12;
        public const int GAMEHALL_ENTERGAMEHALL_BRQ = 13;

        public const int GAMEHALL_STARTGAME_CREQ = 14;
        public const int GAMEHALL_STARTGAME_BRQ = 15;


        public const int GAMEHALL_CHANGEDECK_CREQ = 16;
        public const int GAMEHALL_CHANGEDECK_BRQ = 17;

        public const int GAMEHALL_GAMEHALLMES_CREQ = 18;
        public const int GAMEHALL_GAMEHALLMES_BRQ = 19;
    }
}
