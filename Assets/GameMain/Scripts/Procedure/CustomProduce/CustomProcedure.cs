using System;
using System.Collections.Generic;
using System.Resources;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain.Scripts.Procedure
{
    public partial class CustomProcedure : ProcedureBase
    {
        protected ResourceComponent resourceComponent;
        protected UIComponent uiComponent;
        protected DataTableComponent _dataTableComponent;
        protected EventComponent _eventComponent;
        protected EntityComponent _entityComponent;
        protected ProcedureComponent _procedureComponent;
        protected SettingComponent _settingComponent;
        protected SoundComponent _soundComponent;
        protected WebRequestComponent _webRequestComponent;
        protected NetworkComponent _networkComponent;
        protected UIComponent _uiComponent;
        protected ResourceComponent _resourceComponent;
        protected SceneComponent _sceneComponent;
        protected List<PreLoadData> _preLoadDataList = new List<PreLoadData>();
        protected ProcedureOwner procedureOwner;
        private bool IsPreLoadOk = false;

        /// <summary>
        /// 状态初始化时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            this.procedureOwner = procedureOwner;
            resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            uiComponent = GameEntry.GetComponent<UIComponent>();
            _dataTableComponent = GameEntry.GetComponent<DataTableComponent>();
            _eventComponent = GameEntry.GetComponent<EventComponent>();
            _entityComponent = GameEntry.GetComponent<EntityComponent>();
            _procedureComponent = GameEntry.GetComponent<ProcedureComponent>();
            _sceneComponent = GameEntry.GetComponent<SceneComponent>();
            _settingComponent = GameEntry.GetComponent<SettingComponent>();
            _soundComponent = GameEntry.GetComponent<SoundComponent>();
            _webRequestComponent = GameEntry.GetComponent<WebRequestComponent>();
            _networkComponent = GameEntry.GetComponent<NetworkComponent>();
            _uiComponent = GameEntry.GetComponent<UIComponent>();
            _resourceComponent = GameEntry.GetComponent<ResourceComponent>();
        }


        protected virtual void OnPreLoadFinish(ProcedureOwner procedureOwner)
        {
        }

        protected virtual void OnPreLoading(ProcedureOwner procedureOwner)
        {
            if (IsPreLoadOk)
            {
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }


        protected virtual void OnPreLoadStart(ProcedureOwner procedureOwner)
        {
            IsPreLoadOk = false;
            _preLoadDataList.Clear();
        }

        /// <summary>
        /// 进入状态时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            BindEvent(procedureOwner);
            OnPreLoadStart(procedureOwner);
        }

        private bool IsPreLoadFinish(ProcedureOwner procedureOwner)
        {
            if (IsPreLoadOk)
            {
                return true;
            }

            foreach (var preLoad in _preLoadDataList)
            {
                if (preLoad.Status != LoadResourceStatus.Success)
                {
                    return false;
                }
            }
            IsPreLoadOk = true;
            OnPreLoadFinish(procedureOwner);
            return true;
        }

        private void ReleaseLoadRes()
        {
            foreach (var preLoad in _preLoadDataList)
            {
                if (preLoad.IsReleaseOnExit)
                {
                    switch (preLoad.PreLoadType)
                    {
                        case PreLoadData.PreLoadTypeEnum.Asset:
                            _resourceComponent.UnloadAsset(preLoad.asset);
                            break;
                        case PreLoadData.PreLoadTypeEnum.Scene:
                            _sceneComponent.UnloadScene(preLoad.Path);
                            break;
                        default:
                            break;
                    }
                }
            }
            _preLoadDataList.Clear();
        }

        protected virtual void OnUpdateAfterLoad(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            
        }


        /// <summary>
        /// 状态轮询时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected sealed override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!IsPreLoadFinish(procedureOwner))
            {
                return;
            }
            OnUpdateAfterLoad(procedureOwner, elapseSeconds, realElapseSeconds);
        }


        /// <summary>
        /// 离开状态时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            UnBindEvent(procedureOwner);
            ReleaseLoadRes();
        }

        /// <summary>
        /// 状态销毁时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}