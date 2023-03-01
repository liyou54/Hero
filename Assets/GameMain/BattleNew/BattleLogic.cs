using System;
using System.Collections.Generic;

namespace GameMain.BattleNew
{
    public class BattleLogic
    {
        public BattleModel Model;

        public BattleLogic(BattleModel model)
        {
            Model = model;
        }

        public void GenBattleModel()
        {
            Model.CharacterRounds = new List<CharacterLogicBase>();
            Model.CharacterRounds.AddRange(Model.PlayerCharacters);
            Model.CharacterRounds.AddRange(Model.MonsterCharacters);
        }

        public void OnStartBattle()
        {
        }

        public void OnUpdateBattle()
        {
            while (Model.CurrentEffects.Count > 0)
            {
                var effect = Model.CurrentEffects.First.Value;
                effect.EffectExcute();
            }
        }

        public void FinishCharacterRound()
        {
        }

        public void OnEndBattle()
        {
        }
    }
}