using AceNetFrameWork.ace;
using LolServer.Biz;
using LolServer.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer
{
    public class GameHall : AbsMulitHandler, HandlerInterface
    {
        public List<UserToken> list = new List<UserToken>();

        public void ClientConnect(AceNetFrameWork.ace.UserToken token)
        {
            
        }

        public void Messagereceive(AceNetFrameWork.ace.UserToken token, AceNetFrameWork.ace.auto.SocketModel message)
        {
            
        }

        public void ClientClose(AceNetFrameWork.ace.UserToken token, string error)
        {
           
        }
    }
}
