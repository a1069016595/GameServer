using LolServer.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Biz
{
    class AccountBiz:IAccountBiz
    {
        public int val = 0;

        IAccountCache accountCache = CacheFactory.accountCache;

        public int create(AceNetFrameWork.ace.UserToken token, string account, string password)
        {
            return 0;
        }

        public int login(AceNetFrameWork.ace.UserToken token, string account, string password)
        {
            //if (!accountCache.hasAccount(account))
            //{
            //    return Login_Result_Protool.AccountNotExit;
            //}
            if (accountCache.isOnline(account))
            {
                return Login_Result_Protool.UserIsOnLine;
            }
            //if (!accountCache.match(account, password))
            //{
            //    return Login_Result_Protool.PasswordError;
            //}

            accountCache.online(token, account);
            return Login_Result_Protool.LoginSuccess;
        }

        public void close(AceNetFrameWork.ace.UserToken token)
        {
            accountCache.offline(token);
            Console.WriteLine("关闭token");
        }

        public int get(AceNetFrameWork.ace.UserToken token)
        {
            throw new NotImplementedException();
        }
    }
}
