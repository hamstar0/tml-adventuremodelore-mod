using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	public partial class ProgressionLogic {
		public static string FindMerchantTitle => "Find The Merchant";

		internal static Objective FindMerchant() {
			return new FlatObjective(
				title: ProgressionLogic.FindMerchantTitle,
				description: "Other inhabitants exist in this land, some less enslaved than others. Build a"
					+ "\n" + "house for the merchant to settle in.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.Merchant );
				}
			);
		}

		////

		public static string ReachJungleTitle => "Find Jungle";

		internal static Objective ReachJungle() {
			return new FlatObjective(
				title: ProgressionLogic.ReachJungleTitle,
				description: "The old man says there's something suspicious in the jungle. Maybe take a look?",
				condition: ( obj ) => {
					return Main.LocalPlayer.ZoneJungle;
				}
			);
		}


		////////////////

		private static bool Run01_OldMan() {
			Objective objInvesDung = ObjectivesAPI.GetObjective( ProgressionLogic.InvestigateDungeonTitle );

			Objective objReachJungle = ObjectivesAPI.GetObjective( ProgressionLogic.ReachJungleTitle );
			Objective objFindMerch = ObjectivesAPI.GetObjective( ProgressionLogic.FindMerchantTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( objInvesDung?.IsComplete != true ) {
				return true;
			}

			// Already done?
			bool isJungFinished = ObjectivesAPI.IsFinishedObjective( ProgressionLogic.ReachJungleTitle );
			bool isMerchFinished = ObjectivesAPI.IsFinishedObjective( ProgressionLogic.FindMerchantTitle );
			if( isJungFinished || isMerchFinished ) {
				if( objReachJungle == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( ProgressionLogic.ReachJungle(), 0, true, out _ );
				}
				if( objFindMerch == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( ProgressionLogic.FindMerchant(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.OldMan );
			int conveyance = 0;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.OldMan, ( string msg, out bool alert ) => {
				// Only show NPC alert icon
				if( conveyance <= 1 && string.IsNullOrEmpty(msg) ) {
					alert = true;
					return msg;
				}
				
				switch( conveyance ) {
				case 0:
					// 01b Objective: Find Merchant
					if( objFindMerch == null ) {
						ObjectivesAPI.AddObjective( ProgressionLogic.FindMerchant(), 0, true, out _ );
					}

					alert = true;
					return "You're in no shape to concern with why I'm here, or what this place is."
						+ " No, I'm not the only inhabitant of this cursed land, but anyone with sense will be in hiding."
						+ " They might come to you if you have something to offer them and a safe place to stay.";
				case 1:
					// 01a Objective: Reach Jungle
					if( objReachJungle == null ) {
						ObjectivesAPI.AddObjective( ProgressionLogic.ReachJungle(), 0, true, out _ );
					}

					alert = false;
					return "The plague? I know nothing of this. All I know is I made a pact long ago to keep this place sealed."
						+ " I don't even remember why. This dungeon has its secrets, but I want nothing to do with it!"
						+ "\nYou might instead try investigating the jungle. I hear it too has its secrets. And dangers.";
				default:
					alert = false;

					NPCChat.SetPriorityChat( NPCID.OldMan, func );
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
