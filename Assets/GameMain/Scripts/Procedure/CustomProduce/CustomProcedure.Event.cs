using System;
using GameFramework.Event;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain.Scripts.Procedure
{
    public  class  PreLoadData
    {
        public enum PreLoadTypeEnum
        {
            Asset,
            Scene,
            DataTable,
            UIForm,
        }

        public PreLoadTypeEnum PreLoadType;
        public object UserData;
        public LoadResourceStatus Status;
        public string Path;
        public bool IsReleaseOnExit;
        public object asset;
        public int Priority;
        public EventHandler<GameEventArgs> loadAssetSuccessEventHandler;
        public LoadAssetSuccessCallback loadAssetSuccessCallback;

        public PreLoadData(string path,PreLoadTypeEnum preLoadType, object userData, bool isReleaseOnExit = true,
            LoadAssetSuccessCallback loadAssetSuccessCallback = null,
            EventHandler<GameEventArgs> loadAssetSuccessEventHandler = null, int priority = 1)
        {
            this.Path = path;
            this.UserData = userData;
            this.PreLoadType = preLoadType;
            this.IsReleaseOnExit = isReleaseOnExit;
            this.loadAssetSuccessCallback = loadAssetSuccessCallback;
            this.loadAssetSuccessEventHandler = loadAssetSuccessEventHandler;
            this.Priority = priority;
            this.Status = LoadResourceStatus.NotReady;
        }

    }

    public partial  class CustomProcedure
    {
        protected virtual void BindEvent(ProcedureOwner procedureOwner)
        {
            _eventComponent.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnReadTableSuccess);
            _eventComponent.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            _eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        }

        protected virtual void UnBindEvent(ProcedureOwner procedureOwner)
        {
            _eventComponent.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnReadTableSuccess);
            _eventComponent.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            _eventComponent.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        }
        
        protected void PreLoadScene(SceneNameAlais alais, object userData, bool isReleaseOnExit = true,
            EventHandler<GameEventArgs> loadAssetSuccessEventHandler = null)
        {
            var sceneTable = _dataTableComponent.GetDataTable<SceneTable>().GetDataRow((int)alais);
            foreach (var preLoad in _preLoadDataList)
            {
                if (preLoad.Path == sceneTable.Path)
                {
                    return;
                }
            }

            var data = new PreLoadData(sceneTable.Path,PreLoadData.PreLoadTypeEnum.Scene ,userData, true, null, loadAssetSuccessEventHandler, 1);
            _preLoadDataList.Add(data);
            _sceneComponent.LoadScene(sceneTable.Path, data);
        }

        
        protected void PreLoadTable(Type tableType, object userData, bool isReleaseOnExit = true,
            EventHandler<GameEventArgs> loadAssetSuccessEventHandler = null)
        {
            var name = tableType.Name.Replace("Table", "");
            var path = "Assets/Table/" + name + ".csv";
            foreach (var preLoad in _preLoadDataList)
            {
                if (preLoad.Path == path)
                {
                    return;
                }
            }

            var preLoadData = new PreLoadData(path,PreLoadData.PreLoadTypeEnum.DataTable, userData, true, null, loadAssetSuccessEventHandler, 1);
            _preLoadDataList.Add(preLoadData);
            var table = _dataTableComponent.CreateDataTable(tableType);
            table.ReadData(path, preLoadData);
        }

        protected void PreOpenUI(UINameAlais alais, object userData, bool isReleaseOnExit = true,
            EventHandler<GameEventArgs> loadAssetSuccessEventHandler = null)
        {
            var uiTable = _dataTableComponent.GetDataTable<UITable>().GetDataRow((int)alais);
            foreach (var preLoad in _preLoadDataList)
            {
                if (preLoad.Path == uiTable.Path)
                {
                    return;
                }
            }

            var data = new PreLoadData(uiTable.Path,PreLoadData.PreLoadTypeEnum.UIForm, userData, true, null, loadAssetSuccessEventHandler, 1);
            _preLoadDataList.Add(data);
            _uiComponent.OpenUIForm(uiTable.Path, uiTable.Group.ToString(), data);
        }

        protected void PreLoadAsset(string assetName, object userData,
            LoadAssetSuccessCallback loadAssetSuccessCallback = null, bool isReleaseOnExit = true)

        {
            foreach (var preLoad in _preLoadDataList)
            {
                if (preLoad.Path == assetName)
                {
                    return;
                }
            }

            var data = new PreLoadData(assetName,PreLoadData.PreLoadTypeEnum.Asset, userData, true, loadAssetSuccessCallback, null, 1);
            _preLoadDataList.Add(data);
            _resourceComponent.LoadAsset(assetName, new LoadAssetCallbacks(LoadAssetSuccessCallback), data);
        }

        void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData)
        {
            if (userData != null)
            {
                PreLoadData data = (PreLoadData)userData;
                data.asset = asset;
                data?.loadAssetSuccessCallback?.Invoke(assetName, asset, duration, data.UserData);
                data.Status = LoadResourceStatus.Success;
            }
        }

        private void OnReadTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != null)
            {
                PreLoadData data = (PreLoadData)ne.UserData;
                data?.loadAssetSuccessEventHandler?.Invoke(sender, e);
                data.Status = LoadResourceStatus.Success;
            }
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != null)
            {
                PreLoadData data = (PreLoadData)ne.UserData;
                data?.loadAssetSuccessEventHandler?.Invoke(sender, e);
                data.Status = LoadResourceStatus.Success;
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != null)
            {
                PreLoadData data = (PreLoadData)ne.UserData;
                data?.loadAssetSuccessEventHandler?.Invoke(sender, e);
                data.Status = LoadResourceStatus.Success;
            }
        }
    }
}