using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using GameMain.Scripts.Procedure;
using GameMain.Scripts.Tools;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class ProcedureLaunch : CustomProcedure
{




    protected override  void OnPreLoadStart(ProcedureOwner procedureOwner)
    {
        base.OnPreLoadStart(procedureOwner);
        PreLoadTable(typeof(CardTable),this);
        PreLoadTable(typeof(SceneTable),this);
        PreLoadTable(typeof(MapTable),this);
        PreLoadTable(typeof(UITable),this);
        PreLoadTable(typeof(EntityTable),this);
    }
    

    protected override void OnUpdateAfterLoad(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdateAfterLoad(procedureOwner, elapseSeconds, realElapseSeconds);
        procedureOwner.SetData<VarSceneId>(ProcedureChangeScene.EntrySceneID, new VarSceneId(SceneNameAlais.MainMenuScene));
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }





}