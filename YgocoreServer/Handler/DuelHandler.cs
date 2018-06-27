using AceNetFrameWork.ace;
using LolServer.Cache;
using LolServer.Model;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace LolServer.Handler
{
    public class DuelHandler : HandlerInterface
    {

        IAccountCache cache = CacheFactory.accountCache;

        ConcurrentDictionary<UserToken, DuelRoom> roomMap;

        public DuelHandler()
        {
            roomMap = new ConcurrentDictionary<UserToken, DuelRoom>();
            EventUtil.Instance.duelStart += DuelStart;
            EventUtil.Instance.duelEnd += DuelEnd;
        }

        private void DuelEnd(UserToken hostMes, UserToken playerMes)
        {
            DuelRoom room;
            if (roomMap.TryGetValue(hostMes, out room))
            {
                roomMap.TryRemove(hostMes, out room);
                roomMap.TryRemove(playerMes, out room);
                room = null;
               Debug.LogTest("销毁游戏房间 "+roomMap.Count);
            }
        }

        private void DuelStart(UserToken hostMes, UserToken playerMes,DuelMesDTO hostDto,DuelMesDTO playerDto)
        {
            Console.WriteLine("新建房间");
            DuelRoom a = new DuelRoom(hostMes, playerMes,hostDto,playerDto);
            a.SetAccount(cache.getAccount(hostMes), cache.getAccount(playerMes));
            roomMap.TryAdd(hostMes, a);
            roomMap.TryAdd(playerMes, a);
        }


        public void ClientConnect(AceNetFrameWork.ace.UserToken token)
        {
            
        }

        public void Messagereceive(AceNetFrameWork.ace.UserToken token, AceNetFrameWork.ace.auto.SocketModel message)
        {
            roomMap[token].Messagereceive(token, message);
        }

        public void ClientClose(AceNetFrameWork.ace.UserToken token, string error)
        {
            DuelRoom room;
            if (roomMap.TryGetValue(token, out room))
            {
                room.ClientClose(token, error);
            }
        }
    }
}
