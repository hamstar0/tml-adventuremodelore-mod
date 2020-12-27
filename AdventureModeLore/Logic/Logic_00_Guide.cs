using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Logic {
	public partial class AMLLogic : ILoadable {
		public static string InvestigateDungeonTitle => "Investigate Dungeon";

		internal static Objective InvestigateDungeon() {
			return new FlatObjective(
				title: AMLLogic.InvestigateDungeonTitle,
				description: "There appears to be a large, ominous structure with a suspicious old man"
					+ "\n" + "wandering around it's entrance. Recommend an investigation.",
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


		////////////////

		internal static bool Run00_Guide() {
			if( ObjectivesAPI.IsFinishedObjective( AMLLogic.InvestigateDungeonTitle ) ) {
				AMLLogic.Run00_Guide_Actions_ObjectiveFinished();
			} else {
				AMLLogic.Run00_Guide_Actions_ObjectiveUnfinished();
			}

			return false;
		}

		////

		private static void Run00_Guide_Actions_ObjectiveUnfinished() {
			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( NPCID.Guide );
			int conveyances = 0;

			// Dialogue
			DialogueEditor.SetDynamicDialogueHandler( NPCID.Guide, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					switch( conveyances++ ) {
					case 0:
						ObjectivesAPI.AddObjective(
							objective: AMLLogic.InvestigateDungeon(),
							order: -1,
							alertPlayer: true,
							out string _
						);

						return "Before the attack, reports came in of a large brick structure on the island a bit inland."
							+ " Perhaps we should check it out?";
					case 1:
						msg = "If you're having trouble getting to a place, use your ropes, platforms, framing planks, and "
							+"track deployment kits. There should be some in the barrel on the raft. If you need more, some "
							+"of these can be crafted or bought, if you have the needed materials or money.";
						break;
					}

					if( oldHandler != null ) {
						DialogueEditor.SetDynamicDialogueHandler( NPCID.Guide, oldHandler );
					} else {
						DialogueEditor.RemoveDynamicDialogueHandler( NPCID.Guide );
					}

					return msg;
				},
				isShowingAlert: () => true
			) );
		}

		private static void Run00_Guide_Actions_ObjectiveFinished() {
			ObjectivesAPI.AddObjective(
				objective: AMLLogic.InvestigateDungeon(),
				order: -1,
				alertPlayer: true,
				out string _
			);
		}
	}
}
