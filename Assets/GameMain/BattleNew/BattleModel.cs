using System.Collections.Generic;

namespace GameMain.BattleNew
{

    
    public class BattleModel
    {
        // 角色
        public List<CharacterLogicBase> PlayerCharacters;
        public List<CharacterLogicBase> MonsterCharacters;
        // 回合
        public List<CharacterLogicBase> CharacterRounds;
        public LinkedList<EffectBase> CurrentEffects;

        
        public BattleModel()
        {
            
        }
        

    }
}