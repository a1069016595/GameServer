using AceNetFrameWork.ace;
using LolServer.Biz;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceNetFrameWork.ace.auto;

namespace LolServer.Model
{
    class DuelRoom : AbsMulitHandler, HandlerInterface
    {
        UserToken hostToken;
        UserToken playerToken;

        Dictionary<UserToken, string> userMap;

        int player1GuessVal;
        int player2GuessVal;

        string hostAccount;
        string playerAccount;

        Deck hostDeck;
        Deck playerDeck;

        public DuelRoom(UserToken hostMes, UserToken playerMes, DuelMesDTO hostDto, DuelMesDTO playrDto)
        {
            hostToken = hostMes;
            playerToken = playerMes;
            list.Add(hostToken);
            list.Add(playerToken);
            hostDeck = hostDto.deck;
            playerDeck = playrDto.deck;

            Debug.LogTest("jj" + playerDeck.mainDeck.Length);

            setType(TypeProtocol.TYPE_DUEL_CREQ);
            setArea(0);

            player1GuessVal = 0;
            player2GuessVal = 0;
            hostAccount = null;
            playerAccount = null;
        }

        public void SetAccount(string _account1, string _account2)
        {
            userMap = new Dictionary<UserToken, string>();
            userMap.Add(hostToken, _account1);
            userMap.Add(playerToken, _account2);

            hostAccount = _account1;
            playerAccount = _account2;
        }



        public void ClientConnect(UserToken token)
        {

        }

        public void Messagereceive(UserToken token, SocketModel message)
        {
            Debug.LogTest("接受信息   "+GetTokenName(token)+"  "+ message.command);
            switch (message.command)
            {
                case DuelProtocol.GUESS_BRQ:
                    GuessFirst(token, message);
                    break;
                case DuelProtocol.STARTGAME_BRQ:
                    StartGame();
                    break;
                case DuelProtocol.GameFinish_BRQ:
                    EventUtil.Instance.duelEnd(hostToken, playerToken);
                    break;
                default:
                    SendToOther(token, message.command - 1, message.message);
                    break;
                //case DuelProtocol.DialogBoxSelect_BRQ:
                //    SendToOther(token, DuelProtocol.DialogBoxSelect_CREQ, message.message);
                //    break;
                //case DuelProtocol.OperateCard_BRQ:
                //    SendToOther(token, DuelProtocol.OperateCard_CREQ, message.message);
                //    break;
                //case DuelProtocol.SelectCardGroup_BRQ:
                //    SendToOther(token, DuelProtocol.SelectCardGroup_CREQ, message.message);
                //    break;
                //case DuelProtocol.SelectFieldCard_BRQ:
                //    SendToOther(token, DuelProtocol.SelectFieldCard_CREQ, message.message);
                //    break;
                //case DuelProtocol.SelectGroupCardCon_BRQ:
                //    SendToOther(token, DuelProtocol.SelectGroupCardCon_CREQ, message.message);
                //    break;
                //case DuelProtocol.ChangePhase_BRQ:
                //    SendToOther(token, DuelProtocol.ChangePhase_CREQ, message.message);
                //    break;
                //case DuelProtocol.SelectPutType_BRQ:
                //     SendToOther(token, DuelProtocol.SelectPutType_CREQ, message.message);
                //    break;
            }
        }

        private void SendToOther(UserToken token, int command, object mes)
        {
            write(GetOtherPlayer(token), TypeProtocol.TYPE_DUEL_CREQ, 0, command, mes);
        }


        private void SendToPlayer(UserToken token, int command, object mes)
        {
            write(token, TypeProtocol.TYPE_DUEL_CREQ, 0, command, mes);
        }

        /// <summary>
        /// 猜拳决定谁先
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        void GuessFirst(UserToken token, SocketModel message)
        {
            int val = message.getMessage<int>();
          //  Debug.LogTest("接受信息前：   "+ player1GuessVal + "  " + player2GuessVal);
            if (userMap[token] == hostAccount)
            {
            //    Debug.LogTest("玩家1：   "+val );
                player1GuessVal = val;
            }
            else if (userMap[token] == playerAccount)
            {
              //  Debug.LogTest("玩家2：   " + val);
                player2GuessVal = val;
            }
            else
            {
                Console.WriteLine("error");
            }

            Debug.LogTest(player1GuessVal + "  " + player2GuessVal);

            if (player1GuessVal != 0 && player2GuessVal != 0)
            {

                DuelGuessMesDTO dto = new Protocol.DuelGuessMesDTO();
                dto.account1 = userMap[hostToken];
                dto.account1Val = player1GuessVal;
                dto.account2 = userMap[playerToken];
                dto.account2Val = player2GuessVal;



                brocast(DuelProtocol.GUESS_CREQ, dto);
                player1GuessVal = 0;
                player2GuessVal = 0;
            }
        }

        

        public void ClientClose(UserToken token, string error)
        {
            string val;
            val = GetTokenName(token);
            Debug.LogError(val+"  "+ error);
            SendToOther(token, DuelProtocol.GameFinish_CREQ, null);
            EventUtil.Instance.duelEnd(hostToken, playerToken);
        }

        private string GetTokenName(UserToken token)
        {
            string val;
            if (token == hostToken)
            {
                val = "房主";
            }
            else
            {
                val = "玩家";
            }
            return val;
        }

        void StartGame()
        {
            ShuffleCard(hostDeck);
            ShuffleCard(playerDeck);

            DeckMes deckMes1 = new DeckMes();
            deckMes1.accountName = hostAccount;
            deckMes1.deck = hostDeck;

            DeckMes deckMes2 = new DeckMes();
            deckMes2.accountName = playerAccount;
            deckMes2.deck = playerDeck;

          

            SendToPlayer(hostToken, DuelProtocol.STARTGAME_CREQ, new StartGameDTO(deckMes1, deckMes2));
            SendToPlayer(playerToken, DuelProtocol.STARTGAME_CREQ, new StartGameDTO(deckMes2, deckMes1));
        }

        void ShuffleCard(Deck _deck)
        {
           // Deck deck = new Deck();
            int count = _deck.mainDeck.Length;
            for (int i = 0; i < count; i++)
            {
                int a = GetRandom.Random(0, count);
                string val = _deck.mainDeck[a];
                _deck.mainDeck[a] = _deck.mainDeck[count - i - 1];
                _deck.mainDeck[count - i - 1] = val;
            }
        }

        private UserToken GetOtherPlayer(UserToken token)
        {
            if (token == hostToken)
            {
                return playerToken;
            }
            else
            {
                return hostToken;
            }
        }
    }
}

public class GetRandom
{
    public static int Random(int min, int max)
    {
        Random r = new Random();
        byte[] buffer = Guid.NewGuid().ToByteArray();
        int iSeed = BitConverter.ToInt32(buffer, 0);
        r = new Random(iSeed);
        int result = r.Next(min, max);
        return result;
    }
}