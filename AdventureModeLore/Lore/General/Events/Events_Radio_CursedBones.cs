using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_CursedBones_PreReq( int range ) {
			int cursedBonesType = ModContent.TileType<CursedBones.Tiles.CursedBonesTile>();

			int minX = (int)Main.LocalPlayer.Center.X / 16;
			minX -= range;
			if( minX < 0 ) { minX = 0; }

			int minY = (int)Main.LocalPlayer.Center.Y / 16;
			minY -= range;
			if( minY < 0 ) { minY = 0; }

			int maxX = minX + ( range * 2 );
			if( maxX >= Main.maxTilesX ) { maxX = Main.maxTilesX - 1; }

			int maxY = minY + ( range * 2 );
			if( maxY >= Main.maxTilesY ) { maxY = Main.maxTilesY - 1; }

			for( int tileX = minX; tileX < maxX; tileX++ ) {
				for( int tileY = minY; tileY < maxY; tileY++ ) {
					Tile tile = Main.tile[tileX, tileY];
					if( tile?.active() != true ) { continue; }

					if( tile.type == cursedBonesType ) {
						return true;
					}
				}
			}
			return false;
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_CursedBones() {
			int range = 8;

			bool PreReq() {
				if( ModLoader.GetMod("CursedBones") == null ) {
					return false;
				}
				return GeneralLoreEventDefinitions.Event_Radio_CursedBones_PreReq( range );
			}

			//

			return new GeneralLoreEvent(
				name: "Radio - Cursed Bones",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About cursed bones patches",
						description: "Guide: See those flickering, gross-looking piles of bones that seem to want"
							+" to send deadly magical skulls flying your way? You'll need some good mining tools"
							+" if you want to clear them out. Otherwise, I'd suggest avoiding them, for now. Use"
							+" your P.B.G to protect against their projectiles, if you need to get close.",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_CursedBones"
					);
				},
				isRepeatable: false
			);
		}
	}
}
