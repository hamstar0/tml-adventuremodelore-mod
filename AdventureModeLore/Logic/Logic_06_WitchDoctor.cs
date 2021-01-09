using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Logic {
	public partial class LoreLogic : ILoadable {
		public static string SummonWoFTitle => "Sacrifice Voodoo Doll";

		internal static Objective SummonWoF() {
			return new FlatObjective(
				title: LoreLogic.SummonWoFTitle,
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
			Objective objSummonWoF = ObjectivesAPI.GetObjective( LoreLogic.SummonWoFTitle );

			/***********************/
			/**** Conditions:	****/
			/***********************/

			// Not ready yet?
			if( !NPC.AnyNPCs( NPCID.WitchDoctor ) ) {
				return true;
			}

			// Already done?
			bool isWofFinished = ObjectivesAPI.IsFinishedObjective( LoreLogic.SummonWoFTitle );
			if( isWofFinished ) {
				if( objSummonWoF == null ) {   // Be sure objective is also declared
					ObjectivesAPI.AddObjective( LoreLogic.SummonWoF(), 0, true, out _ );
				}
				return false;
			}

			/***********************/
			/**** Actions:		****/
			/***********************/

			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( NPCID.WitchDoctor );
			int conveyances = 0;

			// Dialogue
			DialogueEditor.SetDynamicDialogueHandler( NPCID.WitchDoctor, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					switch( conveyances++ ) {
					case 0:
						return "What you call a plague is the disruption of the souls of the dead in their journey back into the"
							+ " land. Their anger can be felt by the living and dead alike, causing all of the troubles you now"
							+ " know too well. Long ago, a ritual was performed to steal the power of dead for use by greedy"
							+ " men. It resulted in a war that reached out even into the stars, and nearly brought our world to"
							+ " an end.";
					case 1:
						return "Today, few live who even remember of these events. The end of the war did not truly come until"
							+ " the stolen souls were finally returned to the world. However, these souls had since become"
							+ " troubled, and the ritual performed for this task was executed improperly!";
					case 2:
						return "I'm afraid to say, but it may already be too late. Long have these tormented spirits dwelled in"
							+ " a most pitiful incarceration in the depths. Now their containment is failing, and once unleashed,"
							+ " their wrath will surely plunge all the land into chaos! If you wish to do something about this,"
							+ " you may not like what you will hear next.";
					case 3:
						// 06 - Summon WoF
						if( objSummonWoF == null ) {
							ObjectivesAPI.AddObjective( LoreLogic.SummonWoF(), 0, true, out _ );
						}

						if( oldHandler != null ) {
							DialogueEditor.SetDynamicDialogueHandler( NPCID.WitchDoctor, oldHandler );
						} else {
							DialogueEditor.RemoveDynamicDialogueHandler( NPCID.WitchDoctor );
						}

						return "You must find a living descendant of the ones responsible for placing the fateful seal upon"
							+ " the dead, and sacrifice their soul to these spirits. I will show you the ritual. Alas, a"
							+ " person of whom I speak resides among you as a trusted companion. One who has been with you"
							+ " from the beginning. It is your choice to inform them of this. May this land have mercy on their"
							+ " soul.";
					}
					return msg;
				},
				isShowingAlert: () => true
			) );

			return false;
		}
	}
}
