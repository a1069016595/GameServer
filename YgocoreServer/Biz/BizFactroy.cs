using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Biz
{
    public class BizFactory
    {
        public readonly static IAccountBiz accountBiz;

        public readonly static IUserBiz userBiz;

        public readonly static IGameHallBiz gameHallBiz;

        static BizFactory()
        {
            accountBiz = new AccountBiz();
            userBiz = new UserBiz();
            gameHallBiz = new GameHallBiz();
        }
    }
}
