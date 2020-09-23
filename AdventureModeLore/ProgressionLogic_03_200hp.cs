using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		private static void Run03_200hp() {
			/**** Conditions: ****/

			if( Main.LocalPlayer.statLifeMax < 200 ) {
				return;
			}

			/**** Actions: ****/

			// 03 - Find goblin tinkerer
			Objective objFindGoblin = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindGoblinTitle );
			if( objFindGoblin?.IsComplete != true ) {
				if( objFindGoblin == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindGoblin(), 0, true, out _ );
				}
			}
		}
	}
}
