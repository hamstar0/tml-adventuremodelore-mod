using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public (bool CanBegin, bool IsDone) GetStatusForLocalPlayer() {
			var myplayer = Main.LocalPlayer.GetModPlayer<AMLPlayer>();
			if( myplayer.CompletedLoreStages.Contains( this.Name ) ) {
				return (false, true);
			}

			return (this.ArePrerequisitesMet(), this.AreAllObjectivesPreviouslyOrInAdvanceComplete());
		}


		public void BeginForLocalPlayer( bool forceObjectiveIncomplete ) {
			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NPCType );
			int currSubStage = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NPCType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					msg = this.GetDialogueAndAdvanceSubStage( msg, forceObjectiveIncomplete, ref currSubStage, out bool isFinal );

					if( isFinal ) {
						if( oldDialogueHandler != null ) {
							DialogueEditor.SetDynamicDialogueHandler( this.NPCType, oldDialogueHandler );
						} else {
							DialogueEditor.RemoveDynamicDialogueHandler( this.NPCType );
						}
					}

					return msg;
				},
				isShowingAlert: () => true
			) );
		}


		////////////////

		private string GetDialogueAndAdvanceSubStage(
					string existingMessage,
					bool forceObjectiveIncomplete,
					ref int currSubStage,
					out bool isFinal ) {
			NPCLoreSubStage subStage = this.StepSubStage( ref currSubStage, forceObjectiveIncomplete, out isFinal );

			return subStage?.Dialogue.Invoke()
				?? existingMessage;
		}


		////////////////

		private NPCLoreSubStage StepSubStage( ref int currStage, bool forceObjectiveIncomplete, out bool isFinal ) {
			NPCLoreSubStage resultSubStage = null;

			for( ; currStage < this.SubStages.Length; currStage++ ) {
				NPCLoreSubStage currSubStage = this.SubStages[ currStage ];
				string objectiveName = currSubStage.Objective.Title;

				// No objective; dialogue only
				if( currSubStage.Objective == null ) {
					resultSubStage = currSubStage;
					break;
				}

				if( ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(objectiveName) && forceObjectiveIncomplete ) {
					ObjectivesAPI.RemoveObjective( objectiveName, true );
				}

				Objective obj = ObjectivesAPI.GetObjective( objectiveName );
				if( obj == null ) {
					obj = currSubStage.Objective;

					ObjectivesAPI.AddObjective(	// Evaluates `obj` if finished
						objective: obj,
						order: -1,
						alertPlayer: true,
						out string _
					);
				}

				if( obj.IsComplete != true ) {
					resultSubStage = currSubStage;
					break;
				}
			}

			currStage++;

			isFinal = currStage >= this.SubStages.Length;
			return resultSubStage;
		}
	}
}
