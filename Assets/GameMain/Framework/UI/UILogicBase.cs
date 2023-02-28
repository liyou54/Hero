using System;
using System.Collections.Generic;
using GameFramework.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain.Framework.UI
{
    public class UILogicBase : UIFormLogic
    {
        private class EventCache
        {
           public int eventId;
           public EventHandler<GameEventArgs> handler;
        }
        
        
        
        protected EventComponent eventComponent;
        private List<EventCache> eventCatches = new List<EventCache>();

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            eventComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            BindEvent();
        }
        
        protected void BindSingleEvent(int eventId, EventHandler<GameEventArgs> handler)
        {
            eventComponent.Subscribe(eventId, handler);
            eventCatches.Add(new EventCache() {eventId = eventId, handler = handler});
        }

        protected virtual void BindEvent()
        {
        }

        private  void UnBindEvent()
        {
            foreach (var e in eventCatches)
            {
                eventComponent.Unsubscribe(e.eventId,e.handler);
            }
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            UnBindEvent();
        }
    }
}