﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Logic {
	public partial class AMLLogic : ILoadable {
		public static string FindOrbTitle => "Find an Orb";

		internal static Objective FindOrb() {
			return new FlatObjective(
				title: AMLLogic.FindOrbTitle,
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
			Objective objFindMerch = ObjectivesAPI.GetObjective( AMLLogic.FindMerchantTitle );
			Objective objReachJungle = ObjectivesAPI.GetObjective( AMLLogic.ReachJungleTitle );

			Objective objFindOrb = ObjectivesAPI.GetObjective( AMLLogic.FindOrbTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( objFindMerch?.IsComplete != true ) {
				return true;
			}

			// Already done?
			if( ObjectivesAPI.IsFinishedObjective(AMLLogic.FindOrbTitle) ) {
				if( objFindOrb == null ) {	// Be sure objective is also declared
					ObjectivesAPI.AddObjective( AMLLogic.FindOrb(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( NPCID.Merchant );
			bool conveyance = true;

			// Dialogue
			DialogueEditor.SetDynamicDialogueHandler( NPCID.Merchant, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					if( !conveyance ) {
						return msg;
					}
					conveyance = false;

					// 02 - Find an Orb
					if( objFindOrb == null ) {
						ObjectivesAPI.AddObjective( AMLLogic.FindOrb(), 0, true, out _ );
					}

					if( oldHandler != null ) {
						DialogueEditor.SetDynamicDialogueHandler( NPCID.Merchant, oldHandler );
					} else {
						DialogueEditor.RemoveDynamicDialogueHandler( NPCID.Merchant );
					}

					return "I go where the money is. If you're looking for some, you'll need to find treasures."
						+" This land is enchanted, and most areas can be accessed by using sacred magic orbs."
						+" These can often be found accompanying other treasures. Strange, huh?";
				},
				isShowingAlert: () => true
			) );

			return false;
		}
	}
}