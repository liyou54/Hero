using System.Collections.Generic;

namespace GameMain.Battle
{
    
    
    public abstract class CharacterLogicBase
    {
        public CubeCoordinate Coordinate { get; }

        public bool RealFinishAction
        {
            get
            {
                return ActionQueue. Count == 0 && EffectQueue.Count == 0 && FinishAction;
            }
            set
            {
                FinishAction = value;
            }
        }

        public bool FinishAction { get; set; }
        protected Queue<PlayAction> ActionQueue { get; set; }
        protected BuffBase BuffBase { get; set; }
        protected List<Effect> EffectQueue { get; set; }

        public void AddAction(PlayAction action)
        {
            ActionQueue.Enqueue(action);
        }
        
        public abstract  void OnInit();
        
        public void OnUpdate(IBattleManager battleManager)
        {
            if (ActionQueue.Count > 0)
            {
                var action = ActionQueue.Peek();
                action.OnUpdate();
                if (action.isFinish)
                {
                    ActionQueue.Dequeue();
                }
            }
            EffectQueue.ForEach(effect => effect.Excute(battleManager));
        }
    }
    
    public class PlayAction
    {
        public bool isFinish { get; set; }
        public void OnUpdate()
        {
            
        }
    }

    
}