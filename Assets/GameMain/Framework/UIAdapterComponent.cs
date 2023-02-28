using GameFramework.Event;
using GameFramework.Resource;
using GameMain.Framework;
using GameMain.Framework.UI;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace GameMain.Framework
{

    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/UIAdapter")]
    public class UIAdapterComponent : GameFrameworkComponent
    {
        ResourceComponent _resourceComponent;
        DataTableComponent _dataTableComponent;
        EventComponent _eventComponent;
        public UITemplateAdapterManager<CardUITemplate, CardUITemplateData> CardAdapterManager;
        
        public bool IsInit()
        {
            return CardAdapterManager != null;
        }

        public void Init()
        {
            _resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            _dataTableComponent = GameEntry.GetComponent<DataTableComponent>();
            _eventComponent = GameEntry.GetComponent<EventComponent>();
            var table = _dataTableComponent.GetDataTable<UITemplateTable>();
            var datas = table.GetDataRows(d => true);
            var callback = new LoadAssetCallbacks(TemplateLoadFinish);
            foreach (var data in datas)
            {
                _resourceComponent.LoadAsset(data.Path, callback, data);
            }
        }

        public void TemplateLoadFinish(string assetName, object asset, float duration, object userData)
        {
            if (userData != null)
            {
                UITemplateTable data = (UITemplateTable)userData;
                var parent = new GameObject(data.Name.ToString());
                parent.transform.SetParent(transform);
                switch (data.Name)
                {
                    case UITemplateNameAlais.CardUI:
                        CardAdapterManager = new UITemplateAdapterManager<CardUITemplate, CardUITemplateData>(asset as GameObject,parent.transform);
                        break;
                }
            }

            
        }
    }
    public class UITemplateLoadSuccessArgs:GameEventArgs
    {
        public static readonly int EventId = typeof(UITemplateLoadSuccessArgs).GetHashCode();
        public override int Id => EventId;
        public override void Clear()
        {
            
        }
    }

}