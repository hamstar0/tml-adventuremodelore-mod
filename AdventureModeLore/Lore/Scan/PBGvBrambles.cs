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
		private static void LoadScannable_PBGvBrambles() {
			if( ModLoader.GetMod("SoulBarriers") == null ) {
				return;
			}
			if( ModLoader.GetMod("PKEMeter") == null ) {
				return;
			}
			if( ModLoader.GetMod("CursedBrambles") == null ) {
				return;
			}

			Scannables.LoadScannable_PBGvBrambles_WeakRef();
		}

		private static void LoadScannable_PBGvBrambles_WeakRef() {
			string msgId = "Scannable_PBGvBrambles";
			string msgTitle = "About Cursed Brambles";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Curse those brambles! I guess you can now see where they get their name. Use your P.B.G to"
				+" clear them out of your way, like most other metaphysical threats. Your pickaxe may also work,"
				+" albeit slowly."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Curse those brambles! I guess you can now see where they get their name. Use your [c/88FF88:P.B.G] to clear them"
				+" out of your way, like most other metaphysical threats."
			);*/

			//

			int brambleType = ModContent.TileType<CursedBrambles.Tiles.CursedBrambleTile>();

			bool canScan( int x, int y ) {
				return Scannables.FindTileNear( (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, brambleType );
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
