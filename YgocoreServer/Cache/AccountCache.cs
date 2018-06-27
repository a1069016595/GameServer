using AceNetFrameWork.ace;
using LolServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Cache
{
    public class AccountCache : IAccountCache
    {
        //token与account的映射
        Dictionary<UserToken, Account> onlineTokenMap = new Dictionary<UserToken, Account>();

        Dictionary<string, Account> onLineAccountMap = new Dictionary<string, Account>();

        //account与Account类的映射
      //  Dictionary<string, Account> accMap = new Dictionary<string, Account>();

        //在房间内的用户
        Dictionary<UserToken, string> roomAccMap = new Dictionary<UserToken, string>();
        //在大厅内的用户
        Dictionary<UserToken, string> gameHallAccMap = new Dictionary<UserToken, string>();

        AtomicInt myAtomic;

        public AccountCache()
        {
            myAtomic = new AtomicInt();

        }

        public bool hasAccount(string account)
        {
            return true;
           // return accMap.ContainsKey(account);
        }

        public bool match(string account, string password)
        {
            //if (hasAccount(account))
            //{
            //    if (accMap[account].password == password)
            //    {
            //        return true;
            //    }
            //}
            //return false;

            return true;
        }

        public bool isOnline(string account)
        {
            return onLineAccountMap.ContainsKey(account);
        }

        public int getId(AceNetFrameWork.ace.UserToken token)
        {
            if (!onlineTokenMap.ContainsKey(token))
            {
                return -1;
            }
            return onlineTokenMap[token].id;
        }

        public void online(AceNetFrameWork.ace.UserToken token, string account)
        {
            Account acc = new Account();
            acc.account = account;
            acc.id = myAtomic.getAndAdd();
            onlineTokenMap.Add(token, acc);
            onLineAccountMap.Add(account, acc);

            addToGameHallAccMap(token);
        }

        public void offline(AceNetFrameWork.ace.UserToken token)
        {
            if (onlineTokenMap.ContainsKey(token))
            {
                string accout = onlineTokenMap[token].account;
                onLineAccountMap.Remove(accout);
                onlineTokenMap.Remove(token);
            }
            removeGameHallAccMap(token);
            removeRoomAccMap(token);
        }

        public void add(string account, string password)
        {
            //Account acc = new Account();
            //acc.account = account;
            //acc.password = password;
            //acc.id = myAtomic.getAndAdd();
            //accMap.Add(account, acc);
        }

        public string getAccount(UserToken token)
        {
            if (onlineTokenMap.ContainsKey(token))
                return onlineTokenMap[token].account;
            else
                return null;
        }

        public List<UserToken> getOnlineTokenList()
        {
            List<UserToken> list = new List<UserToken>();
            list.AddRange(onlineTokenMap.Keys);
            return list;
        }


        public void addToRoomAccMap(UserToken token)
        {
            string acc = getAccount(token);
            if (acc != null)
                roomAccMap.Add(token, acc);
        }
        public void removeRoomAccMap(UserToken token)
        {
            if (roomAccMap.ContainsKey(token))
                roomAccMap.Remove(token);
        }

        public void addToGameHallAccMap(UserToken token)
        {
            string acc = getAccount(token);
            if (acc != null)
                gameHallAccMap.Add(token, acc);
        }

        public void removeGameHallAccMap(UserToken token)
        {

            if (gameHallAccMap.ContainsKey(token))
                gameHallAccMap.Remove(token);
        }

        public List<UserToken> getGameHallTokenList()
        {
            List<UserToken> list = new List<UserToken>();
            list.AddRange(gameHallAccMap.Keys);
            return list;
        }

        public int GetOnLineNum()
        {
            return onlineTokenMap.Count;
        }

        public int GetStandByNum()
        {
            return gameHallAccMap.Count;
        }

        public int GetPlayingNum()
        {
            return roomAccMap.Count;
        }
    }
}
