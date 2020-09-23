using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		private static void Run06_WitchDoctor() {
			/**** Conditions: ****/

			if( !NPC.AnyNPCs( NPCID.WitchDoctor ) ) {
				return;
			}

			/**** Actions: ****/

			// 06 - Summon WoF
			Objective objSummonWoF = ObjectivesAPI.GetObjective( ObjectiveDefinitions.SummonWoFTitle );
			if( objSummonWoF?.IsComplete != true ) {
				if( objSummonWoF == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.SummonWoF(), 0, true, out _ );
				}
			}
		}
	}
}
