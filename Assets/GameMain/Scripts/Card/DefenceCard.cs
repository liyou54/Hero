// using System.Collections.Generic;
// using GameMain.Battle;
// using GameMain.Scripts.Buffer;
//
// namespace GameMain.Scripts.Card
// {
//     public class DefenceCard:ICard
//     {
//         private int cost =1;
//         private List<BuffBase> _buff;
//         
//         public int Cost => cost;
//         public List<BuffBase> Buff => _buff;
//         public void UseCard(CharacterLogicBase src, CharacterLogicBase target, CubeCoordinate coordinate)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public DefenceCard()
//         {
//             _buff = new List<BuffBase>();
//             _buff.Add(new DamageBuff(2));
//         }
//     }
// }