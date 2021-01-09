using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public static string KillCorruptionBossTitle => "Defeat The "+(WorldGen.crimson?"Crimson":"Corruption")+"'s Guardian";

		internal static Objective KillCorruptionBoss() {
			return new FlatObjective(
				title: LoreEvents.KillCorruptionBossTitle,
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

		internal static Objective ReachUnderworld() {
			return new FlatObjective(
				title: LoreEvents.ReachUnderworldTitle,
				description: "It would seem the source of the plague is deep underground. You must find it.",
				condition: ( obj ) => {
					return Main.LocalPlayer.position.Y >= ( WorldHelpers.UnderworldLayerTopTileY * 16 );
				}
			);
		}


		////////////////

		private static bool Run04_Dryad() {
			Objective objKillCorrBoss = ObjectivesAPI.GetObjective( LoreEvents.KillCorruptionBossTitle );
			Objective objReachUnderworld = ObjectivesAPI.GetObjective( LoreEvents.ReachUnderworldTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( !NPC.AnyNPCs(NPCID.Dryad) ) {
				return true;
			}

			// Already done?
			bool isCorrFinished = ObjectivesAPI.IsFinishedObjective( LoreEvents.KillCorruptionBossTitle );
			bool isUnderFinished = ObjectivesAPI.IsFinishedObjective( LoreEvents.ReachUnderworldTitle );
			if( isCorrFinished || isUnderFinished ) {
				if( objKillCorrBoss == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( LoreEvents.KillCorruptionBoss(), 0, true, out _ );
				}
				if( objReachUnderworld == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( LoreEvents.ReachUnderworld(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( NPCID.Dryad );
			int conveyance = 0;

			// Dialogues
			DialogueEditor.SetDynamicDialogueHandler( NPCID.Dryad, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					switch( conveyance++ ) {
					case 0:
						// 04a - Kill EoW/BoC
						if( objKillCorrBoss == null ) {
							ObjectivesAPI.AddObjective( LoreEvents.KillCorruptionBoss(), 0, true, out _ );
						}

						return "I see you are here to stop the undeath plague. Might I suggest you first start with the "
							+(WorldGen.crimson?"crimson":"corruption")+" areas that have begun appearing in this land."
							+" If these aren't stopped soon, evil essence will spread far and wide, and the plague along with it.";
					case 1:
						// 04b - Reach underworld
						if( objReachUnderworld == null ) {
							ObjectivesAPI.AddObjective( LoreEvents.ReachUnderworld(), 0, true, out _ );
						}

						if( oldHandler != null ) {
							DialogueEditor.SetDynamicDialogueHandler( NPCID.Dryad, oldHandler );
						} else {
							DialogueEditor.RemoveDynamicDialogueHandler( NPCID.Dryad );
						}

						return "There are many contributing factors, but the main source of your so-called \"plague\" is from"
							+" the furthest depths of the world itself. It will an endeavor just to make it there. May the"
							+" blessings of nature be with you!";
					}

					return msg;
				},
				isShowingAlert: () => true
			) );

			return false;
		}
	}
}
