using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Logic {
	public partial class LoreLogic : ILoadable {
		public static string FindMerchantTitle => "Find A Merchant";

		internal static Objective FindMerchant() {
			return new FlatObjective(
				title: LoreLogic.FindMerchantTitle,
				description: "Other inhabitants exist in this land, some less enslaved than others. Build a"
					+ "\n" + "house for a merchant to settle in.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.Merchant );
				}
			);
		}

		////

		public static string ReachJungleTitle => "Find Jungle";

		internal static Objective ReachJungle() {
			return new FlatObjective(
				title: LoreLogic.ReachJungleTitle,
				description: "The old man says there's something suspicious in the jungle. Maybe take a look?",
				condition: ( obj ) => {
					return Main.LocalPlayer.ZoneJungle;
				}
			);
		}


		////////////////

		private static bool Run01_OldMan() {
			Objective objInvesDung = ObjectivesAPI.GetObjective( LoreLogic.InvestigateDungeonTitle );

			Objective objReachJungle = ObjectivesAPI.GetObjective( LoreLogic.ReachJungleTitle );
			Objective objFindMerch = ObjectivesAPI.GetObjective( LoreLogic.FindMerchantTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( objInvesDung?.IsComplete != true ) {
				return true;
			}

			// Already done?
			bool isJungFinished = ObjectivesAPI.IsFinishedObjective( LoreLogic.ReachJungleTitle );
			bool isMerchFinished = ObjectivesAPI.IsFinishedObjective( LoreLogic.FindMerchantTitle );
			if( isJungFinished || isMerchFinished ) {
				if( objReachJungle == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( LoreLogic.ReachJungle(), 0, true, out _ );
				}
				if( objFindMerch == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( LoreLogic.FindMerchant(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( NPCID.OldMan );
			int conveyance = 0;

			// Dialogue
			DialogueEditor.SetDynamicDialogueHandler( NPCID.OldMan, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					switch( conveyance++ ) {
					case 0:
						// 01b Objective: Find Merchant
						if( objFindMerch == null ) {
							ObjectivesAPI.AddObjective( LoreLogic.FindMerchant(), 0, true, out _ );
						}

						return "You're in no shape to concern with why I'm here, or what this place is."
							+ " No, I'm not the only inhabitant of this cursed land, but anyone with sense will be in hiding."
							+ " They might come to you if you have something to offer them and a safe place to stay.";
					case 1:
						// 01a Objective: Reach Jungle
						if( objReachJungle == null ) {
							ObjectivesAPI.AddObjective( LoreLogic.ReachJungle(), 0, true, out _ );
						}

						msg = "The plague? I know nothing of this. All I know is I made a pact long ago to keep this place sealed."
							+ " I don't even remember why. This dungeon has its secrets, but I want nothing to do with it!"
							+ "\nYou might instead try investigating the jungle. I hear it too has its secrets. And dangers.";
						break;
					}

					if( oldHandler != null ) {
						DialogueEditor.SetDynamicDialogueHandler( NPCID.OldMan, oldHandler );
					} else {
						DialogueEditor.RemoveDynamicDialogueHandler( NPCID.OldMan );
					}

					return msg;
				},
				isShowingAlert: () => true
			) );

			return false;
		}
	}
}
