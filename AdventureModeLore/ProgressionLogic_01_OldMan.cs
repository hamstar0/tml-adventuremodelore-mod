using System;
using Terraria;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		private static void Run01_OldMan() {
			/**** Conditions: ****/

			Objective objInvesDung = ObjectivesAPI.GetObjective( ObjectiveDefinitions.InvestigateDungeonTitle );
			if( objInvesDung?.IsComplete != true ) {
				return;
			}

			/**** Actions: ****/

			// 01a - Reach Jungle
			Objective objReachJungle = ObjectivesAPI.GetObjective( ObjectiveDefinitions.ReachJungleTitle );
			bool reachJungle = objReachJungle?.IsComplete == true;
			if( !reachJungle ) {
				if( objReachJungle == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.ReachJungle(), 0, true, out _ );
				}
			}
			
			// 01b - Build house
			Objective objFindMerch = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindMerchantTitle );
			bool buildHouseDone = objFindMerch?.IsComplete == true;
			if( !buildHouseDone ) {
				if( objFindMerch == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindMerchant(), 0, true, out _ );
				}
			}
		}
	}
}
