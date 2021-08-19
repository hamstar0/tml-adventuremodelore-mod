using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		internal static IList<DialogueLoreEvent> GetDefinitions() {
			return new List<DialogueLoreEvent> {
				DialogueLoreEventDefinitions.GetEvent_Dialogue00_Guide(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue01_OldMan(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue02_Merchant(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue03a_200hp(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue03b_BgPKE(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue03c_RescueGoblin(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue04_DefeatEvil(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue05_FindMechanicAndWitchDoctor(),
				DialogueLoreEventDefinitions.GetEvent_Dialogue06_SummonWoF()
			};
		}
	}
}
