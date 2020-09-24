using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string FindMerchantTitle => "Find The Merchant";

		public static Objective FindMerchant() {
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

		public static Objective ReachJungle() {
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
			/***********************/
			/**** Conditions:	****/
			/***********************/

			Objective objInvesDung = ObjectivesAPI.GetObjective( ProgressionLogic.InvestigateDungeonTitle );
			if( objInvesDung?.IsComplete != true ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.OldMan );
			bool conveyance1 = true;
			bool conveyance2 = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.OldMan, ( string msg, out bool alert ) => {
				alert = conveyance1 || conveyance2;

				if( conveyance1 ) {
					// 01a Objective: Reach Jungle
					Objective objReachJungle = ObjectivesAPI.GetObjective( ProgressionLogic.ReachJungleTitle );
					bool reachJungle = objReachJungle?.IsComplete == true;
					if( !reachJungle ) {
						if( objReachJungle == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.ReachJungle(), 0, true, out _ );
						}
					}

					conveyance1 = false;
					return "You're in no shape to concern with why I'm here, or what this place is."
						+ " No, I'm not the only inhabitant of this cursed land, but anyone with sense will be in hiding."
						+ " They might come to you if you have something to offer them and a safe place to stay.";
				} else if( conveyance2 ) {
					// 01b Objective: Build house
					Objective objFindMerch = ObjectivesAPI.GetObjective( ProgressionLogic.FindMerchantTitle );
					bool buildHouseDone = objFindMerch?.IsComplete == true;
					if( !buildHouseDone ) {
						if( objFindMerch == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.FindMerchant(), 0, true, out _ );
						}
					}

					conveyance2 = false;
					return "The plague? I know nothing of this. All I know is I made a pact long ago to keep this place sealed."
						+ " I don't even remember why. This dungeon has its secrets, but I want nothing to do with it!"
						+ "\nYou might instead try investigating the jungle. I hear it too has its secrets. And dangers.";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
