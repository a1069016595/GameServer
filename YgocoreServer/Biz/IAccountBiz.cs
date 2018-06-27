using AceNetFrameWork.ace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LolServer.Biz
{
    public interface IAccountBiz
    {
        /// <summary>
        /// 创建帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        int create(UserToken token, string account, string password);


        int login(UserToken token, string account, string password);

        void close(UserToken token);

        int get(UserToken token);
    }
}
