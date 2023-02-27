using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using GameMain.Framework;
using GameMain.Scripts.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class ProcedureMenu : CustomProcedure
{

    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
    }


    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
    }
    
    protected override void OnPreLoadStart(ProcedureOwner procedureOwner)
    {
        base.OnPreLoadStart(procedureOwner);
        PreOpenUI(UINameAlais.MainMenu, this);
    }

    protected override void BindEvent(ProcedureOwner procedureOwner)
    {
        base.BindEvent(procedureOwner);
        _eventComponent.Subscribe(ReqChangeSceneEventArgs.EventId, StartGame);
    }

    public void StartGame(object sender, GameEventArgs e)
    {
        ReqChangeSceneEventArgs ne = (ReqChangeSceneEventArgs)e;
        procedureOwner.SetData<VarSceneId>(ProcedureChangeScene.EntrySceneID,
            new VarSceneId(SceneNameAlais.GameScene));
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }

}