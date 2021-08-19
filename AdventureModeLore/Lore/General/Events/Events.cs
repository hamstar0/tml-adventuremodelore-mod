using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		internal static IList<GeneralLoreEvent> GetDefinitions() {
			return new List<GeneralLoreEvent> {
				GeneralLoreEventDefinitions.GetEvent_Radio_Orbs(),
				GeneralLoreEventDefinitions.GetEvent_Radio_StrongGates()
			};
		}
	}
}
