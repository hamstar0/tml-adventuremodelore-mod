using System;
using Messages;
using ModLibsCore.Classes.Loadable;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			Scannables.LoadScannable_CursedBones();
			Scannables.LoadScannable_Dungeon();
			Scannables.LoadScannable_Gems();
			Scannables.LoadScannable_LostExpeditions();
			Scannables.LoadScannable_MagicItems();
			Scannables.LoadScannable_PBGvBrambles();
			Scannables.LoadScannable_Orbs();
			Scannables.LoadScannable_ShadowMirror();
			Scannables.LoadScannable_StrangePlants();
			Scannables.LoadScannable_StrongGates();
			Scannables.LoadScannable_UGDesert();
			Scannables.LoadScannable_Warn_Jungle();
			Scannables.LoadScannable_Warn_Underworld();
		}
	}
}
