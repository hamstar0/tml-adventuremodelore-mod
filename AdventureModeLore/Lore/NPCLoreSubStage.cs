using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreSubStage {
		public Func<string> Dialogue { get; private set; }

		public Objective Objective { get; private set; }



		////////////////

		public NPCLoreSubStage( Func<string> dialogue, Objective objective=null ) {
			this.Dialogue = dialogue;
			this.Objective = objective;
		}
	}
}
