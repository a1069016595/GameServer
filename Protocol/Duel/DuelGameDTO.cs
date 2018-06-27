using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    [Serializable]
    public class DuelSelectFieldCardDTO
    {
        public bool isSelect;
        public int area;
        public int rank;
        public bool isMy;
    }
    [Serializable]
    public class DuelOperateCardDTO
    {
        public int area;
        public int rank;
        public string str;
        public bool isMy;
    }
    [Serializable]
    public class DuelSelectGroupCardDTO
    {
        public bool isSelect;
        public int rank;
    }
    [Serializable]
    public class DuelSelectGroupCardConDTO
    {
        /// <summary>
        /// 1 为下一页 2为上一页 3为确定
        /// </summary>
        public int CtrType;
    }
    [Serializable]
    public class DuelOperateDialogBoxDTO
    {
        public bool isTrue;
    }
    [Serializable]
    public class DuelChangePhaseDTO
    {
        public int phase;
    }

    [Serializable]

    public class DuelSelectPutTypeDto
    {
        public bool isAttack;
    }

    [Serializable]
    public class DuelApplySelectEffectDTO
    {
        public int rank;
    }

    [Serializable]
    public class DuelSelectEffectButtonDTO
    {
        public bool isRight;
    }

}
