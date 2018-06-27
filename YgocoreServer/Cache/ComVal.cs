using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Cache
{

    //统计代码行数 b*[^:b#/]+.*$
    /// <summary>
    /// 存储游戏变量数值
    /// </summary>
    public class ComVal
    {
        #region 卡片类型

        public const int CardType_Monster_Normal = 0x1;
        public const int CardType_Monster_Effect = 0x2;
        public const int CardType_Monster_Adjust = 0x4;
        /// <summary>
        /// 融合怪兽
        /// </summary>
        public const int CardType_Monster_Fusion = 0x8;
        /// <summary>
        /// 同调怪兽
        /// </summary>
        public const int CardType_Monster_Synchro = 0x10;
        /// <summary>
        /// 超量怪兽
        /// </summary>
        public const int CardType_Monster_XYZ = 0x20;
        public const int CardType_Spell_Normal = 0x40;
        public const int CardType_Spell_Quick = 0x80;
        public const int CardType_Spell_Continuous = 0x100;
        public const int CardType_Spell_Equit = 0x200;
        public const int CardType_Trap_Normal = 0x400;
        public const int CardType_Trap_StrikeBack = 0x800;
        public const int CardType_Trap_Continuous = 0x1000;

        public const int cardType_Monster = CardType_Monster_Adjust | CardType_Monster_Effect | CardType_Monster_Fusion | CardType_Monster_Normal
          | CardType_Monster_Synchro | CardType_Monster_XYZ;
        public const int cardType_Spell = CardType_Spell_Continuous | CardType_Spell_Equit | CardType_Spell_Normal |
            CardType_Spell_Quick;
        public const int cardType_Trap = CardType_Trap_Continuous | CardType_Trap_Normal | CardType_Trap_StrikeBack;
        #endregion

        #region 卡片种族
        public const int CardRace_Dragon = 1;
        public const int CardRace_Zombie = 2;
        public const int CardRace_Fiend = 3;
        public const int CardRace_Pyro = 4;
        public const int CardRace_SeaSerpent = 5;
        public const int CardRace_Rock = 6;
        public const int CardRace_Machine = 7;
        public const int CardRace_Fish = 8;
        public const int CardRace_Dinosaur = 9;
        public const int CardRace_Insect = 10;
        public const int CardRace_Beast = 11;
        public const int CardRace_BeastWarrior = 12;
        public const int CardRace_Plant = 13;
        public const int CardRace_Aqua = 14;
        public const int CardRace_Warrior = 15;
        public const int CardRace_WingedBeast = 16;
        public const int CardRace_Fairy = 17;
        public const int CardRace_Spellcaster = 18;
        public const int CardRace_Thunder = 19;
        public const int CardRace_Reptile = 20;
        #endregion

        #region 卡片属性
        public const int CardAttr_Light = 0x01;
        public const int CardAttr_Dark = 0x02;
        public const int CardAttr_Fire = 0x04;
        public const int CardAttr_Water = 0x08;
        public const int CardAttr_Wind = 0x10;
        public const int CardAttr_Earth = 0x20;
        #endregion

        #region 卡片摆放形态
        /// <summary>
        /// 正面竖放
        /// </summary>
        public const int CardPutType_UpRightFront = 1;
        /// <summary>
        /// 正面横放
        /// </summary>
        public const int CardPutType_layFront = 2;

        /// <summary>
        /// 背面横放
        /// </summary>
        public const int CardPutType_layBack = 3;
        /// <summary>
        /// 背面竖放
        /// </summary>
        public const int CardPutType_UpRightBack = 4;
        #endregion

        #region 游戏阶段
        public const int Phase_Drawphase = 1;
        public const int Phase_Standbyphase = 2;
        public const int Phase_Mainphase1 = 3;
        public const int Phase_Battlephase = 4;
        public const int Phase_Mainphase2 = 5;
        public const int Phase_Endphase = 6;
        #endregion

        #region 卡片位置
        public const int Area_monster = 1;
        public const int Area_Trap = 2;
        public const int Area_Hand = 3;
        public const int Area_Graveyard = 4;
        public const int Area_Remove = 5;
        public const int Area_MainDeck = 6;
        #endregion

        #region 卡片操作选项

        #endregion

        #region 时点
        public const int code_FreeCode = 0x0;
        public const int code_EnterDrawPhase = 0x1;
        public const int code_EnterStandByPhase = 0x2;
        public const int code_EnterMainPhase1 = 0x4;
        public const int code_EnterBattlePhase = 0x8;
        public const int code_EnterMainPhase2 = 0x10;
        public const int code_EnterEndPhase = 0x20;
        public const int code_NormalSummon = 0x40;
        public const int code_DestroyCard = 0x80;
        #endregion

        #region 卡片效果种类
        public const int cardEffectType_mustLauch = 0x1;
        public const int cardEffectType_chooseLauch = 0x2;
        #endregion

        /// <summary>
        /// 判断是否为怪物
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public static bool isMonster(int cardType)
        {
            if (cardType > 0 || cardType < CardType_Spell_Normal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为额外卡
        /// </summary>
        /// <returns></returns>
        public static bool isInExtra(int cardType)
        {
            if (cardType == ComVal.CardType_Monster_Fusion || cardType == ComVal.CardType_Monster_Synchro ||
                   cardType == ComVal.CardType_Monster_XYZ)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检测a是否属于b
        /// </summary>
        /// <returns></returns>
        public static bool isBind(int a, int b)
        {
            if ((a & b) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

}














