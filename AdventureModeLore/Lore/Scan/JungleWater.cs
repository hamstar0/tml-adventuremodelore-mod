﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Messages;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_JungleWater_If() {
			if( ModLoader.GetMod("GreenHell") == null ) {
				return;
			}

			Scannables.LoadScannable_JungleWater_WeakRef_If();
		}

		private static void LoadScannable_JungleWater_WeakRef_If() {
			string msgId = "Scannable_JungleWater";
			string msgTitle = "About Jungle Water";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Beware! That water has parasites in it. Without protection, you might contract something nasty."
				+" Enter if you dare."
			);

			//
			
			if( !MessagesAPI.IsUnread(msgId) ) {
				return;
			}

			//

			bool CanScan( int scrX, int scrY ) {
				if( !Main.LocalPlayer.ZoneJungle ) {
					return false;
				}
				return Scannables.FindTileNear(
					(int)Main.MouseWorld.X / 16,
					(int)Main.MouseWorld.Y / 16,
					tile => tile.liquid > 0 && !tile.lava() && !tile.honey()
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
