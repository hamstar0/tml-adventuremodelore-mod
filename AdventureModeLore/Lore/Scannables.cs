﻿using System;
using Messages;
using ModLibsCore.Classes.Loadable;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			Scannables.LoadScannable_PBGvBrambles();
			Scannables.LoadScannable_Orbs();
			Scannables.LoadScannable_ShadowMirror();
			Scannables.LoadScannable_StrangePlants();
			Scannables.LoadScannable_StrongGates();
			Scannables.LoadScannable_UGDesert();
		}
	}
}
