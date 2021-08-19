using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Sequenced {
	public partial class SequencedLoreEventSubStage {
		public string OptionalRadio { get; private set; }
		
		public Func<string> OptionalDialogue { get; private set; }

		public Objective OptionalObjective { get; private set; }

		public Action OptionalAction { get; private set; }



		////////////////

		public SequencedLoreEventSubStage( string radioMessage, Action optionalAction = null ) {
			this.OptionalRadio = radioMessage;
			this.OptionalDialogue = null;
			this.OptionalObjective = null;
			this.OptionalAction = optionalAction;
		}

		public SequencedLoreEventSubStage( Func<string> dialogue, Action optionalAction = null ) {
			this.OptionalRadio = null;
			this.OptionalDialogue = dialogue;
			this.OptionalObjective = null;
			this.OptionalAction = optionalAction;
		}

		public SequencedLoreEventSubStage( Func<string> dialogue, Objective objective, Action optionalAction = null ) {
			this.OptionalRadio = null;
			this.OptionalDialogue = dialogue;
			this.OptionalObjective = objective;
			this.OptionalAction = optionalAction;
		}
	}
}
