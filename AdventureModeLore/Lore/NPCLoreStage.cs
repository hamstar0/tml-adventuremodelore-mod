using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public string[] PrerequisiteObjectives { get; private set; }

		public int NPCType { get; private set; }

		public NPCLoreSubStage[] SubStages { get; private set; }



		////////////////
		
		public NPCLoreStage( string[] prereqObjectives, int npcType, NPCLoreSubStage[] subStages ) {
			this.PrerequisiteObjectives = prereqObjectives;
			this.NPCType = npcType;
			this.SubStages = subStages;
		}
	}
}
