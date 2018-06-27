using AceNetFrameWork.ace.auto;
using LolServer.Cache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using AceNetFrameWork.ace;
using LolServer.Model;
using System.Threading;

namespace LolServer.Biz
{

    /// <summary>
    /// 游戏大厅
    /// </summary>
    public class GameHallBiz : AbsMulitHandler, IGameHallBiz
    {
        IAccountCache cache = CacheFactory.accountCache;

        /// <summary>
        /// 匹配房间与其对应的序号
        /// </summary>
        ConcurrentDictionary<int, MatchRoom> matchRoomMap = new ConcurrentDictionary<int, MatchRoom>();
        /// <summary>
        /// 玩家id与房间id的映射
        /// </summary>
        ConcurrentDictionary<int, int> userMatch = new ConcurrentDictionary<int, int>();


        List<RoomInfoDTO> roomInfoList = new List<RoomInfoDTO>();

        AtomicInt atomic = new AtomicInt();

        Thread sendGameHallMesThread;

        public GameHallBiz()
        {
            EventUtil.Instance.duelEnd += DuelEnd;
            sendGameHallMesThread = new Thread(SendGameHallMes);
            sendGameHallMesThread.Start();
        }

        private void SendGameHallMes()
        {
            while(true)
            {
                Thread.Sleep(1000);
                GameHallMesDTO mes = new GameHallMesDTO();
                mes.onlineNum = cache.GetOnLineNum();
                mes.playingNum = cache.GetPlayingNum();
                mes.standbyNum = cache.GetStandByNum();
                list = cache.getGameHallTokenList();
                brocast(TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_GAMEHALLMES_CREQ, mes);
            }
        }

        private void DuelEnd(UserToken hostMes, UserToken playerMes)
        {
            int roomID;
            int playerID = cache.getId(hostMes);
            int player2ID = cache.getId(playerMes);
            if (userMatch.TryGetValue(playerID, out roomID))
            {
                MatchRoom room;
                if (matchRoomMap.TryRemove(roomID, out room))
                {
                    matchRoomMap.TryRemove(player2ID, out room);
                    RemoveRoomInfo(roomID);
                    room = null;
                    atomic.getAndReduce();
                    Debug.LogTest("销毁比赛房间" + matchRoomMap.Count);

                    cache.addToGameHallAccMap(hostMes);
                    cache.removeRoomAccMap(hostMes);

                    cache.addToGameHallAccMap(playerMes);
                    cache.removeRoomAccMap(playerMes);
                }
            }
        }


        public int CreateRoom(AceNetFrameWork.ace.UserToken token, SocketModel mes)
        {
            string roomName = mes.getMessage<string>();

            MatchRoom matchRoom = new MatchRoom(token);
            int playerID = cache.getId(token);
            string account = cache.getAccount(token);
            int roomID = atomic.getAndAdd();
            matchRoom.HostID = playerID;
            matchRoom.hostAccount = account;
            if (matchRoomMap.TryAdd(roomID, matchRoom))
            {
                userMatch.TryAdd(playerID, roomID);
            }
            else
            {
                //如果添加房间失败，则发送失败消息
                Console.WriteLine("无法添加房间");
                write(token, TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_CREATEROOM_CREQ, 0);
                return 0;
            }
            RoomInfoDTO info = new RoomInfoDTO();
            info.roomID = roomID; info.roomName = roomName; info.roomState = "正在等待"; info.roomOwner = account;
            roomInfoList.Add(info);


            write(token, TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_CREATEROOM_CREQ, info);

            cache.removeGameHallAccMap(token);//将用户移除出大厅列表 加入到房间列表中
            cache.addToRoomAccMap(token);

            UpdateRoomListToAll();

            return 1;
        }



        public void Chat(AceNetFrameWork.ace.UserToken token, SocketModel mes)
        {
            ChatMesDTO dto = mes.getMessage<ChatMesDTO>();
            string account = cache.getAccount(token);
            dto.userName = account;
            list = cache.getGameHallTokenList();
            brocast(TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_CHAT_CREQ, dto);

            // return 1;
        }

        public int EnterRoom(UserToken token, SocketModel message)
        {
            int roomID = message.getMessage<int>();
            MatchRoom room = matchRoomMap[roomID];
            int userID = cache.getId(token);
            userMatch.TryAdd(userID, roomID);
            string account = cache.getAccount(token);
            room.EnterRoom(token, userID, account);
            cache.removeGameHallAccMap(token);
            cache.addToRoomAccMap(token);

            UpdateRoomState(room, roomID);
            UpdateRoomListToAll();
            return 1;
        }

