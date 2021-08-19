using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions : ILoadable {
		internal IList<GeneralLoreEvent> Defs;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			this.Defs = new List<GeneralLoreEvent> {
				this.GetRadioOrbsEvent()
			};
		}
	}
}
