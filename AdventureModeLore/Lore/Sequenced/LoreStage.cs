using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using Objectives;


namespace AdventureModeLore.Lore.Sequenced {
	public partial class SequencedLoreStage {
		public string Name { get; private set; }

		public Func<bool>[] Prerequisites { get; private set; }

		public int NpcType { get; private set; }

		public SequencedLoreSubStage[] SubStages { get; private set; }

		public bool IsRepeatable { get; private set; }



		////////////////

		public SequencedLoreStage(
					string name,
					Func<bool>[] prereqs,
					int npcType,
					SequencedLoreSubStage[] subStages,
					bool isRepeatable ) {
			this.Name = name;
			this.Prerequisites = prereqs;
			this.NpcType = npcType;
			this.SubStages = subStages;
			this.IsRepeatable = isRepeatable;
		}


		////

		public bool ArePrerequisitesMet() {
			foreach( Func<bool> prereq in this.Prerequisites ) {
				if( !prereq.Invoke() ) {
					return false;
				}
			}

			return true;
		}


		public bool AreAllObjectivesPreviouslyOrInAdvanceComplete() {
			foreach( SequencedLoreSubStage subStage in this.SubStages ) {
				if( subStage.OptionalObjective == null ) { continue; }

				bool isPrevComplete = ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( subStage.OptionalObjective.Title );

				if( !isPrevComplete && subStage.OptionalObjective.ComputeCompletionPercent() < 1f ) {
					return false;
				}
			}

			return true;
		}
	}
}
