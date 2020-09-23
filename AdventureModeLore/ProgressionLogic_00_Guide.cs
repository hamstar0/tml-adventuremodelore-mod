using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
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

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.Guide );
			bool conveyAlert = true;

			//

			NPCChat.SetPriorityChat( NPCID.Guide, (string msg, out bool alert ) => {
				alert = conveyAlert;

				if( conveyAlert ) {
					conveyAlert = false;
					return "Before the attack, reports came in of a large brick structure on the island a bit inland. Perhaps we should check it out?";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );
		}
	}
}
