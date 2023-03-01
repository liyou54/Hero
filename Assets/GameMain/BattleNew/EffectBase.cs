using System.Collections.Generic;
using GameMain.BattleNew;

namespace GameMain.BattleNew
{
    public abstract class EffectBase
    {
        public BuffBase Buff;
        public CharacterLogicBase Master;
        public List<CharacterLogicBase> Target;
        public abstract void EffectStart();

        public virtual void EffectExcute()
        {
            Buff.Excute(Master, Target);
        }
        public abstract void EffectEnd();
    }
}