using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace GameMain.Battle
{
    
    public class BattleEffectConsumerEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(BattleEffectConsumerEventArgs).GetHashCode();
        public object UserData { get; set; }
        public BattleEffectConsumerEventArgs(object userData)
        {
            UserData = userData;
        }
        public override void Clear()
        {
            UserData = null;
        }
        
        public override int Id =>EventId;
    }
    public abstract class Effect
    {
        private CharacterLogicBase _src;
        private CharacterLogicBase _target;
        public void Excute(IBattleManager battleManager)
        {
            battleManager.EventManager.Fire(this,new BattleEffectConsumerEventArgs(this));
        }
        
        public Effect(CharacterLogicBase src, CharacterLogicBase target)
        {
            _src = src;
            _target = target;
        }
    }
}