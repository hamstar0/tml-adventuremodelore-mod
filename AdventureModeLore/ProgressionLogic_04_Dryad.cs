using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string KillCorruptionBossTitle => "Defeat The "+(WorldGen.crimson?"Crimson":"Corruption")+"'s Guardian";

		public static Objective KillCorruptionBoss() {
			return new FlatObjective(
				title: ProgressionLogic.KillCorruptionBossTitle,
				description: "There's evil growing in the "+(WorldGen.crimson?"crimson":"corruption")+". It will need to be stopped, or else the plague"
					+ "\n" + "will spread.",
				condition: ( obj ) => {
					return NPC.killCount[NPCBannerHelpers.GetBannerItemTypeOfNpcType( NPCID.EaterofWorldsHead )] > 0
						|| NPC.killCount[NPCBannerHelpers.GetBannerItemTypeOfNpcType( NPCID.BrainofCthulhu )] > 0;
				}
			);
		}

		////

		public static string ReachUnderworldTitle => "Reach Underworld";

		public static Objective ReachUnderworld() {
			return new FlatObjective(
				title: ProgressionLogic.ReachUnderworldTitle,
				description: "It would seem the source of the plague is deep underground. You must find it.",
				condition: ( obj ) => {
					return Main.LocalPlayer.position.Y >= ( WorldHelpers.UnderworldLayerTopTileY * 16 );
				}
			);
		}


		////////////////

		private static bool Run04_Dryad() {
			/***********************/
			/**** Conditions:	****/
			/***********************/

			if( !NPC.AnyNPCs(NPCID.Dryad) ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.Dryad );
			bool conveyance1 = true;
			bool conveyance2 = true;

			// Dialogues
			NPCChat.SetPriorityChat( NPCID.Dryad, ( string msg, out bool alert ) => {
				alert = conveyance1 || conveyance2;
				if( alert && string.IsNullOrEmpty(msg) ) {
					return msg;
				}

				if( conveyance1 ) {
					// 04a - Kill EoW/BoC
					Objective objKillCorrBoss = ObjectivesAPI.GetObjective( ProgressionLogic.KillCorruptionBossTitle );
					if( objKillCorrBoss?.IsComplete != true ) {
						if( objKillCorrBoss == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.KillCorruptionBoss(), 0, true, out _ );
						}
					}

					conveyance1 = false;
					return "I see you are here to stop the undeath plague. Might I suggest you first start with the "
						+(WorldGen.crimson?"crimson":"corruption")+" areas that have begun appearing in this land."
						+" If these aren't stopped soon, evil essence will spread far and wide, and the plague along with it.";
				} else if( conveyance2 ) {
					// 04b - Reach underworld
					Objective objReachUnderworld = ObjectivesAPI.GetObjective( ProgressionLogic.ReachUnderworldTitle );
					if( objReachUnderworld?.IsComplete != true ) {
						if( objReachUnderworld == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.ReachUnderworld(), 0, true, out _ );
						}
					}

					conveyance2 = false;
					return "There are many contributing factors, but the main source of your so-called \"plague\" is from"
						+" the furthest depths of the world itself. It will an endeavor just to make it there. May the"
						+" blessings of nature be with you!";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
