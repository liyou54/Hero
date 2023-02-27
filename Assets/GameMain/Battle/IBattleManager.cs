using System.Collections.Generic;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace GameMain.Battle
{
    // 战斗 -> 回合 -> 角色 -> 行动 -> 伤害
    public interface IBattleManager
    {
        public List<CharacterLogicBase> Characters{ get;set;}
        
        public IEventManager EventManager{ get;set;}
        public Queue<CharacterLogicBase> m_CharacterQueue{ get;set;}
        protected CharacterLogicBase MNowCharacterLogic{ get;set;}
        public bool IsFinishCharacterAction();
        public void AddCharacter(CharacterLogicBase characterLogic, int queueIndex);
        public void GenCharacterRound();
        public bool IsFinishBattle();
        public bool IsFinishAllCharacterRound();
        public void OnEndRound();
        public void OnEndBattle();
        public void OnStartBattle( List<CharacterLogicBase> characters);
        public void OnStartRound();
        protected void OnUpdate();
    }
}