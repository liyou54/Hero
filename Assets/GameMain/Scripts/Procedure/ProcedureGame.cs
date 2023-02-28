using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameMain.Battle;
using GameMain.Scripts.Character;
using GameMain.Scripts.Procedure;
using GameMain.Scripts.Tools;
using UnityEngine;
using UnityGameFramework.Runtime;

public class ProcedureGame : CustomProcedure
{
    protected BattleComponent _battleComponent;

    protected override void OnPreLoadFinish(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnPreLoadFinish(procedureOwner);
        var player = new WarriorCharacter();
        player.OnInit();
        var monster = new MonsterCharacter();
        monster.OnInit();
        var players = new List<CharacterLogicBase>() { player };
        var monsters = new List<CharacterLogicBase>() { monster };
        _battleComponent.StartBattle(players, monsters );
        _uiComponent.OpenUIForm(UINameAlais.MainGame);
    }

    protected override void OnPreLoadStart(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnPreLoadStart(procedureOwner);
        var map = _dataTableComponent.GetDataTable<MapTable>();
        var data = map.GetDataRow((int)MapNameAlais.Default);
        PreLoadAsset(data.Path, this, OnLoadMapData);
    }

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        _battleComponent = GameEntry.GetComponent<BattleComponent>();
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
    }


    private void OnLoadMapData(string assetName, object asset, float duration, object userData)
    {
        var textAsst = asset as TextAsset;
        //解析二进制到vector3
        var bytes = textAsst.bytes;
        var cellCount = bytes.Length / 12;
        var vertices = new Vector3[cellCount];

        for (int i = 0; i < cellCount; i++)
        {
            var x = System.BitConverter.ToInt32(bytes, i * 12);
            var y = System.BitConverter.ToInt32(bytes, i * 12 + 4);
            var z = System.BitConverter.ToInt32(bytes, i * 12 + 8);
            vertices[i] = new Vector3(x, y, z);

            CubeCoordinate cube = CubeCoordinate.OffsetToCubeCoordinate(x, y);
            Vector3 pos = CubeCoordinate.GetHexagonCenter(cube, 1);
            var entityData = _dataTableComponent.GetDataTable<EntityTable>();
            var data = entityData.GetDataRow((int)EntityNameAlais.TileCell);
            _entityComponent.GetEntities(data.Path);
            // _entityComponent.ShowEntityEx<TileCellLogic>(data.Path, "TileMap");
        }


    }
}