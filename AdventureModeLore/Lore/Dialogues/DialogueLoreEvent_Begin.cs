using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsNPCDialogue.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues {
	public partial class DialogueLoreEvent : LoreEvent {
		private static bool ProcessSubStage( DialogueLoreEventStage subStage, bool forceObjectiveIncomplete ) {
			// No objective; dialogue only
			if( subStage.OptionalObjectives.Length == 0 ) {
				return true;
			}

			foreach( Objective topLevelObj in subStage.OptionalObjectives ) {
				Objective currObj = DialogueLoreEvent.ProcessSubStageObjective( topLevelObj, forceObjectiveIncomplete );

				// If sub stage's objective is incomplete, step only to this sub stage
				if( currObj.IsComplete != true ) {
					return false;
				}
			}

			return true;
		}

		////

		private static Objective ProcessSubStageObjective(
					Objective topLevelObj,
					bool forceObjectiveIncomplete ) {
			string objectiveName = topLevelObj.Title;

			if( ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( objectiveName ) ) {
				if( forceObjectiveIncomplete ) {
					ObjectivesAPI.RemoveObjectiveIf( objectiveName, true );
				}
			}

			Objective obj = ObjectivesAPI.GetObjective( objectiveName );
			if( obj == null ) {
				obj = topLevelObj;

				ObjectivesAPI.AddObjective( // Evaluates `obj` if finished
					objective: obj,
					order: -1,
					alertPlayer: true,
					out string _
				);
			}

			return obj;
		}



		////////////////

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
				if( DialogueLoreEvent.ProcessSubStage( this.Stages[currSubStageIdx], forceObjectiveIncomplete) ) {
					subStage = this.Stages[currSubStageIdx];

					break;
				}
			}

			// Be ready start with the next sub stage, next time
			currSubStageIdx++;

			isFinal = currSubStageIdx >= this.Stages.Length;
			return subStage;
		}
	}
}
