using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.General {
	public partial class GeneralLoreEvent : LoreEvent {
		private bool IsFinished = false;


		////////////////

		public Action Event { get; private set; }



		////////////////

		public GeneralLoreEvent(
					string name,
					Func<bool>[] prereqs,
					Action myevent,
					bool isRepeatable )
				: base( name, prereqs, isRepeatable ) {
			this.Event = myevent;
		}


		////

		internal override void Initialize() { }


		////////////////
		
		protected override bool HasEventFinished() {
			return this.IsFinished;
		}


		////////////////

		public override void BeginForLocalPlayer( bool isRepeat ) {
			this.Event.Invoke();

			this.IsFinished = true;
		}
	}
}
