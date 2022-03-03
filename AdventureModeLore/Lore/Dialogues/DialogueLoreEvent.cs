using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues {
	public partial class DialogueLoreEvent : LoreEvent {
		public int NpcType { get; private set; }

		public DialogueLoreEventStage[] Stages { get; private set; }



		////////////////

		public DialogueLoreEvent(
					string name,
					Func<bool>[] prereqs,
					int npcType,
					DialogueLoreEventStage[] subStages,
					bool isRepeatable )
				: base( name, prereqs, isRepeatable ) {
			this.NpcType = npcType;
			this.Stages = subStages;
		}


		////

		internal override void Initialize() {
			// Process event objectives
			foreach( DialogueLoreEventStage stage in this.Stages ) {
				foreach( Objective objective in stage.OptionalObjectives ) {
					if( !ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( objective.Title ) ) {
						continue;
					}

					if( ObjectivesAPI.GetObjective( objective.Title ) == null ) {
						ObjectivesAPI.AddObjective( objective, 0, false, out _ );
					}
				}
			}
		}


		////////////////

		protected override bool HasEventFinished() {
			// Check objectives
			foreach( DialogueLoreEventStage stage in this.Stages ) {
				foreach( Objective objective in stage.OptionalObjectives ) {
					bool isPrevComplete = ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(
						objective.Title
					);

					if( !isPrevComplete && objective.ComputeCompletionPercent() < 1f ) {
						return false;
					}
				}
			}

			return true;
		}
	}
}
