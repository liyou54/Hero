using System.Collections.Generic;

namespace GameMain.BattleNew
{
    public abstract class CharacterLogicPlayer : CharacterLogicBase
    {
        public List<CardBase> AllCards;
        public List<CardBase> HandCards;
        public List<CardBase> DeckCards;
        public List<CardBase> DiscardCards;

        public abstract int ActionPoint { get; set; }
        public abstract int MaxActionPoint { get; set; }
    }
}