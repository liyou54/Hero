using System.Collections.Generic;

namespace GameMain.BattleNew
{
    public abstract class CardBase
    {
        public abstract bool IsOnceCard { get; set; }
        public abstract List<BuffBase> Buffs { get; set; }
        public abstract int Cost { get; set; }

        public virtual bool BeforeUse(CharacterLogicPlayer master, List<CharacterLogicBase> target)
        {
            if (master.ActionPoint > Cost)
            {
                return true;
            }

            return false;
        }

        public virtual void Use(CharacterLogicPlayer master, List<CharacterLogicBase> target)
        {
            master.HandCards.Remove(this);
            if (IsOnceCard)
            {
                master.DiscardCards.Add(this);
            }
            else
            {
                master.DeckCards.Add(this);
            }

            master.ActionPoint -= Cost;
            
        }

        public virtual void AfterUse(CharacterLogicPlayer master, List<CharacterLogicBase> target)
        {
            
        }
    }
}