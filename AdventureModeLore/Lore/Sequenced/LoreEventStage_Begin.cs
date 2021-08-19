using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsNPCDialogue.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Sequenced {
	public partial class SequencedLoreEventStage {
		public (bool CanBegin, bool IsDone) GetStatusForLocalPlayer() {
			var myplayer = Main.LocalPlayer.GetModPlayer<AMLPlayer>();
			if( myplayer.CompletedLoreStages.Contains( this.Name ) ) {
				return (false, true);
			}

			return (this.ArePrerequisitesMet(), this.AreAllObjectivesPreviouslyOrInAdvanceComplete());
		}


		public void BeginForLocalPlayer( bool forceObjectiveIncomplete ) {
			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NpcType );
			int currSubStageIdx = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NpcType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					SequencedLoreEventSubStage subStage = this.GetAndAdvanceSubStage(
						forceObjectiveIncomplete: forceObjectiveIncomplete,
						currSubStageIdx: ref currSubStageIdx,
						isFinal: out bool isFinal
					);

					msg = subStage?.OptionalDialogue?.Invoke() ?? msg;

					if( isFinal ) {
						if( oldDialogueHandler != null ) {
							DialogueEditor.SetDynamicDialogueHandler( this.NpcType, oldDialogueHandler );
						} else {
							DialogueEditor.RemoveDynamicDialogueHandler( this.NpcType );
						}
					}

					return msg;
				},
				isShowingAlert: () => true
			) );
		}


		////////////////

		private SequencedLoreEventSubStage GetAndAdvanceSubStage(
					bool forceObjectiveIncomplete,
					ref int currSubStageIdx,
					out bool isFinal ) {
			SequencedLoreEventSubStage subStage = null;
			
			// Skip substages with completed objectives
			for( ; currSubStageIdx < this.SubStages.Length; currSubStageIdx++ ) {
				if( this.ProcessSubStage( this.SubStages[currSubStageIdx], forceObjectiveIncomplete) ) {
					subStage = this.SubStages[currSubStageIdx];

					break;
				}
			}

			// Be ready start with the next sub stage, next time
			currSubStageIdx++;

			subStage?.OptionalAction?.Invoke();

			isFinal = currSubStageIdx >= this.SubStages.Length;
			return subStage;
		}


		////

		private bool ProcessSubStage( SequencedLoreEventSubStage subStage, bool forceObjectiveIncomplete ) {
			// No objective; dialogue only
			if( subStage.OptionalObjective == null ) {
				return true;
			}

			var obj = this.ProcessSubStageObjective( subStage, forceObjectiveIncomplete );

			// If sub stage's objective is incomplete, step only to this sub stage
			return obj.IsComplete != true;
		}

		////

		private Objective ProcessSubStageObjective( SequencedLoreEventSubStage currSubStage, bool forceObjectiveIncomplete ) {
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
