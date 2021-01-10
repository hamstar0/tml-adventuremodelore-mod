using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public bool Begin() {
			if( !this.ArePrerequisitesMet() ) {
				return false;
			}
			//if( this.IsComplete ) {	// TODO
			//	return true;
			//}

			//

			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NPCType );
			int currStage = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NPCType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					return this.GetDialogue( msg, oldDialogueHandler, ref currStage );
				},
				isShowingAlert: () => true
			) );

			return true;
		}


		////////////////

		private string GetDialogue( string existingMessage, DynamicDialogueHandler oldDialogueHandler, ref int currStage ) {
			string msg = existingMessage;
			NPCLoreSubStage subStage = this.StepStage( ref currStage );

			if( subStage != null ) {
				msg = subStage.Dialogue.Invoke();
			}

			if( currStage < this.SubStages.Length ) {
				return msg;
			}

			if( oldDialogueHandler != null ) {
				DialogueEditor.SetDynamicDialogueHandler( this.NPCType, oldDialogueHandler );
			} else {
				DialogueEditor.RemoveDynamicDialogueHandler( this.NPCType );
			}

			return msg;
		}


		////////////////

		private NPCLoreSubStage StepStage( ref int currStage ) {
			NPCLoreSubStage resultSubStage = null;

			for( ; currStage < this.SubStages.Length; currStage++ ) {
				NPCLoreSubStage currSubStage = this.SubStages[ currStage ];

				if( currSubStage.Objective == null ) {
					resultSubStage = currSubStage;
					break;
				}

				Objective obj = ObjectivesAPI.GetObjective( currSubStage.Objective.Title );

				if( obj == null ) {
					obj = currSubStage.Objective;

					ObjectivesAPI.AddObjective(
						objective: obj,
						order: -1,
						alertPlayer: true,
						out string _
					);
				}

				if( !obj.IsComplete ) {
					resultSubStage = currSubStage;
					break;
				}
			}

			currStage++;
			return resultSubStage;
		}
	}
}
