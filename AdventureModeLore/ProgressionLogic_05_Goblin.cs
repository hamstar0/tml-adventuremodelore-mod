using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string FindMechanicTitle => "Find Nechanic";

		public static Objective FindMechanic() {
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

		public static Objective FindWitchDoctor() {
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
			/***********************/
			/**** Conditions:	****/
			/***********************/

			if( !NPC.AnyNPCs(NPCID.GoblinTinkerer) ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.GoblinTinkerer );
			bool conveyance1 = true;
			bool conveyance2 = true;
			bool conveyance3 = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.GoblinTinkerer, ( string msg, out bool alert ) => {
				alert = conveyance1 || conveyance2 || conveyance3;

				if( conveyance1 ) {
					conveyance1 = false;
					return "Sorry, I cannot be of much assistance in diplomacy with my former tribe. I doubt they would have"
						+" an open mind, anyhow.";
				} else if( conveyance2 ) {
					// 05a - Find mechanic
					Objective objFindMechanicBoss = ObjectivesAPI.GetObjective( ProgressionLogic.FindMechanicTitle );
					if( objFindMechanicBoss?.IsComplete != true ) {
						if( objFindMechanicBoss == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.FindMechanic(), 0, true, out _ );
						}
					}

					conveyance2 = false;
					return "I have urgent information for you: An unknown faction basing within the dungeon is attempting"
						+" to infuse spiritual energy into advanced machinery. The undeath plague will be"
						+" nothing compared to the devestation this will unleash! They must be receiving outside help."
						+" Find their engineer!";
				} else if( conveyance3 ) {
					// 05b - Find Witch Doctor
					Objective objFindWitchDoctor = ObjectivesAPI.GetObjective( ProgressionLogic.FindWitchDoctorTitle );
					if( objFindWitchDoctor?.IsComplete != true ) {
						if( objFindWitchDoctor == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.FindWitchDoctor(), 0, true, out _ );
						}
					}

					conveyance3 = false;
					return "I know little about the undeath plague, but I do know of another inhabitant of these lands who"
						+" may: A lone witch doctor residing in the jungle. Unfortunately, he has gone into hiding on"
						+" on account of powerful monsters now residing in the jungle.";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
