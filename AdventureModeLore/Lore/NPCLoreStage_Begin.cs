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

			//

			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NPCType );
			int currStage = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NPCType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					return this.GetDialogue( oldDialogueHandler, msg, ref currStage );
				},
				isShowingAlert: () => true
			) );

			return true;
		}


		////////////////

		private string GetDialogue( DynamicDialogueHandler oldDialogueHandler, string existingMessage, ref int currStage ) {
			NPCLoreSubStage stage = this.StepStage( ref currStage );
			if( stage != null ) {
				return stage.Dialogue.Invoke();
			}

			if( oldDialogueHandler != null ) {
				DialogueEditor.SetDynamicDialogueHandler( this.NPCType, oldDialogueHandler );
			} else {
				DialogueEditor.RemoveDynamicDialogueHandler( this.NPCType );
			}

			return existingMessage;
		}


		////////////////

		private NPCLoreSubStage StepStage( ref int currStage ) {
			for( ; currStage < this.SubStages.Length; currStage++ ) {
				NPCLoreSubStage stage = this.SubStages[currStage];
				if( stage.Objective == null ) {
					return stage;
				}

				string objTitle = stage.Objective.Title;
				Objective obj = ObjectivesAPI.GetObjective( objTitle );

				if( obj == null ) {
					bool success = ObjectivesAPI.AddObjective(
						objective: obj,
						order: -1,
						alertPlayer: true,
						out string _
					);

					if( success ) {
						obj = ObjectivesAPI.GetObjective( objTitle );
					} else {
						throw new ModHelpersException( "Could not add objective '" + objTitle + "'" );
					}
				}

				if( !obj.IsComplete ) {
					return stage;
				}
			}

			return null;
		}
	}
}
