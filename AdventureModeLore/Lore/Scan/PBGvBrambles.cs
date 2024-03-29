﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using Messages;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_PBGvBrambles_If() {
			if( ModLoader.GetMod("SoulBarriers") == null ) {
				return;
			}
			if( ModLoader.GetMod("PKEMeter") == null ) {
				return;
			}
			if( ModLoader.GetMod("CursedBrambles") == null ) {
				return;
			}

			//

			Scannables.LoadScannable_PBGvBrambles_WeakRef_If();
		}

		private static void LoadScannable_PBGvBrambles_WeakRef_If() {
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

			if( !MessagesAPI.IsUnread(msgId) ) {
				return;
			}

			//

			int brambleType = ModContent.TileType<CursedBrambles.Tiles.CursedBrambleTile>();

			bool CanScan( int scrX, int scrY ) {
				return Scannables.FindTileNear(
					(int)Main.MouseWorld.X / 16,
					(int)Main.MouseWorld.Y / 16,
					tile => tile.type == brambleType
				);
			}

			//

			var scannable = new PKEScannable(
				canScan: CanScan,
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg )
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
