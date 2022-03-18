using System;
using System.Collections.Generic;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_Gems() {
			string msgId = "Scannable_Gems";
			string msgTitle = "About Gems";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Gem stones in these lands often associate with magical power. Since our goal here may involve "
				+"learning about magic, using these to procure magic items is a priority. Seems the best way"
				+"to understand magic is with magic."
				+"\n \n"
				+"Be sure to bring me gems to see what to craft from them."
			);

			int[] anyOfItemTypes = new int[] {
				ItemID.Amethyst,
				ItemID.Topaz,
				ItemID.Sapphire,
				ItemID.Emerald,
				ItemID.Ruby,
				ItemID.Diamond,
				ItemID.Amber,
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
