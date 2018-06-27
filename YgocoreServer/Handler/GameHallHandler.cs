using LolServer.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;

namespace LolServer.Handler
{
    class GameHallHandler : HandlerInterface
    {
        IGameHallBiz biz = BizFactory.gameHallBiz;


        public void ClientConnect(AceNetFrameWork.ace.UserToken token)
        {

        }

        public void Messagereceive(AceNetFrameWork.ace.UserToken token, AceNetFrameWork.ace.auto.SocketModel message)
        {
            switch (message.command)
            {
                case GameHallProtocol.GAMEHALL_CHAT_BRQ:
                    biz.Chat(token, message);
                    break;
                case GameHallProtocol.GAMEHALL_CREATEROOM_BRQ:
                    biz.CreateRoom(token, message);
                    break;
                case GameHallProtocol.GAMEHALL_ENTERGAMEHALL_BRQ:
                    biz.SendRoomListToUser(token);
                    break;
                case GameHallProtocol.GAMEHALL_ENTERROOM_BRQ:
                    biz.EnterRoom(token, message);
                    break;
                case GameHallProtocol.GAMEHALL_LEAVEROOM_BRQ:
                    biz.LeaveRoom(token, message);
                    break;
                case GameHallProtocol.GAMEHALL_READY_BRQ:
                    biz.Ready(token, message);
                    break;
                case GameHallProtocol.GAMEHALL_STARTGAME_BRQ:
                    biz.StartGame(token, message);
                    break;
                case GameHallProtocol.GAMEHALL_CHANGEDECK_BRQ:
                    biz.ChangeDeck(token, message);
                    break;
                default:
                    break;
            }
        }

        public void ClientClose(AceNetFrameWork.ace.UserToken token, string error)
        {
            ExecutorPool.Instance.execute
            (
                delegate()
                {
                    biz.Close(token);
                }
            );
          
        }
    }
}
