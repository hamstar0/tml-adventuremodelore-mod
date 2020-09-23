using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		private static void Run04_Dryad() {
			/**** Conditions: ****/

			if( !NPC.AnyNPCs(NPCID.Dryad) ) {
				return;
			}

			/**** Actions: ****/

			// 04a - Kill EoW/BoC
			Objective objKillCorrBoss = ObjectivesAPI.GetObjective( ObjectiveDefinitions.KillCorruptionBossTitle );
			if( objKillCorrBoss?.IsComplete != true ) {
				if( objKillCorrBoss == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.KillCorruptionBoss(), 0, true, out _ );
				}
			}
			
			// 04b - Reach underworld
			Objective objReachUnderworld = ObjectivesAPI.GetObjective( ObjectiveDefinitions.ReachUnderworldTitle );
			if( objReachUnderworld?.IsComplete != true ) {
				if( objReachUnderworld == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.ReachUnderworld(), 0, true, out _ );
				}
			}
		}
	}
}
