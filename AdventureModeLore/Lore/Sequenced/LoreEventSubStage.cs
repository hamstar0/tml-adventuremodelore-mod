using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogue {
	public partial class DialogueLoreEventStage {
		public string OptionalRadio { get; private set; }
		
		public Func<string> OptionalDialogue { get; private set; }

		public Objective OptionalObjective { get; private set; }



		////////////////

		public DialogueLoreEventStage( string radioMessage ) {
			this.OptionalRadio = radioMessage;
			this.OptionalDialogue = null;
			this.OptionalObjective = null;
		}

		public DialogueLoreEventStage( Func<string> dialogue ) {
			this.OptionalRadio = null;
			this.OptionalDialogue = dialogue;
			this.OptionalObjective = null;
		}

		public DialogueLoreEventStage( Func<string> dialogue, Objective objective ) {
			this.OptionalRadio = null;
			this.OptionalDialogue = dialogue;
			this.OptionalObjective = objective;
		}
	}
}