        public int LeaveRoom(UserToken token, SocketModel message)
        {
            int userID = cache.getId(token);
            int roomID;
            userMatch.TryGetValue(userID, out roomID);
            MatchRoom room;
            if (!matchRoomMap.TryGetValue(roomID, out room))
            {
                return 0;
            }
            room.LeaveRoom(token, userID);
            int a;
            userMatch.TryRemove(userID, out a);
            cache.removeRoomAccMap(token);
            cache.addToGameHallAccMap(token);
            if (room.isEmpty())
            {
                MatchRoom b = new MatchRoom(null);
                matchRoomMap.TryRemove(roomID, out b);
                RemoveRoomInfo(roomID);
                //  Console.WriteLine("fw3");

            }
            else
            {
                foreach (var item in roomInfoList)
                {
                    if (item.roomID == roomID)
                    {
                        item.roomOwner = room.hostAccount;
                        UpdateRoomState(room, roomID);
                    }
                }
            }
            UpdateRoomListToAll();
            return 1;
        }

        private void UpdateRoomState(MatchRoom room, int roomID)
        {
            if (room.isFill)
            {
                getRoomInfo(roomID).roomState = "准备开始";
            }
            else
            {
                getRoomInfo(roomID).roomState = "正在等待";
            }
        }

        private void RemoveRoomInfo(int roomID)
        {
            foreach (var item in roomInfoList)
            {
                if (item.roomID == roomID)
                {
                    roomInfoList.Remove(item);
                    break;
                }
            }
        }


        public void SendRoomListToUser(UserToken token)
        {
            if (roomInfoList.Count == 0)
            {
                RoomListDTO listDto = new RoomListDTO();
                listDto.roomCount = 0;
                write(token, TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_ENTERGAMEHALL_CREQ, listDto);
            }
            else
            {
                RoomInfoDTO[] array = new RoomInfoDTO[roomInfoList.Count];
                array = roomInfoList.ToArray();
                RoomListDTO listDto = new RoomListDTO();
                listDto.roomCount = array.Length;
                listDto.roomList = array;
                write(token, TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_ENTERGAMEHALL_CREQ, listDto);
            }
        }


        public void Ready(UserToken token, SocketModel message)
        {
            DuelMesDTO dto = message.getMessage<DuelMesDTO>();

            int id = cache.getId(token);
            MatchRoom room = matchRoomMap[userMatch[id]];

            room.PlayerReady(dto.isReady);
        }

        public void ChangeDeck(UserToken token, SocketModel message)
        {
            DuelMesDTO dto = message.getMessage<DuelMesDTO>();

            int id = cache.getId(token);
            MatchRoom room = matchRoomMap[userMatch[id]];

            room.SetPlayerDeck(dto.deck);
        }

        /// <summary>
        /// 更新房间列表消息到用户大厅的用户
        /// </summary>
        void UpdateRoomListToAll()
        {
            Debug.LogTest("更新房间信息");
            if (roomInfoList.Count == 0)
            {
                RoomListDTO listDto = new RoomListDTO();
                listDto.roomCount = 0;
                list = cache.getGameHallTokenList();
                brocast(TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_ROOMLIST_CREQ, listDto);
                }
            else
            {

                RoomInfoDTO[] array = roomInfoList.ToArray();
                RoomListDTO listDto = new RoomListDTO();
                listDto.roomList = array;
                listDto.roomCount = array.Length;
                list = cache.getGameHallTokenList();
                brocast(TypeProtocol.TYPE_GAMEHALL_CREQ, 0, GameHallProtocol.GAMEHALL_ROOMLIST_CREQ, listDto);
            }
        }


        public void Close(UserToken token)
        {
            if (userMatch.ContainsKey(cache.getId(token)))
                LeaveRoom(token, null);
            cache.removeGameHallAccMap(token);
            cache.removeRoomAccMap(token);
        }


        public void StartGame(UserToken token, SocketModel mes)
        {
            DuelMesDTO dto = mes.getMessage<DuelMesDTO>();
            int userID = cache.getId(token);
            MatchRoom room = matchRoomMap[userMatch[userID]];
            room.StartGame(dto);

            getRoomInfo(userMatch[userID]).roomState = "正在游戏";
            UpdateRoomListToAll();
        }

        /// <summary>
        /// 通过id获取房间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoomInfoDTO getRoomInfo(int id)
        {
            for (int i = 0; i < roomInfoList.Count; i++)
            {
                if (roomInfoList[i].roomID == id)
                {
                    return roomInfoList[i];
                }
            }
            return null;
        }
    }
}
