using System;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using GameMain.Scripts.Procedure;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

class VarSceneId : Variable<SceneNameAlais>
{
    public VarSceneId(SceneNameAlais id)
    {
        Value = id;
    }
}

public class ReqChangeSceneEventArgs : GameEventArgs
{
    public static int EventId = typeof(ReqChangeSceneEventArgs).GetHashCode();
    public int SceneId;

    public ReqChangeSceneEventArgs(int sceneId)
    {
        SceneId = sceneId;
    }

    public override void Clear()
    {
    }

    public override int Id => EventId;
}

public class ProcedureChangeScene : CustomProcedure
{
    public static string EntrySceneID = "EntrySceneID";

    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
    }


    protected override void OnPreLoadStart(ProcedureOwner procedureOwner)
    {
        base.OnPreLoadStart(procedureOwner);
        var scene = procedureOwner.GetData<VarSceneId>(ProcedureChangeScene.EntrySceneID).Value;
        PreLoadScene(scene, this);
    }


    public void AddCameraStack()
    {
        var data = _sceneComponent.MainCamera.GetComponent<UniversalAdditionalCameraData>();
        var uiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        data.cameraStack.Add(uiCamera);
    }

    protected override void OnUpdateAfterLoad(ProcedureOwner procedureOwner, float elapseSeconds,
        float realElapseSeconds)
    {
        base.OnUpdateAfterLoad(procedureOwner, elapseSeconds, realElapseSeconds);
        AddCameraStack();
        var scene = procedureOwner.GetData<VarSceneId>(ProcedureChangeScene.EntrySceneID).Value;
        if (scene == SceneNameAlais.MainMenuScene)
        {
            ChangeState<ProcedureMenu>(procedureOwner);
        }
        else if (scene == SceneNameAlais.GameScene)
        {
            ChangeState<ProcedureGame>(procedureOwner);
        }
        else
        {
            Log.Error("场景类型错误");
        }
    }
}