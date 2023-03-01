using System.Collections.Generic;
using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain.Framework.UI
{
    public abstract class UITemplateData
    {
    }

    public class TemplaterAdaterData<TOutter, TInner>
        where TOutter : UITemplateBase<TInner> where TInner : UITemplateData
    {
        public List<TOutter> ObjectList;
        public Transform Parent;
        public GameObject Prefab;
        private UITemplateAdapterManager<TOutter, TInner> manager;
        public TemplaterAdaterData(UITemplateAdapterManager<TOutter, TInner> manager,Transform parent, GameObject prefab)
        {
            Parent = parent;
            Prefab = prefab;
            this.manager = manager;
            ObjectList = new List<TOutter>();
        }

        public void SetData( List<TInner> dataList)
        {
            for (int i = ObjectList.Count - 1; i >= 0; i--)
            {
                var release = ObjectList[i];
                ObjectList.Remove(ObjectList[i]);
                manager.RelaseTemplate(release);
            }

            foreach (var data in dataList)
            {
                var comp = manager.GetTemplate();
                comp.Data = data;
                comp.transform.parent = Parent.transform;
                comp.SetData(data);
                ObjectList.Add(comp);
            }
        }
    }

    public class UITemplateAdapterManager<TOutter, TInner> where TOutter : UITemplateBase<TInner>
        where TInner : UITemplateData
    {
        public Transform RelaseParent;
        public GameObject Prefab;
        private IObjectPool<UITemplateBaseObject<TOutter, TInner>> UITemplatePool;
        private List<TemplaterAdaterData<TOutter, TInner>> _adaterDatas = new List<TemplaterAdaterData<TOutter, TInner>>();
        public UITemplateAdapterManager(GameObject prefab, Transform parent)
        {
            RelaseParent = parent;
            Prefab = prefab;
            UITemplatePool = GameEntry.GetComponent<ObjectPoolComponent>()
                .CreateSingleSpawnObjectPool<UITemplateBaseObject<TOutter, TInner>>(
                    typeof(UITemplateBaseObject<TOutter, TInner>).Name, 5);
        }
        
        public TemplaterAdaterData<TOutter, TInner> TryGetAdapterData(Transform parent, GameObject prefab)
        {
            var res = new TemplaterAdaterData<TOutter, TInner>(this,parent, prefab);
            _adaterDatas.Add(res);
            return res;
        }

        public void RelaseTemplate(TOutter template)
        {
            UITemplatePool.Unspawn(template);
        }
        public TOutter GetTemplate()
        {
            UITemplateBaseObject<TOutter, TInner> obj = UITemplatePool.Spawn();
            TOutter comp = null;
            if (obj != null)
            {
                comp = (TOutter)obj.Target;
            }
            else
            {
                GameObject go = GameObject.Instantiate(Prefab);
                comp = go.GetComponent<TOutter>();
                Transform transform = comp.GetComponent<Transform>();
                transform.localScale = Vector3.one;
                UITemplatePool.Register(UITemplateBaseObject<TOutter, TInner>.Create(comp), true);
            }

            return comp;
        }
    }

    public class UITemplateBase<T> : MonoBehaviour where T : UITemplateData
    {
        public T Data { get; set; }

        public virtual void OnAwake()
        {
        }

        public virtual void SetData(T data)
        {
            Data = data;
        }

        public virtual void OnHide()
        {
            
        }
    }

    public class UITemplateBaseObject<TOutter, TInner> : ObjectBase where TOutter : UITemplateBase<TInner>
        where TInner : UITemplateData
    {
        public static UITemplateBaseObject<TOutter, TInner> Create(TOutter instance)
        {
            UITemplateBaseObject<TOutter, TInner> obj =
                ReferencePool.Acquire<UITemplateBaseObject<TOutter, TInner>>();
            obj.Initialize(instance);
            return obj;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
        }

        protected override void OnUnspawn()
        {
            base.OnUnspawn();
        }

        protected override void Release(bool isShutdown)
        {
        }
    }
}