using System.Collections.Generic;

namespace GameMain.Battle
{
    public abstract class PlayerCharacterLogic:CharacterLogicBase
    {
        public abstract List<ICard> AllCard { get; set; }
        public abstract List<ICard> HandCard { get; set; }
        public abstract List<ICard> RemainCard { get; set; }
        public abstract List<ICard> DestroyCard { get; set; }
        public abstract List<ICard> DiscardCard { get; set; }

        public void UseCard(ICard card, CharacterLogicBase target, CubeCoordinate coordinate)
        {
            card.UseCard(this, target, coordinate);
        }

        public abstract void GetAllCard();
        public abstract void AddCard(ICard card);
        

    }
}