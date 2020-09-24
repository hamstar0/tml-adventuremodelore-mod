using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	partial class ProgressionLogic {
		public static string SummonWoFTitle => "Sacrifice Voodoo Doll";

		public static Objective SummonWoF() {
			return new FlatObjective(
				title: ProgressionLogic.FindWitchDoctorTitle,
				description: "The witch doctor describes a ritual to destroy the spiritual energy"
					+ "\n" + "confluence; the source of the plague. It involves a voodoo sacrifice"
					+ "\n" + "of one of its makers near its source: The underworld.",
				condition: ( obj ) => {
					return NPC.AnyNPCs( NPCID.WallofFlesh );
				}
			);
		}


		////////////////

		private static bool Run06_WitchDoctor() {
			/***********************/
			/**** Conditions:	****/
			/***********************/

			if( !NPC.AnyNPCs( NPCID.WitchDoctor ) ) {
				return true;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.WitchDoctor );
			bool conveyance1 = true;
			bool conveyance2 = true;
			bool conveyance3 = true;
			bool conveyance4 = true;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.WitchDoctor, ( string msg, out bool alert ) => {
				alert = conveyance1 || conveyance2 || conveyance3 || conveyance4;

				if( conveyance1 ) {
					conveyance1 = false;
					return "What you call a plague is the disruption of the souls of the dead in their journey back into the"
						+" land. Their anger can be felt by the living and dead alike, causing all of the troubles you now"
						+" know too well. Long ago, a ritual was performed to steal the power of dead for use by greedy"
						+" men. It resulted in a war that reached out even into the stars, and nearly brought our world to"
						+" an end.";
				} else if( conveyance2 ) {
					conveyance2 = false;
					return "Today, few live who even remember of these events. The end of the war did not truly come until"
						+" the stolen souls were finally returned to the earth. However, these souls had since become"
						+" troubled, and the ritual performed for this task was not complete!";
				} else if( conveyance3 ) {
					conveyance3 = false;
					return "I'm afraid to say, but it may be too late. Long have these tormented spirits dwelled in a most"
						+" pitiful incarceration. Even now, their containment is failing, and once unleashed, their wrath"
						+" will surely plunge all the land into chaos! If you wish to do something about this, you may not"
						+" like what you will hear next.";
				} else if( conveyance4 ) {
					// 06 - Summon WoF
					Objective objSummonWoF = ObjectivesAPI.GetObjective( ProgressionLogic.SummonWoFTitle );
					if( objSummonWoF?.IsComplete != true ) {
						if( objSummonWoF == null ) {
							ObjectivesAPI.AddObjective( ProgressionLogic.SummonWoF(), 0, true, out _ );
						}
					}

					conveyance4 = false;
					return "You must find a living descendent of the ones responsible for placing the fateful seal upon"
						+" the dead, and sacrifice their soul to these spirits. I will show you the ritual. Alas, a"
						+" person of whom I speak resides among you as a trusted companion. One who has been with you"
						+" from the beginning. It is your choice to inform them of this. May this land have mercy on their"
						+" soul.";
				} else {
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
