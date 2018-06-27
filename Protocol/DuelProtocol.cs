using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    [Serializable]
    public class DuelProtocol
    {
        public const byte GUESS_CREQ = 0;
        public const byte GUESS_BRQ = 1;

        public const byte STARTGAME_CREQ = 2;
        public const byte STARTGAME_BRQ = 3;

        public const byte SelectFieldCard_CREQ = 4;
        public const byte SelectFieldCard_BRQ = 5;

        public const byte DialogBoxSelect_CREQ = 6;
        public const byte DialogBoxSelect_BRQ = 7;

        public const byte SelectCardGroup_CREQ = 8;
        public const byte SelectCardGroup_BRQ = 9;

        public const byte OperateCard_CREQ = 10;
        public const byte OperateCard_BRQ = 11;

        public const byte SelectGroupCardCon_CREQ = 12;
        public const byte SelectGroupCardCon_BRQ = 13;

        public const byte ChangePhase_CREQ = 14;
        public const byte ChangePhase_BRQ = 15;

        public const byte SelectPutType_CREQ = 16;
        public const byte SelectPutType_BRQ = 17;

        public const byte ChangeSelectEffect_CREQ = 18;
        public const byte ChangeSelectEffect_BRQ = 19;

        public const byte ApplySelectEffect_CREQ = 20;
        public const byte ApplySelectEffect_BRQ = 21;

        public const byte GameFinish_CREQ = 22;
        public const byte GameFinish_BRQ = 23;

        public const byte Surrender_CREQ = 24;
        public const byte Surrender_BRQ = 25;
    }
}
