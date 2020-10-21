using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.NPCChat;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Logic {
	public partial class AMLLogic : ILoadable {
		public static string SummonWoFTitle => "Sacrifice Voodoo Doll";

		internal static Objective SummonWoF() {
			return new FlatObjective(
				title: AMLLogic.FindWitchDoctorTitle,
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
			Objective objSummonWoF = ObjectivesAPI.GetObjective( AMLLogic.SummonWoFTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( !NPC.AnyNPCs( NPCID.WitchDoctor ) ) {
				return true;
			}

			// Already done?
			bool isWofFinished = ObjectivesAPI.IsFinishedObjective( AMLLogic.SummonWoFTitle );
			if( isWofFinished ) {
				if( objSummonWoF == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( AMLLogic.SummonWoF(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			ProcessMessage func = NPCChat.GetPriorityChat( NPCID.WitchDoctor );
			int conveyance = 0;

			// Dialogue
			NPCChat.SetPriorityChat( NPCID.WitchDoctor, ( string msg, out bool alert ) => {
				// Only show NPC alert icon
				if( conveyance <= 3 && string.IsNullOrEmpty(msg) ) {
					alert = true;
					return msg;
				}

				switch( conveyance++ ) {
				case 0:
					alert = true;
					return "What you call a plague is the disruption of the souls of the dead in their journey back into the"
						+ " land. Their anger can be felt by the living and dead alike, causing all of the troubles you now"
						+ " know too well. Long ago, a ritual was performed to steal the power of dead for use by greedy"
						+ " men. It resulted in a war that reached out even into the stars, and nearly brought our world to"
						+ " an end.";
				case 1:
					alert = true;
					return "Today, few live who even remember of these events. The end of the war did not truly come until"
						+ " the stolen souls were finally returned to the world. However, these souls had since become"
						+ " troubled, and the ritual performed for this task was executed improperly!";
				case 2:
					alert = true;
					return "I'm afraid to say, but it may already be too late. Long have these tormented spirits dwelled in"
						+ " a most pitiful incarceration in the depths. Now their containment is failing, and once unleashed,"
						+ " their wrath will surely plunge all the land into chaos! If you wish to do something about this,"
						+ " you may not like what you will hear next.";
				case 3:
					// 06 - Summon WoF
					if( objSummonWoF == null ) {
						ObjectivesAPI.AddObjective( AMLLogic.SummonWoF(), 0, true, out _ );
					}

					alert = false;
					return "You must find a living descendant of the ones responsible for placing the fateful seal upon"
						+ " the dead, and sacrifice their soul to these spirits. I will show you the ritual. Alas, a"
						+ " person of whom I speak resides among you as a trusted companion. One who has been with you"
						+ " from the beginning. It is your choice to inform them of this. May this land have mercy on their"
						+ " soul.";
				default:
					alert = false;

					NPCChat.SetPriorityChat( NPCID.WitchDoctor, func );
					return func?.Invoke( msg, out alert ) ?? msg;
				}
			} );

			return false;
		}
	}
}
