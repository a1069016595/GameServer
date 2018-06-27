using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceNetFrameWork;
using Protocol;
using AceNetFrameWork.ace;
using AceNetFrameWork.ace.auto;
using LolServer.Handler;

namespace LolServer
{
    public class HandlerCenter : AbsHandlerCenter
    {
        HandlerInterface login;
        HandlerInterface gameHall;
        HandlerInterface duel;
        public HandlerCenter()
        {
            login = new LoginHandler();
            gameHall = new GameHallHandler();
            duel = new DuelHandler();
        }

        public override void ClientConnect(UserToken token)
        {
            Console.WriteLine("有客户端连接");
        }
       

        public override void ClientClose(UserToken token, string error)
        {
            Console.WriteLine("有客户端断开连接  "+error);

            gameHall.ClientClose(token, error);
            login.ClientClose(token, error);
            duel.ClientClose(token, error);
        }

        public override void MessageReceive(UserToken token, object message)
        {

            SocketModel model = message as SocketModel;

            switch (model.type)
            {
                case TypeProtocol.TYPE_LOGIN_BRQ:
                    login.Messagereceive(token, model);
                    break;
                case TypeProtocol.TYPE_GAMEHALL_BRQ:
                    gameHall.Messagereceive(token, model);
                    break;
                case TypeProtocol.TYPE_DUEL_BRQ:
                    duel.Messagereceive(token, model);
                    break;
            }
        }
    }
}
