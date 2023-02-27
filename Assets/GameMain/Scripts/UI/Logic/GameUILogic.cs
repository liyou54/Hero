using System;
using UnityEngine;
using GameMain.Framework.UI;
using UnityGameFramework.Runtime;

namespace GameMain.Framework
{
    public class GameUILogic : UILogicBase
    {
        [SerializeField] private  GameUIView view;
        private BattleComponent _battleComponent;

        private void InstanceCard()
        {
            foreach (var player in _battleComponent.Players)
            {
               var card =  Instantiate(view.CardTemp, view.MyCardGroup.transform, true);
            }
            foreach (var monster in _battleComponent.Monsters)
            {
                var card =  Instantiate(view.CardTemp, view.MonsterCard.transform, true);
            }
            
        }
        
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            _battleComponent = GameEntry.GetComponent<BattleComponent>();
            InstanceCard();
        }
    }
}