using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues {
	public partial class DialogueLoreEventStage {
		public Func<string> OptionalDialogue { get; private set; }

		public Objective OptionalObjective { get; private set; }



		////////////////

		public DialogueLoreEventStage( Func<string> dialogue ) {
			this.OptionalDialogue = dialogue;
			this.OptionalObjective = null;
		}

		public DialogueLoreEventStage( Func<string> dialogue, Objective objective ) {
			this.OptionalDialogue = dialogue;
			this.OptionalObjective = objective;
		}
	}
}
