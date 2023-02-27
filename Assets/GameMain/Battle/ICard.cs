using System.Collections.Generic;

namespace GameMain.Battle
{
    public interface ICard
    {
        public int Cost { get; }
        public List<BuffBase> Buff { get; }
        public void UseCard(CharacterLogicBase src, CharacterLogicBase target, CubeCoordinate coordinate);
    }
}