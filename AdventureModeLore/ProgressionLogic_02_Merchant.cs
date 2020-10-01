using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string FindOrbTitle => "Find an Orb";

		public static Objective FindOrb() {
			return new FlatObjective(
				title: ProgressionLogic.FindOrbTitle,
				description: "It seems the land itself is enchanted. Sacred orbs appear to resonate with"
					+ "\n" + "terrain. Maybe this will be of help?",
				condition: ( obj ) => {
					var orbsMod = ModLoader.GetMod( "Orbs" );

					return PlayerItemFinderHelpers.CountTotalOfEach(
						player: Main.LocalPlayer,
						itemTypes: new HashSet<int> {
							orbsMod.ItemType("RedOrbItem"),
							orbsMod.ItemType("BlueOrbItem"),
							orbsMod.ItemType("TealOrbItem"),
							orbsMod.ItemType("PurpleOrbItem"),
							orbsMod.ItemType("CyanOrbItem"),
							orbsMod.ItemType("GreenOrbItem"),
							orbsMod.ItemType("PinkOrbItem"),
							orbsMod.ItemType("YellowOrbItem"),
							orbsMod.ItemType("WhiteOrbItem")
						},
						includeBanks: true
					) > 0;
				}
			);
		}


		////////////////

		private static bool Run02_Merchant() {
			/***********************/
			/**** Conditions:	****/
			/***********************/

			Objective objFindMerch = ObjectivesAPI.GetObjective( ProgressionLogic.FindMerchantTitle );
			if( objFindMerch?.IsComplete != true ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			Objective objReachJungle = ObjectivesAPI.GetObjective( ProgressionLogic.ReachJungleTitle );
			Objective objBuildHouse = ObjectivesAPI.GetObjective( ProgressionLogic.FindMerchantTitle );
			
			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.Merchant );
			bool conveyance = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.Merchant, ( string msg, out bool alert ) => {
				alert = conveyance;
				if( alert && string.IsNullOrEmpty(msg) ) {
					return msg;
				}

				if( conveyance ) {
					// 02 - Find an Orb
					Objective objFindOrb = ObjectivesAPI.GetObjective( ProgressionLogic.FindOrbTitle );
					if( objFindOrb?.IsComplete != true ) {
						if( objFindOrb == null ) {
							if( objReachJungle?.IsComplete == true && objBuildHouse?.IsComplete == true ) {
								ObjectivesAPI.AddObjective( ProgressionLogic.FindOrb(), 0, true, out _ );
							}
						}
					}

					conveyance = false;
					return "I go where the money is. If you're looking for some, you'll need to find treasures."
						+" This land is enchanted, and most areas can be accessed by using sacred magic orbs."
						+" These can often be found accompanying other treasures. Strange, huh?";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
