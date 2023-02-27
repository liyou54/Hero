using System;
using GameFramework.Event;
using UnityEngine;
using GameMain.Framework.UI;
using UnityGameFramework.Runtime;

namespace GameMain.Framework
{

    
    public class MainMenuLogic : UILogicBase
    {
        
        ProcedureComponent procedureComponent;
        EventComponent eventComponent;
        UIComponent uiComponent;
        [SerializeField] private  MainMenuView view;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            procedureComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
            eventComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            uiComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            view.BtnStart.onClick.AddListener(StartGame);
            //
        }
        
        public void  StartGame()
        {
            uiComponent.CloseUIForm(this.UIForm);
            eventComponent.Fire(this, new ReqChangeSceneEventArgs(2));
        }
    }
}