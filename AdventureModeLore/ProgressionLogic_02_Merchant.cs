using System;
using Terraria;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		private static void Run02_Merchant() {
			/**** Conditions: ****/

			Objective objFindMerch = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindMerchantTitle );
			if( objFindMerch?.IsComplete != true ) {
				return;
			}

			/**** Actions: ****/

			Objective objReachJungle = ObjectivesAPI.GetObjective( ObjectiveDefinitions.ReachJungleTitle );
			Objective objBuildHouse = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindMerchantTitle );
			
			// 02 - Find an Orb
			Objective objFindOrb = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindOrbTitle );
			if( objFindOrb?.IsComplete != true ) {
				if( objFindOrb == null ) {
					if( objReachJungle?.IsComplete == true && objBuildHouse?.IsComplete == true ) {
						ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindOrb(), 0, true, out _ );
					}
				}
			}
		}
	}
}
