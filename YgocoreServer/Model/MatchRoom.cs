using AceNetFrameWork.ace;
using LolServer.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
namespace LolServer.Model
{
    public class MatchRoom : AbsMulitHandler
    {
        /// <summary>
        /// 房主用户名
        /// </summary>
        public string hostAccount;

        /// <summary>
        /// 房主token
        /// </summary>
        public UserToken hostToken;

        /// <summary>
        /// 房主id
        /// </summary>
        public int HostID;
        /// <summary>
        /// 玩家id
        /// </summary>
        public int playerID = 0;
        /// <summary>
        /// 玩家用户名
        /// </summary>
        public string playerAccount;


        /// <summary>
        /// 是否正在游戏
        /// </summary>
        public bool isPlaying;

        /// <summary>
        /// 是否满员
        /// </summary>
        public bool isFill;

        public DuelMesDTO hostDto;
        public DuelMesDTO playerDto;

        public MatchRoom(UserToken token)
        {
            enter(token);
            hostToken = token;
            setType(TypeProtocol.TYPE_GAMEHALL_CREQ);
            setArea(0);
            playerDto = new DuelMesDTO();
        }

        public void EnterRoom(UserToken token, int id, string account)
        {
            if (isFill)
            {
                return;
            }
            playerID = id;
            playerAccount = account;
            isFill = true;
            playerDto.account = account;
            enter(token);
            write(token, TypeProtocol.TYPE_GAMEHALL_CREQ, GameHallProtocol.GAMEHALL_ENTERROOM_CREQ, hostAccount);
            write(hostToken, TypeProtocol.TYPE_GAMEHALL_CREQ, GameHallProtocol.GAMEHALL_ENTERROOM_CREQ, account);
        }
        /// <summary>
        /// 若不存在用户,返回false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void LeaveRoom(UserToken token, int id)
        {
            isFill = false;
            leave(token);
            if (HostID == id)
            {
                HostID = playerID;//改变房主
                if (list.Count != 0)
                {
                    hostToken = list[0];
                    hostAccount = playerAccount;
                }
                playerID = 0;
            }
            else if (playerID == id)
            {
                playerAccount = "";
                playerID = 0;
            }
            if (!isEmpty())
            {
                write(hostToken, TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_LEAVEROOM_CREQ, null);
            }
        }

        public bool PlayerReady(bool isReady)
        {
            playerDto.isReady = isReady;
            brocast(TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_READY_CREQ, isReady);
            return isReady;
        }

        public void SetPlayerDeck(Deck deck)
        {
            playerDto.deck = deck;
        }

        public void StartGame(DuelMesDTO dto)
        {
            hostDto = dto;

            EventUtil.Instance.duelStart(list[0], list[1], hostDto, playerDto);

            DuelRoomMesDTO duelDto = new DuelRoomMesDTO();
            duelDto.dto1 = hostDto;
            duelDto.dto2 = playerDto;
            brocast(TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_STARTGAME_CREQ, duelDto);
        }

        public bool isEmpty()
        {
            if (HostID == 0)
                return true;
            else
                return false;
        }
    }
}
