using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.World;
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

		public static string FindLighthouseTitle => "Find Jungle";

		public static Objective FindLighthouse() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindLighthouseTitle,
				description: "The old man says there's something suspicious in the jungle. Maybe take a look?.",
				condition: ( obj ) => {
					return Main.LocalPlayer.ZoneJungle;
				}
			);
		}

		////

		public static string FindMerchantTitle => "Find The Merchant";

		public static Objective FindMerchant() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindMerchantTitle,
				description: "Other inhabitants exist in this land, some less enslaved than others. Build a house for the merchant to settle in.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.Merchant );
				}
			);
		}

		////

		public static string FindOrbTitle => "Find an Orb";

		public static Objective FindOrb() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindOrbTitle,
				description: "It seems the land itself is enchanted. Sacred orbs appear to resonate with terrain. Maybe this will be of help?",
				condition: ( obj ) => {
					var orbsMod = ModLoader.GetMod( "Orbs" );

					return PlayerItemFinderHelpers.CountTotalOfEach(
						player: Main.LocalPlayer,
						itemTypes: new HashSet<int> {
							orbsMod.ItemType("RedOrbItem"),
							orbsMod.ItemType("BlueOrbItem"),
							orbsMod.ItemType("TealOrbItem"),
							orbsMod.ItemType("PurpleOrbItem"),
							orbsMod.ItemType("CyanOrbItem"),
							orbsMod.ItemType("GreenOrbItem"),
							orbsMod.ItemType("PinkOrbItem"),
							orbsMod.ItemType("YellowOrbItem"),
							orbsMod.ItemType("WhiteOrbItem")
						},
						includeBanks: true
					) > 0;
				}
			);
		}

		////

		public static string FindGoblinTitle => "Talk To A Goblin";

		public static Objective FindGoblin() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindGoblinTitle,
				description: "It would seem there are native(?) inhabitants in this land, if you'd call them that. Try to make contact.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.GoblinTinkerer );
				}
			);
		}

		////

		public static string KillCorruptionBossTitle => "Defeat "+(WorldGen.crimson ? "Brain of Cthulhu" : "Eater of Worlds");

		public static Objective KillCorruptionBoss() {
			return new FlatObjective(
				title: ObjectiveDefinitions.KillCorruptionBossTitle,
				description: "There's evil growing in the "+(WorldGen.crimson?"crimson":"corruption")+". It will need to be stopped, or else the plague will spread.",
				condition: ( obj ) => {
					return NPC.killCount[ NPCBannerHelpers.GetBannerItemTypeOfNpcType(NPCID.EaterofWorldsHead) ] > 0
						|| NPC.killCount[ NPCBannerHelpers.GetBannerItemTypeOfNpcType(NPCID.BrainofCthulhu) ] > 0;
				}
			);
		}

		////

		public static string ReachUnderworldTitle => "Reach Underworld";

		public static Objective ReachUnderworld() {
			return new FlatObjective(
				title: ObjectiveDefinitions.ReachUnderworldTitle,
				description: "It would seem the source of the plague is deep underground. You must find it.",
				condition: ( obj ) => {
					return Main.LocalPlayer.position.Y >= (WorldHelpers.UnderworldLayerTopTileY * 16);
				}
			);
		}

		////

		public static string FindMechanicTitle => "Find Nechanic";

		public static Objective FindMechanic() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindMechanicTitle,
				description: "Rumors exist of a plan to empower technology with the dungeon's spiritual energies. This could be disasterous. Liberate the engineer.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.Mechanic );
				}
			);
		}

		////

		public static string FindWitchDoctorTitle => "Find Witch Doctor";

		public static Objective FindWitchDoctor() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindWitchDoctorTitle,
				description: "A mysterious lizard-man sorcerer may know the plague's secret. Something powerful in the jungle has put him into hiding, though.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.WitchDoctor );
				}
			);
		}

		////

		public static string SummonWoFTitle => "Sacrifice Voodoo Doll";

		public static Objective SummonWoF() {
			return new FlatObjective(
				title: ObjectiveDefinitions.FindWitchDoctorTitle,
				description: "The witch doctor describes a ritual to destroy the spiritual energy confluence; the source of the plague. It involves a voodoo sacrifice of one of its makers near its source: The underworld.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.WallofFlesh );
				}
			);
		}
	}
}
