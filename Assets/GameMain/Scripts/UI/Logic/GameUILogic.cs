using System;
using System.Collections.Generic;
using UnityEngine;
using GameMain.Framework.UI;
using UnityGameFramework.Runtime;

namespace GameMain.Framework
{
    public class GameUILogic : UILogicBase
    {
        [SerializeField] private  GameUIView view;
        private BattleComponent _battleComponent;
        private UITemplateAdapterManager<CardUITemplate,CardUITemplateData> _monsterCardAdapterManager;
        UIAdapterComponent _uiAdapterComponent;
        private void InstanceCard()
        {
          var adapter =  _uiAdapterComponent.CardAdapterManager.TryGetAdapterData(view.MyCardGroup.transform, view.CardTemp.gameObject);
          adapter.SetData(new List<CardUITemplateData>(){new CardUITemplateData()});
        }
        
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            _uiAdapterComponent = GameEntry.GetComponent<UIAdapterComponent>();
            _battleComponent = GameEntry.GetComponent<BattleComponent>();
            InstanceCard();
        }
    }
}