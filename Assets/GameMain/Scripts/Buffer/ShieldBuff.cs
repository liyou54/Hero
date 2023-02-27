using GameMain.Battle;

namespace GameMain.Scripts.Buffer
{
    public class ShieldBuff:BuffBase
    {
        private int value;
        public override int Value => value;
        public override BuffCheckType BuffCheckType => BuffCheckType.Immediately;
        public override BuffType BuffType => BuffType.Defence;

        public override bool CheckReleaseConditional()
        {
            return true;
        }

        public ShieldBuff(int value)
        {
            this.value = value;
        }
        
    }
}