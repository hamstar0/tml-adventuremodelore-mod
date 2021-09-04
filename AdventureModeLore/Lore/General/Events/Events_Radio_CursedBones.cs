using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_CursedBones_PreReq( int tileRange ) {
			int cursedBonesType = ModContent.TileType<CursedBones.Tiles.CursedBonesTile>();

			int minX = (int)Main.LocalPlayer.MountedCenter.X / 16;
			minX -= tileRange;
			if( minX < 0 ) { minX = 0; }

			int minY = (int)Main.LocalPlayer.MountedCenter.Y / 16;
			minY -= tileRange;
			if( minY < 0 ) { minY = 0; }

			int maxX = minX + ( tileRange * 2 );
			if( maxX >= Main.maxTilesX ) { maxX = Main.maxTilesX - 1; }

			int maxY = minY + ( tileRange * 2 );
			if( maxY >= Main.maxTilesY ) { maxY = Main.maxTilesY - 1; }

			for( int tileX = minX; tileX < maxX; tileX++ ) {
				for( int tileY = minY; tileY < maxY; tileY++ ) {
					Tile tile = Main.tile[tileX, tileY];
					if( tile?.active() == true && tile.type == cursedBonesType ) {
						return true;
					}
				}
			}

			return false;
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_CursedBones() {
			int tileRange = 12;

			bool PreReq() {
				return ModLoader.GetMod( "CursedBones" ) == null
					? false
					: GeneralLoreEventDefinitions.Event_Radio_CursedBones_PreReq( tileRange );
			}

			//

			string msgId = "AML_Radio_CursedBones";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"See those flickering, gross-looking piles of bones that seem to want"
				+" to send deadly magical skulls your way? Those can only be removed with some good"
				+" strong mining tools... if you can safely get close enough. I'd suggest avoiding them,"
				+" for now. Use your P.B.G to protect against their projectiles, if you can't."
			);

			return new GeneralLoreEvent(
				name: "Radio - Cursed Bones",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About cursed bones patches",
						description: msg,
						modOfOrigin: AMLMod.Instance,
						alertPlayer: MessagesAPI.IsUnread(msgId),
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: msgId
					);
				},
				isRepeatable: false
			);
		}
	}
}
