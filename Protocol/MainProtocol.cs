using System;
using System.Collections.Generic;

using System.Text;
namespace Protocol
{
    [Serializable]
    public class TypeProtocol
    {
        public const byte TYPE_LOGIN_CREQ = 0;
        public const byte TYPE_LOGIN_BRQ = 1;

        public const byte TYPE_GAMEHALL_CREQ = 2;
        public const byte TYPE_GAMEHALL_BRQ = 3;

        public const byte TYPE_DUEL_CREQ = 4;
        public const byte TYPE_DUEL_BRQ = 5; 
    }
}
