using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsNPCDialogue.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues {
	public partial class DialogueLoreEvent : LoreEvent {
		public override void BeginForLocalPlayer( bool isRepeat ) {
			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NpcType );
			int currSubStageIdx = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NpcType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					DialogueLoreEventStage subStage = this.GetAndAdvanceSubStage(
						forceObjectiveIncomplete: isRepeat,
						currSubStageIdx: ref currSubStageIdx,
						isFinal: out bool isFinal
					);

					msg = subStage?.OptionalDialogue?.Invoke()
						?? msg;

					if( isFinal ) {
						if( oldDialogueHandler != null ) {
							// Restore previous dialogue handler
							DialogueEditor.SetDynamicDialogueHandler( this.NpcType, oldDialogueHandler );
						} else {
							// Otherwise, clear handler
							DialogueEditor.RemoveDynamicDialogueHandler( this.NpcType );
						}
					}

					return msg;
				},
				isShowingAlert: () => true
			) );
		}


		////////////////

		private DialogueLoreEventStage GetAndAdvanceSubStage(
					bool forceObjectiveIncomplete,
					ref int currSubStageIdx,
					out bool isFinal ) {
			DialogueLoreEventStage subStage = null;
			
			// Skip substages with completed objectives
			for( ; currSubStageIdx < this.Stages.Length; currSubStageIdx++ ) {
				if( this.ProcessSubStage( this.Stages[currSubStageIdx], forceObjectiveIncomplete) ) {
					subStage = this.Stages[currSubStageIdx];

					break;
				}
			}

			// Be ready start with the next sub stage, next time
			currSubStageIdx++;

			isFinal = currSubStageIdx >= this.Stages.Length;
			return subStage;
		}


		////

		private bool ProcessSubStage( DialogueLoreEventStage subStage, bool forceObjectiveIncomplete ) {
			// No objective; dialogue only
			if( subStage.OptionalObjective == null ) {
				return true;
			}

			var obj = this.ProcessSubStageObjective( subStage, forceObjectiveIncomplete );

			// If sub stage's objective is incomplete, step only to this sub stage
			return obj.IsComplete != true;
		}

		////

		private Objective ProcessSubStageObjective( DialogueLoreEventStage currSubStage, bool forceObjectiveIncomplete ) {
			string objectiveName = currSubStage.OptionalObjective.Title;

			if( ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(objectiveName) ) {
				if( forceObjectiveIncomplete ) {
					ObjectivesAPI.RemoveObjective( objectiveName, true );
				}
			}

			Objective obj = ObjectivesAPI.GetObjective( objectiveName );
			if( obj == null ) {
				obj = currSubStage.OptionalObjective;

				ObjectivesAPI.AddObjective( // Evaluates `obj` if finished
					objective: obj,
					order: -1,
					alertPlayer: true,
					out string _
				);
			}

			return obj;
		}
	}
}
