using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using Objectives;


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
			foreach( DialogueLoreEventStage substage in this.Stages ) {
				if( substage.OptionalObjective == null ) {
					continue;
				}
				if( !ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( substage.OptionalObjective.Title ) ) {
					continue;
				}

				if( ObjectivesAPI.GetObjective( substage.OptionalObjective.Title ) == null ) {
					ObjectivesAPI.AddObjective( substage.OptionalObjective, 0, false, out _ );
				}
			}
		}


		////////////////

		protected override bool HasEventFinished() {
			// Check objectives
			foreach( DialogueLoreEventStage stage in this.Stages ) {
				if( stage.OptionalObjective == null ) {
					continue;
				}

				bool isPrevComplete = ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(
					stage.OptionalObjective.Title
				);

				if( !isPrevComplete && stage.OptionalObjective.ComputeCompletionPercent() < 1f ) {
					return false;
				}
			}

			return true;
		}
	}
}
