using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_LostExpeditions() {
			int brambleType = ModContent.TileType<CursedBrambles.Tiles.CursedBrambleTile>();

			bool CanScan( int x, int y ) {
				PKEGaugeValues gauge = PKEMeterAPI.GetGauge()?
					.Invoke( Main.LocalPlayer, Main.LocalPlayer.MountedCenter );
				return gauge != null
					? gauge.GreenPercent >= 0.85f
					: false;
			}

			//

			string msgId = "Scannable_LostExpeditions";
			string msgTitle = "About Lost Expeditions";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"According to your PKE meter, you're getting close to something. Observe your green meter, and"
				+" follow it's signal. If my hunch is correct, you may find something important to your mission!"
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"According to your PKE meter, you're getting close to something. [c/88FF88:Observe your green meter, and follow"
				+" it's signal]. If my hunch is correct, you may find something important to your mission!"
			);*/

			//

			//

			var scannable = new PKEScannable(
				canScan: CanScan,
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg )
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
