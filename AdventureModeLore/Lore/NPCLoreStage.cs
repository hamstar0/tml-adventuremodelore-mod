using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using Objectives;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public string[] PrerequisiteObjectives { get; private set; }

		public Func<bool>[] PrerequisiteConditions { get; private set; }

		public int NPCType { get; private set; }

		public NPCLoreSubStage[] SubStages { get; private set; }



		////////////////

		public NPCLoreStage( string[] prereqObjectives, Func<bool>[] prereqConditions, int npcType, NPCLoreSubStage[] subStages ) {
			this.PrerequisiteObjectives = prereqObjectives;
			this.PrerequisiteConditions = prereqConditions;
			this.NPCType = npcType;
			this.SubStages = subStages;
		}


		////

		public bool ArePrerequisitesMet() {
			foreach( string prereq in this.PrerequisiteObjectives ) {
				if( !ObjectivesAPI.IsFinishedObjective( prereq ) ) {
					return false;
				}
			}

			foreach( Func<bool> prereq in this.PrerequisiteConditions ) {
				if( !prereq.Invoke() ) {
					return false;
				}
			}

			return true;
		}
	}
}
