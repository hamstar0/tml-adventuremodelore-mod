using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	public partial class ProgressionLogic {
		public static string InvestigateDungeonTitle => "Investigate Dungeon";

		internal static Objective InvestigateDungeon() {
			return new FlatObjective(
				title: ProgressionLogic.InvestigateDungeonTitle,
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
			if( ObjectivesAPI.IsFinishedObjective( ProgressionLogic.InvestigateDungeonTitle ) ) {
				ProgressionLogic.Run00_Guide_Actions_ObjectiveFinished();
			} else {
				ProgressionLogic.Run00_Guide_Actions_ObjectiveUnfinished();
			}

			return false;
		}

		////

		private static void Run00_Guide_Actions_ObjectiveUnfinished() {
			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.Guide );
			bool conveyance = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.Guide, (string msg, out bool alert) => {
				// Only show NPC alert icon
				alert = conveyance;
				if( alert && string.IsNullOrEmpty(msg) ) {
					return msg;
				}

				if( conveyance ) {
					ObjectivesAPI.AddObjective(
						objective: ProgressionLogic.InvestigateDungeon(),
						order: -1,
						alertPlayer: true,
						out string _
					);

					conveyance = false;
					return "Before the attack, reports came in of a large brick structure on the island a bit inland."
						+" Perhaps we should check it out?";
				} else {
					NPCChat.SetPriorityChat( NPCID.Guide, func );
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );
		}

		private static void Run00_Guide_Actions_ObjectiveFinished() {
			ObjectivesAPI.AddObjective(
				objective: ProgressionLogic.InvestigateDungeon(),
				order: -1,
				alertPlayer: true,
				out string _
			);
		}
	}
}
