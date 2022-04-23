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
		private static void LoadScannable_JungleWater() {
			if( ModLoader.GetMod("GreenHell") == null ) {
				return;
			}

			Scannables.LoadScannable_JungleWater_WeakRef();
		}

		private static void LoadScannable_JungleWater_WeakRef() {
			int cursedBonesType = ModContent.TileType<CursedBones.Tiles.CursedBonesTile>();

			bool CanScan( int x, int y ) {
				return Scannables.FindTileNear(
					(int)Main.MouseWorld.X,
					(int)Main.MouseWorld.Y,
					tile => Main.LocalPlayer.ZoneJungle && tile.liquid > 0 && !tile.lava() && !tile.honey()
				);
			}

			//

			string msgId = "Scannable_JungleWater";
			string msgTitle = "About Jungle Water";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Beware! That water has parasites in it. Without protection, you might contract something nasty."
				+" Enter if you dare."
			);

			//

			var scannable = new PKEScannable(
				canScan: CanScan,
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg )
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
