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
		private static void LoadScannable_CursedBones() {
			if( ModLoader.GetMod( "CursedBones" ) == null ) {
				return;
			}

			Scannables.LoadScannable_CursedBones_WeakRef();
		}

		private static void LoadScannable_CursedBones_WeakRef() {
			string msgId = "Scannable_CursedBones";
			string msgTitle = "About Cursed Brambles";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"See those eerie patches of bones that like sending deadly magical skulls flying your way?"
				+" Those can only be removed with some strong mining tools... if you can safely get close"
				+" enough. I'd suggest avoiding them, for now. Use your P.B.G to protect against their"
				+" projectiles, if you can't."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"See those flickering, gross-looking piles of bones that seem to want to send deadly magical skulls your way?"
				+" Those can [c/88FF88:only be removed with some strong mining tools]... if you can safely get close enough. I'd"
				+" suggest avoiding them, for now. Use your [c/88FF88:P.B.G] to protect against their projectiles, if you can't."
			);*/

			//

			int cursedBonesType = ModContent.TileType<CursedBones.Tiles.CursedBonesTile>();

			bool canScan( int x, int y ) {
				return Scannables.FindTileNear( (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, cursedBonesType );
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
