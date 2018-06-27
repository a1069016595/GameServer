using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolServer.Cache
{
    /// <summary>
    /// 存储游戏string变量
    /// </summary>
    public class ComStr
    {
        #region 卡片类型
        public const string CardType_Monster_Effect = "效果怪兽";
        public const string CardType_Monster_Normal = "通常怪兽";
        public const string CardType_Monster_Synchro = "同调怪兽";
        public const string CardType_Monster_XYZ = "超量怪兽";
        public const string CardType_Monster_Fusion = "融合怪兽";
        public const string CardType_Monster_Adjust = "调整怪兽";
        public const string CardType_Spell_Normal = "通常魔法";
        public const string CardType_Spell_Quick = "速攻魔法";
        public const string CardType_Spell_Continuous = "永续魔法";
        public const string CardType_Spell_Equit = "装备魔法";
        public const string CardType_Trap_Normal = "通常陷阱";
        public const string CardType_Trap_StrikeBack = "反击陷阱";
        public const string CardType_Trap_Continuous = "永续陷阱";
        #endregion

        #region 卡片种族
        public const string CardRace_Dragon = "龙";
        public const string CardRace_Zombie = "不死";
        public const string CardRace_Fiend = "恶魔";
        public const string CardRace_Pyro = "炎";
        public const string CardRace_SeaSerpent = "海龙";
        public const string CardRace_Rock = "岩石";
        public const string CardRace_Machine = "机械";
        public const string CardRace_Fish = "鱼";
        public const string CardRace_Dinosaur = "恐龙";
        public const string CardRace_Insect = "虫";
        public const string CardRace_Beast = "兽";
        public const string CardRace_BeastWarrior = "兽战士";
        public const string CardRace_Plant = "植物";
        public const string CardRace_Aqua = "水";
        public const string CardRace_Warrior = "战士";
        public const string CardRace_WingedBeast = "鸟兽";
        public const string CardRace_Fairy = "天使";
        public const string CardRace_Spellcaster = "魔法师";
        public const string CardRace_Thunder = "雷";
        public const string CardRace_Reptile = "爬虫";
        #endregion

        #region 卡片属性
        public const string CardAttr_Light = "光";
        public const string CardAttr_Dark = "暗";
        public const string CardAttr_Fire = "火";
        public const string CardAttr_Water = "水";
        public const string CardAttr_Wind = "风";
        public const string CardAttr_Earth = "地";

        #endregion

        #region 卡片操作选项
        public const string Operate_NormalSummon = "普通召唤";
        public const string Operate_SpecialSummon = "特殊召唤";
        public const string Operate_Set = "设置";
        public const string Operate_CheckList = "查看列表";
        public const string Operate_Attack = "攻击";
        public const string Operate_launch = "发动";
        public const string Operate_ChangeType = "转变";
        #endregion

        #region 卡片位置
        public const string Area_monster = "怪兽区";
        public const string Area_Trap = "魔陷区";
        public const string Area_Hand = "手牌";
        public const string Area_Graveyard = "墓地";
        public const string Area_Remove = "除外区";
        #endregion

        public const string EffectDescribe_40044918_1 = "选最多有这张卡以外的自己场上的名字带有「英雄」的怪兽数量的场上的魔法·陷阱卡破坏。";
        public const string EffectDescribe_40044918_2 = "从卡组把1只名字带有「英雄」的怪兽加入手卡。";

        public const string UI_DuelFieldUI = "DuelFieldUI";
        public const string UI_MainMenuUI = "MainMenuUI";
        public const string UI_EditCardUI = "EditCardUI";
        public const string UI_LoginUI = "LoginUI";
        public const string UI_GameHallUI = "GameHallUI";
        public const string UI_PerpareUI = "ErrorPlane";
    }
}
