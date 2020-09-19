using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	class ProgressionLogic {
		public static void Run() {
			// 00 - Investigate Dungeon
			Objective objInvesDung = ObjectivesAPI.GetObjective( ObjectiveDefinitions.InvestigateDungeonTitle );
			if( objInvesDung?.IsComplete != true ) {
				return;
			}

			if( !ProgressionLogic.Run01() ) {
				return;
			}
			if( !ProgressionLogic.Run02() ) {
				return;
			}
			if( !ProgressionLogic.Run03() ) {
				return;
			}
			if( !ProgressionLogic.Run04() ) {
				return;
			}
			if( !ProgressionLogic.Run05() ) {
				return;
			}
			if( !ProgressionLogic.Run06() ) {
				return;
			}
		}


		////
		
		private static bool Run01() {
			// 01a - Reach Jungle
			Objective objReachJungle = ObjectivesAPI.GetObjective( ObjectiveDefinitions.ReachJungleTitle );
			bool reachJungle = objReachJungle?.IsComplete == true;
			if( !reachJungle ) {
				if( objReachJungle == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.ReachJungle(), 0, true, out _ );
				}
			}
			
			// 01b - Build house
			Objective objBuildHouse = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindMerchantTitle );
			bool buildHouseDone = objBuildHouse?.IsComplete == true;
			if( !buildHouseDone ) {
				if( objBuildHouse == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindMerchant(), 0, true, out _ );
				}
			}

			return buildHouseDone && reachJungle;
		}

		private static bool Run02() {
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

			return true;
		}

		private static bool Run03() {
			// 03 - Find goblin tinkerer
			Objective objFindGoblin = ObjectivesAPI.GetObjective( ObjectiveDefinitions.FindGoblinTitle );
			if( objFindGoblin?.IsComplete != true ) {
				if( objFindGoblin == null && Main.LocalPlayer.statLifeMax >= 200 ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.FindGoblin(), 0, true, out _ );
				}
			}
			
			return true;
		}

		private static bool Run04() {
			if( !NPC.AnyNPCs(NPCID.Dryad) ) {
				return false;
			}
			
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

			return true;
		}

		private static bool Run05() {
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

				return false;
			}

			return true;
		}

		private static bool Run06() {
			// 06 - Summon WoF
			Objective objSummonWoF = ObjectivesAPI.GetObjective( ObjectiveDefinitions.SummonWoFTitle );
			if( objSummonWoF?.IsComplete != true ) {
				if( objSummonWoF == null ) {
					ObjectivesAPI.AddObjective( ObjectiveDefinitions.SummonWoF(), 0, true, out _ );
				}
			}
			
			return true;
		}
	}
}
