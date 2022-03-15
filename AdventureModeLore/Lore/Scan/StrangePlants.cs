using System;
using System.Collections.Generic;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_StrangePlants() {
			string msgId = "Scannable_StrangePlants";
			string msgTitle = "About the Strange Plants";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Certain strange plants seem to have inherited some metaphysical properties from their environment. It is"
				+ " rare to see such a completely passive manifestation of spiritual energy, but I guess it's statistically"
				+ " plausible, considering all the other types of manifestation we've seen. In any case, try to harvest these"
				+ " when you can. They're just brimming with hidden powers!"
			);

			int[] anyOfItemTypes = new int[] {
				ItemID.StrangePlant1,
				ItemID.StrangePlant2,
				ItemID.StrangePlant3,
				ItemID.StrangePlant4,
			};

			//

			var scannable = new PKEScannable(
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg ),
				anyOfItemTypes: anyOfItemTypes
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
