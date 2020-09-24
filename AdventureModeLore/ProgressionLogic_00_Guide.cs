using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string InvestigateDungeonTitle => "Investigate Dungeon";

		public static Objective InvestigateDungeon() {
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
			/***********************/
			/**** Conditions:	****/
			/***********************/

			Objective objInvesDung = ObjectivesAPI.GetObjective( ProgressionLogic.InvestigateDungeonTitle );
			if( objInvesDung != null ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.Guide );
			bool conveyAlert = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.Guide, (string msg, out bool alert ) => {
				alert = conveyAlert;

				if( conveyAlert ) {
					// 00 Objective: Investigate Dungeon
					ObjectivesAPI.AddObjective(
						objective: ProgressionLogic.InvestigateDungeon(),
						order: -1,
						alertPlayer: true,
						out string _
					);

					conveyAlert = false;
					return "Before the attack, reports came in of a large brick structure on the island a bit inland."
						+" Perhaps we should check it out?";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
