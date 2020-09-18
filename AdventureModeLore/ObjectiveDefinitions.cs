using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Objectives.Definitions;


namespace AdventureModeLore {
	class ObjectiveDefinitions {
		public static string InvestigateDungeonTitle => "Investigate Dungeon";

		public static Objective InvestigateDungeon() {
			return new FlatObjective(
				title: ObjectiveDefinitions.InvestigateDungeonTitle,
				description: "There appears to be a large, ominous structure with a suspicious old man wandering around it's entrance. Recommend an investigation.",
				condition: ( obj ) => {
					return Main.player.Any( plr => {
						if( plr?.active != true ) {
							return false;
						}

						NPC oldMan = Main.npc.FirstOrDefault( n => n.type == NPCID.OldMan );
						if( oldMan?.active != true ) {
							return false;
						}

						return ( plr.position - oldMan.position ).LengthSquared() < ( 256f * 256f );
					} );
				}
			);
		}

		////

		public static string FindLighthouseTitle => "Find Celestial Lighthouse";

		public static Objective FindLighthouse() {

		}

		////

		public static string BuildHousingTitle => "Build A House";

		public static Objective BuildHousing() {

		}

		////

		public static string FindOrbTitle => "Find an Orb";

		public static Objective FindOrb() {

		}

		////

		public static string FindGoblinTitle => "Find Goblin Tinkerer";

		public static Objective FindGoblin() {

		}

		////

		public static string KillCorruptionBossTitle => "Defeat "+(WorldGen.crimson ? "Brain of Cthulhu" : "Eater of Worlds");

		public static Objective KillCorruptionBoss() {

		}

		////

		public static string ReachUnderworldTitle => "Reach Underworld";

		public static Objective ReachUnderworld() {

		}

		////

		public static string FindMechanicTitle => "Find Nechanic";

		public static Objective FindMechanic() {

		}

		////

		public static string FindWitchDoctorTitle => "Find Witch Doctor";

		public static Objective FindWitchDoctor() {

		}

		////

		public static string SummonWoFTitle => "Melt Guide Voodoo Doll in Underworld";

		public static Objective SummonWoF() {

		}
	}
}
