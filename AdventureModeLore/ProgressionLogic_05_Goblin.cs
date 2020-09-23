using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		private static void Run05_Goblin() {
			/**** Conditions: ****/

			/**** Actions: ****/

			// 05a - Find mechanic
			Objective objFindMechanicBoss = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindMechanicTitle );
			if( objFindMechanicBoss?.IsComplete != true ) {
				if( objFindMechanicBoss == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindMechanic(), 0, true, out _ );
				}
			}
			
			// 05b - Find Witch Doctor
			Objective objFindWitchDoctor = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindWitchDoctorTitle );
			if( objFindWitchDoctor?.IsComplete != true ) {
				if( objFindWitchDoctor == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindWitchDoctor(), 0, true, out _ );
				}
			}
		}
	}
}
