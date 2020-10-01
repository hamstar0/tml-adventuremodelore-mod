﻿using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string FindGoblinTitle => "Talk To A Goblin";

		public static Objective FindGoblin() {
			return new FlatObjective(
				title: ProgressionLogic.FindGoblinTitle,
				description: "It would seem there are natives in this land, if you'd call them that. Try to"
					+ "\n"+"somehow open a line of communication with them.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.GoblinTinkerer );
				}
			);
		}


		////////////////

		private static bool Run03_200hp() {
			/***********************/
			/**** Conditions:	****/
			/***********************/

			if( Main.LocalPlayer.statLifeMax < 200 ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.Guide );
			bool conveyance = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.Guide, ( string msg, out bool alert ) => {
				alert = conveyance;
				if( alert && string.IsNullOrEmpty(msg) ) {
					return msg;
				}

				if( conveyance ) {
					// 03 - Find goblin tinkerer
					Objective objFindGoblin = ObjectivesAPI.GetObjective( ProgressionLogic.FindGoblinTitle );
					if( objFindGoblin?.IsComplete != true ) {
						if( objFindGoblin == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.FindGoblin(), 0, true, out _ );
						}
					}

					conveyance = false;
					return "I have to tell you something. There are natives on this island!"
						+" Not mere scattered survivors or profiteers, but a full blown army of goblins!"
						+" We must find a way to communicate with them directly."
						+" I fear our presence here might be taken the wrong way!";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
