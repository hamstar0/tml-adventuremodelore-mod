using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_UGDesert() {
			string msgId = "Scannable_UGDesert";
			string msgTitle = "About the Underground Desert";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I've just gotten a report that neutrally-aligned goblin folk may be found somewhere in your current"
				+" vicinity. What an unusual place to find any civilized being... even one such as a goblin."
				+"\n \n"
				+"Anyway, if you want to gain access to the depths of this desert, you'll need some tools. If you have a supply"
				+" of orbs and bombs, you can use these to craft a special explosive designed for destroying"
				+" hardened sand. There are variations of these explosives available for other uses, also. Don't"
				+" squander your orbs, though."
			);

			bool canScan( int x, int y ) {
				int mouseTileX = (int)Main.MouseWorld.X / 16;
				int mouseTileY = (int)Main.MouseWorld.Y / 16;

				return WorldGates.WorldGatesAPI.GetGateBarriers()
					.Where( b => b.Strength > Main.LocalPlayer.statManaMax2 )
					.Any( b => b.TileArea.Contains(mouseTileX, mouseTileY) );
			}

			//

			var scannable = new PKEScannable(
				canScan: canScan,
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg )
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
