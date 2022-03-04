using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues {
	public partial class DialogueLoreEventStage {
		public Func<string> OptionalDialogue { get; private set; }

		public Objective[] OptionalObjectives { get; private set; }



		////////////////

		public DialogueLoreEventStage( Func<string> dialogue ) {
			this.OptionalDialogue = dialogue;
			this.OptionalObjectives = new Objective[] { };
		}

		public DialogueLoreEventStage( Func<string> dialogue, Objective objective ) {
			this.OptionalDialogue = dialogue;
			this.OptionalObjectives = new Objective[] { objective };
		}

		public DialogueLoreEventStage( Func<string> dialogue, Objective[] objectives ) {
			this.OptionalDialogue = dialogue;
			this.OptionalObjectives = objectives;
		}
	}
}
