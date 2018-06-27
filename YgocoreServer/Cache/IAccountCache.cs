using AceNetFrameWork.ace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Cache
{

    interface IAccountCache
    {
        /// <summary>
        /// 获取帐号
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string getAccount(UserToken token);

        /// <summary>
        /// 帐号是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool hasAccount(string account);
        /// <summary>
        /// 帐号密码是否匹配
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool match(string account, string password);
        /// <summary>
        /// 帐号是否在线
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool isOnline(string account);
        /// <summary>
        /// 获取当前用户 帐号ID
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int getId(UserToken token);
        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        void online(UserToken token, string account);
        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="token"></param>
        void offline(UserToken token);

        /// <summary>
        /// 添加帐号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        void add(string account, string password);

        /// <summary>
        /// 获取在线的token
        /// </summary>
        /// <returns></returns>
        List<UserToken> getOnlineTokenList();

        /// <summary>
        /// 获取大厅的token
        /// </summary>
        /// <returns></returns>
        List<UserToken> getGameHallTokenList();

        /// <summary>
        /// 将用户移到房间map当中
        /// </summary>
        /// <param name="token"></param>
        void addToRoomAccMap(UserToken token);

        /// <summary>
        /// 将用户移除出房间map
        /// </summary>
        /// <param name="token"></param>
        void removeRoomAccMap(UserToken token);

        /// <summary>
        /// 将用户添加到游戏大厅
        /// </summary>
        /// <param name="token"></param>
        void addToGameHallAccMap(UserToken token);


        /// <summary>
        /// 将用户移除到游戏大厅
        /// </summary>
        /// <param name="token"></param>
        void removeGameHallAccMap(UserToken token);



        int GetOnLineNum();

        int GetStandByNum();

        int GetPlayingNum();

    }
}
