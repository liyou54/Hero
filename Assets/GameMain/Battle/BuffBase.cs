namespace GameMain.Battle
{
    public enum BuffCheckType
    {
        AttackTime, //进攻时触发判定条件
        DamageType, // 受伤时触发判定条件
        BeforeAction, // 行动轮开始时触发判定条件
        BeforeEffect, // 执行影响前触发判定条件
        AfterEffect, // 执行影响后触发判定条件
        AfterAction, // 行动论结束后判定
        Immediately // 立即产生影响
    }

    public enum BuffType
    {
        Debuff,
        Buff,
        Attack,
        Defence
    }

    public abstract class BuffBase
    {
        public abstract int Value { get; }
        public abstract BuffCheckType BuffCheckType { get; }
        public abstract BuffType BuffType { get; }
        public abstract bool CheckReleaseConditional();

        public bool CheckBufferExcuteCondition()
        {
            return true;
        }
    }
    
    
}