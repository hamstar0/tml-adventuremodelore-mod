using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Logic {
	public partial class LoreLogic : ILoadable {
		public static string FindGoblinTitle => "Talk To A Goblin";

		internal static Objective FindGoblin() {
			return new FlatObjective(
				title: LoreLogic.FindGoblinTitle,
				description: "It would seem there are natives in this land, if you'd call them that. Try to"
					+ "\n"+"somehow open a line of communication with them.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.GoblinTinkerer );
				}
			);
		}


		////////////////

		private static bool Run03_200hp() {
			Objective objFindGoblin = ObjectivesAPI.GetObjective( LoreLogic.FindGoblinTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( Main.LocalPlayer.statLifeMax < 200 ) {
				return true;
			}

			// Already done?
			if( ObjectivesAPI.IsFinishedObjective(LoreLogic.FindGoblinTitle) ) {
				if( objFindGoblin == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( LoreLogic.FindGoblin(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( NPCID.Guide );
			bool conveyance = true;

			// Dialogue
			DialogueEditor.SetDynamicDialogueHandler( NPCID.Guide, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					if( !conveyance ) {
						return msg;
					}
					conveyance = false;

					// 03 - Find goblin tinkerer
					if( objFindGoblin == null ) {
						ObjectivesAPI.AddObjective( LoreLogic.FindGoblin(), 0, true, out _ );
					}

					if( oldHandler != null ) {
						DialogueEditor.SetDynamicDialogueHandler( NPCID.Guide, oldHandler );
					} else {
						DialogueEditor.RemoveDynamicDialogueHandler( NPCID.Guide );
					}

					return "I have to tell you something. There are natives on this island!"
						+" Not mere scattered survivors or profiteers, but a full blown army of goblins!"
						+" We must find a way to communicate with them directly."
						+" I fear our presence here might be taken the wrong way!";
				},
				isShowingAlert: () => true
			) );

			return false;
		}
	}
}
