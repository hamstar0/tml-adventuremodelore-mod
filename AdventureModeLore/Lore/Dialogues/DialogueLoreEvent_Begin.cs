using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsNPCDialogue.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues {
	public partial class DialogueLoreEvent : LoreEvent {
		private static bool ExecuteStage( DialogueLoreEventStage stage, bool forceObjectiveIncomplete ) {
			if( stage.OptionalObjectives.Length == 0 ) {
				return true;	// No objectives? dialogue only; may advance
			}

			//

			bool isIncomplete = false;

			foreach( Objective dataObj in stage.OptionalObjectives ) {
				Objective obj = DialogueLoreEvent.ExecuteStageObjective( dataObj, forceObjectiveIncomplete );

				// If stage's objective is incomplete, advance only to this stage
				if( obj.IsComplete != true ) {
					isIncomplete = true;
				}
			}

			return isIncomplete;
		}

		////

		private static Objective ExecuteStageObjective(
					Objective dataOnlyObjective,
					bool forceObjectiveIncomplete ) {
			string objectiveName = dataOnlyObjective.Title;

			if( ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( objectiveName ) ) {
				if( forceObjectiveIncomplete ) {
					ObjectivesAPI.RemoveObjectiveIf( objectiveName, true );
				}
			}

			Objective objective = ObjectivesAPI.GetObjective( objectiveName );
			if( objective == null ) {
				objective = dataOnlyObjective;

				ObjectivesAPI.AddObjective( // Evaluates `obj` if finished
					objective: objective,
					order: -1,
					alertPlayer: true,
					out string _
				);
			}

			return objective;
		}



		////////////////

		public override void Begin_Local( bool isRepeat ) {
			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NpcType );
			int currStageIdx = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NpcType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					DialogueLoreEventStage stage = this.GetAndAdvanceStage(
						forceObjectiveIncomplete: isRepeat,
						currStageIdx: ref currStageIdx,
						isFinal: out bool isFinal
					);

					msg = stage?.OptionalDialogue?.Invoke()
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

		private DialogueLoreEventStage GetAndAdvanceStage(
					bool forceObjectiveIncomplete,
					ref int currStageIdx,
					out bool isFinal ) {
			DialogueLoreEventStage stage = null;
			
			// Skip substages with completed objectives
			for( ; currStageIdx < this.Stages.Length; currStageIdx++ ) {
				DialogueLoreEventStage currStage = this.Stages[ currStageIdx ];

				if( DialogueLoreEvent.ExecuteStage(currStage, forceObjectiveIncomplete) ) {
					stage = currStage;

					break;
				}
			}

			// Be ready start with the next stage, next time
			currStageIdx++;

			isFinal = currStageIdx >= this.Stages.Length;
			return stage;
		}
	}
}
