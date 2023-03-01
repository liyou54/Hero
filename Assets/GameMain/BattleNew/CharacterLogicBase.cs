using System.Collections.Generic;

namespace GameMain.BattleNew
{
    public abstract class CharacterLogicBase
    {
        public bool IsFinish;
        public abstract List<BuffBase> Buffs{ get; set; }
        public abstract void OnCharacterRound();
        public abstract void OnStartCharacterRound();
        public abstract void OnFinishCharacterRound();
        protected  void FinishCharacterRound()
        {
            IsFinish = true;
        }
    }
}