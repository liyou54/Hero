// using GameMain.Battle;
//
// namespace GameMain.Scripts.Buffer
// {
//     public class DamageBuff:BuffBase
//     {
//         private int value;
//         public override int Value => value;
//         public override BuffCheckType BuffCheckType => BuffCheckType.Immediately;
//         public override BuffType BuffType => BuffType.Attack;
//
//         public override bool CheckReleaseConditional()
//         {
//             return true;
//         }
//
//         public DamageBuff(int value)
//         {
//             this.value = value;
//         }
//         
//     }
// }