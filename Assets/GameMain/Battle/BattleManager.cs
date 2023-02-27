using System.Collections.Generic;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace GameMain.Battle
{
    public class BattleManager : IBattleManager
    {
        private CharacterLogicBase _nowCharacterLogic;
        public List<CharacterLogicBase> Characters { get; set; }
        public IEventManager EventManager { get; set; }
        public Queue<CharacterLogicBase> m_CharacterQueue { get; set; }
        public EventComponent EventComponent { get; set; }
        public bool StartBattle { get; set; }
        CharacterLogicBase IBattleManager.MNowCharacterLogic
        {
            get => _nowCharacterLogic;
            set => _nowCharacterLogic = value;
        }

        public bool IsFinishCharacterAction()
        {
            return _nowCharacterLogic == null || _nowCharacterLogic.RealFinishAction;
        }


        // 添加角色 queueIndex = -1 不加本入回合队列
        public void AddCharacter(CharacterLogicBase characterLogic, int queueIndex = -1)
        {
            if (characterLogic == null)
            {
                return;
            }

            Characters.Add(characterLogic);
            if (queueIndex != -1)
            {
                m_CharacterQueue.Enqueue(characterLogic);
            }
        }


        public void GenCharacterRound()
        {
            m_CharacterQueue = new Queue<CharacterLogicBase>();
            foreach (var character in Characters)
            {
                m_CharacterQueue.Enqueue(character);
            }
        }

        void IBattleManager.OnUpdate()
        {
            if (StartBattle == false)
            {
                return;
            }
            if (IsFinishCharacterAction() && IsFinishAllCharacterRound() && IsFinishBattle())
            {
                // 战斗结束
                OnEndBattle();
            }
            else if (IsFinishCharacterAction() && IsFinishAllCharacterRound() )
            {
                // 回合结束
                OnEndRound();
                // 下一回合开始
                OnStartRound();
            }
            else if (IsFinishCharacterAction())
            {
                // 角色行动结束
                _nowCharacterLogic = m_CharacterQueue.Dequeue();
                _nowCharacterLogic.RealFinishAction = false;
            }
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].OnUpdate(this);
            }
        }
        
        

        public bool IsFinishBattle()
        {
            return Characters.Count == 0;
        }

        public bool IsFinishAllCharacterRound()
        {
            return m_CharacterQueue.Count == 0 && 
                   (_nowCharacterLogic != null && _nowCharacterLogic.RealFinishAction);
        }

        public void OnEndRound()
        {
        }

        public void OnEndBattle()
        {
            StartBattle = false;
            Characters = null;
        }



        public void OnStartBattle(List<CharacterLogicBase> characters)
        {
            StartBattle = true;
            Characters = characters;
            
        }

        public void OnStartRound()
        {
            GenCharacterRound();
        }
    }
}