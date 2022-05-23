using System;
using Messages;
using ModLibsCore.Classes.Loadable;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			Scannables.LoadScannable_CursedBones_If();
			Scannables.LoadScannable_Dungeon_If();
			Scannables.LoadScannable_Gems_If();
			Scannables.LoadScannable_JungleWater_If();
			Scannables.LoadScannable_LostExpeditions_If();
			Scannables.LoadScannable_MagicItems_If();
			Scannables.LoadScannable_PBGvBrambles_If();
			Scannables.LoadScannable_Orbs_If();
			Scannables.LoadScannable_Scaffolds_If();
			Scannables.LoadScannable_ShadowMirror();
			Scannables.LoadScannable_StrangePlants_If();
			Scannables.LoadScannable_StrongGates_If();
			Scannables.LoadScannable_UGDesert_If();
			Scannables.LoadScannable_Warn_Jungle_If();
			Scannables.LoadScannable_Warn_Underworld_If();
		}
	}
}
