﻿using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using Objectives;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public string Name { get; private set; }

		public Func<bool>[] Prerequisites { get; private set; }

		public int NPCType { get; private set; }

		public NPCLoreSubStage[] SubStages { get; private set; }

		public bool IsRepeatable { get; private set; }



		////////////////

		public NPCLoreStage( string name, Func<bool>[] prereqs, int npcType, NPCLoreSubStage[] subStages, bool isRepeatable ) {
			this.Name = name;
			this.Prerequisites = prereqs;
			this.NPCType = npcType;
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
			foreach( NPCLoreSubStage subStage in this.SubStages ) {
				if( subStage.Objective == null ) { continue; }

				bool isPrevComplete = ObjectivesAPI.HasRecordedObjectiveByNameAsFinished( subStage.Objective.Title );

				if( !isPrevComplete && subStage.Objective.ComputeCompletionPercent() < 1f ) {
					return false;
				}
			}

			return true;
		}
	}
}
