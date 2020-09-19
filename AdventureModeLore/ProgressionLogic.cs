using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	class ProgressionLogic {
		public static void Run() {
			ProgressionLogic.Run00_Guide();
			ProgressionLogic.Run01_OldMan();
			ProgressionLogic.Run02_Merchant();
			ProgressionLogic.Run03_200hp();
			ProgressionLogic.Run04_Dryad();
			ProgressionLogic.Run05_Goblin();
			ProgressionLogic.Run06_WitchDoctor();
		}


		////////////////

		internal static void Run00_Guide() {
			/**** Conditions: ****/

			Objective objInvesDung = ObjectivesAPI.GetObjective( ObjectiveDefinitions.InvestigateDungeonTitle );
			if( objInvesDung != null ) {
				return;
			}

			/**** Actions: ****/

			// 00 - Investigate Dungeon
			ObjectivesAPI.AddObjective(
				objective: ObjectiveDefinitions.InvestigateDungeon(),
				order: -1,
				alertPlayer: true,
				out string _
			);
		}
		
		////

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
