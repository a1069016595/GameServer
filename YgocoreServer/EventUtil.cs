using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using AceNetFrameWork.ace;
using LolServer.Model;
namespace LolServer
{
    public class EventUtil
    {
        private static EventUtil util;

        public static EventUtil Instance
        {
            get
            {
                if (util == null)
                    util = new EventUtil();
                return util;
            }
        }


        public delegate void DuelStart(UserToken hostMes, UserToken playerMes,DuelMesDTO hostDto,DuelMesDTO playerDto);
        public DuelStart duelStart;

        public delegate void DuelEnd(UserToken hostMes, UserToken playerMes);
        public DuelEnd duelEnd;
    }
}
