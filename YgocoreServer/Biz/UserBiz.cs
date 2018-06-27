using AceNetFrameWork.ace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Biz
{
    public class UserBiz:IUserBiz
    {

        public int create(UserToken token, string account, string password)
        {
            return 0;
        }

        public int login(UserToken token, string account, string password)
        {
            return 0;
        }

        public void close(UserToken token)
        {
            throw new NotImplementedException();
        }

        public int get( UserToken token)
        {
            return 0;
        }

        public bool create( UserToken token, string name)
        {
            throw new NotImplementedException();
        }

        User IUserBiz.get( UserToken token)
        {
            throw new NotImplementedException();
        }

        public User get(int id)
        {
            throw new NotImplementedException();
        }

        public bool hasHero(int userId, int heroCode)
        {
            throw new NotImplementedException();
        }

        public void addHero(int userId, int heroCode)
        {
            throw new NotImplementedException();
        }

        public void addResult(int userId, int value)
        {
            throw new NotImplementedException();
        }

        public User online( UserToken token)
        {
            throw new NotImplementedException();
        }

        public void offline( UserToken token)
        {
            throw new NotImplementedException();
        }

        public User getByAccount( UserToken token)
        {
            throw new NotImplementedException();
        }

        public  UserToken getToken(int id)
        {
            throw new NotImplementedException();
        }
    }
}
