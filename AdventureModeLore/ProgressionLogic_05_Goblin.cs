using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	public partial class ProgressionLogic {
		public static string FindMechanicTitle => "Find Nechanic";

		internal static Objective FindMechanic() {
			return new FlatObjective(
				title: ProgressionLogic.FindMechanicTitle,
				description: "Rumors exist of a plan to empower technology with the dungeon's spiritual"
					+ "\n" + "energies. This could be disasterous. Liberate the engineer.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.Mechanic );
				}
			);
		}

		////

		public static string FindWitchDoctorTitle => "Find Witch Doctor";

		internal static Objective FindWitchDoctor() {
			return new FlatObjective(
				title: ProgressionLogic.FindWitchDoctorTitle,
				description: "A mysterious lizard-man sorcerer may know the plague's secret. Some"
					+ "\n" + "powerful monster in the jungle has put him into hiding, though.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.WitchDoctor );
				}
			);
		}


		////////////////

		private static bool Run05_Goblin() {
			Objective objFindMechanicBoss = ObjectivesAPI.GetObjective( ProgressionLogic.FindMechanicTitle );
			Objective objFindWitchDoctor = ObjectivesAPI.GetObjective( ProgressionLogic.FindWitchDoctorTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( !NPC.AnyNPCs(NPCID.GoblinTinkerer) ) {
				return true;
			}

			// Already done?
			bool isMechFinished = ObjectivesAPI.IsFinishedObjective( ProgressionLogic.FindMechanicTitle );
			bool isWitchinished = ObjectivesAPI.IsFinishedObjective( ProgressionLogic.FindWitchDoctorTitle );
			if( isMechFinished || isWitchinished ) {
				if( objFindMechanicBoss == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( ProgressionLogic.FindMechanic(), 0, true, out _ );
				}
				if( objFindWitchDoctor == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( ProgressionLogic.FindWitchDoctor(), 0, true, out _ );
				}
				return false;
			}
			
			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.GoblinTinkerer );
			int conveyance = 0;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.GoblinTinkerer, ( string msg, out bool alert ) => {
				// Only show NPC alert icon
				if( conveyance <= 2 && string.IsNullOrEmpty( msg ) ) {
					alert = true;
					return msg;
				}

				switch( conveyance ) {
				case 0:
					alert = true;
					return "Sorry, I cannot be of much assistance in diplomacy with my former tribe. I doubt they would have"
						+" an open mind, anyhow.";
				case 1:
					// 05a - Find mechanic
					if( objFindMechanicBoss == null ) {
						ObjectivesAPI.AddObjective( ProgressionLogic.FindMechanic(), 0, true, out _ );
					}

					alert = true;
					return "I have urgent information for you: An unknown faction basing within the dungeon is attempting"
						+" to infuse spiritual energy into advanced machinery. The undeath plague will be"
						+" nothing compared to the devestation this will unleash! They must be receiving outside help."
						+" Find their engineer!";
				case 2:
					// 05b - Find Witch Doctor
					if( objFindWitchDoctor == null ) {
						ObjectivesAPI.AddObjective( ProgressionLogic.FindWitchDoctor(), 0, true, out _ );
					}

					alert = false;
					return "I know little about the undeath plague, but I do know of another inhabitant of these lands who"
						+" may: A lone witch doctor residing in the jungle. Unfortunately, he has gone into hiding on"
						+" on account of powerful monsters now residing in the jungle.";
				default:
					alert = false;

					NPCChat.SetPriorityChat( NPCID.GoblinTinkerer, func );
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
