using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		internal static IList<GeneralLoreEvent> GetDefinitions() {
			return new List<GeneralLoreEvent> {
				GeneralLoreEventDefinitions.GetEvent_Radio_Orbs(),
				GeneralLoreEventDefinitions.GetEvent_Radio_StrongGates(),
				GeneralLoreEventDefinitions.GetEvent_Radio_CursedBones(),
				GeneralLoreEventDefinitions.GetEvent_Radio_ShadowMirror(),
				GeneralLoreEventDefinitions.GetEvent_Radio_PKE(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Settlements(),
				GeneralLoreEventDefinitions.GetEvent_Radio_RedBar(),
				GeneralLoreEventDefinitions.GetEvent_Radio_UGDesert(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Dungeon(),
				GeneralLoreEventDefinitions.GetEvent_Radio_ManaShardHints1(),
				GeneralLoreEventDefinitions.GetEvent_Radio_ManaShardHints2(),
				GeneralLoreEventDefinitions.GetEvent_Radio_MagicSecrets(),
				GeneralLoreEventDefinitions.GetEvent_Radio_AnimaEmpty(),
				GeneralLoreEventDefinitions.GetEvent_Radio_LostExpeditions()
			};
		}
	}
}
