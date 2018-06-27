using AceNetFrameWork.ace;
using AceNetFrameWork.ace.auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Biz
{
    public interface IGameHallBiz
    {

        int CreateRoom(UserToken token, SocketModel message);

        void Chat(UserToken token, SocketModel message);

        int EnterRoom(UserToken token, SocketModel message);

        int LeaveRoom(UserToken token, SocketModel message);
        /// <summary>
        /// 用于向刚登陆的用户发送房间列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        void SendRoomListToUser(UserToken token);

        void Ready(UserToken token, SocketModel message);
        /// <summary>
        /// 用户连接关闭
        /// </summary>
        /// <param name="token"></param>
        void Close(UserToken token);
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mes"></param>
        void StartGame(UserToken token, SocketModel mes);

        void ChangeDeck(UserToken token, SocketModel mes);
    }
}
