using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameMain.Battle;
using Unity.VisualScripting;
using UnityEngine;
using UnityGameFramework.Runtime;

[DisallowMultipleComponent]
[AddComponentMenu("Game Framework/Battle")]
public  class BattleComponent : GameFrameworkComponent
{
    public IBattleManager BattleManager;

    public List<CharacterLogicBase> Players;
    public List<CharacterLogicBase> Monsters;

    private TimerComponent m_TimerComponent;
    private void Start()
    {
        BattleManager = new BattleManager();
        BattleManager.EventManager = GameFrameworkEntry.GetModule<IEventManager>();
        m_TimerComponent = GameEntry.GetComponent<TimerComponent>();

    }
    
    public void StartBattle(List<CharacterLogicBase> players,List<CharacterLogicBase> monsters)
    {
        Players = players;
        Monsters = monsters;
        var chars = new List<CharacterLogicBase>();
        chars.AddRange(players);
        chars.AddRange(monsters);
        BattleManager.OnStartBattle(chars);
    }
    
    
}