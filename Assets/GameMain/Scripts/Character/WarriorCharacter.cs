// using System.Collections.Generic;
// using GameMain.Battle;
// using GameMain.Scripts.Card;
// using UnityGameFramework.Runtime;
//
// namespace GameMain.Scripts.Character
// {
//     public class WarriorCharacter : PlayerCharacterLogic
//     {
//         public override List<ICard> AllCard { get; set; }
//         public override List<ICard> HandCard { get; set; }
//         public override List<ICard> RemainCard { get; set; }
//         public override List<ICard> DestroyCard { get; set; }
//         public override List<ICard> DiscardCard { get; set; }
//         public DataTableComponent DataTableComponent { get; set; }
//
//         public override void GetAllCard()
//         {
//             var dataTable = DataTableComponent.GetDataTable<CardTable>();
//             var cards = new List<CardTable>();
//             dataTable.GetDataRows((data) => true, cards);
//             for (int i = 0; i < 5; i++)
//             {
//                 AllCard.Add(new AttackCard());
//             }
//
//             for (int i = 0; i < 5; i++)
//             {
//                 AllCard.Add(new DefenceCard());
//             }
//         }
//
//         public override void AddCard(ICard card)
//         {
//             AllCard.Add(card);
//         }
//
//         public override void OnInit()
//         {
//             DataTableComponent = GameEntry.GetComponent<DataTableComponent>();
//             AllCard = new List<ICard>();
//             HandCard = new List<ICard>();
//             RemainCard = new List<ICard>();
//             DestroyCard = new List<ICard>();
//             DiscardCard = new List<ICard>();
//             GetAllCard();
//         }
//     }
// }