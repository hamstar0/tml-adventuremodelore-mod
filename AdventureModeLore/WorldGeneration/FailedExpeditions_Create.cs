using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private int CreateExpeditionAt( int tileX, int tileY, int campWidth, int paveTileType, bool rememberLocation ) {
			int toTileX = tileX + campWidth;

			for( int i = tileX; i < toTileX; i++ ) {
				this.PaveCampAt( tileX, tileY, paveTileType );
			}

			// Tent
			TilePlacementHelpers.TryPrecisePlace( tileX + 1, tileY, TileID.LargePiles2, 26 );

			// Campfire
			TilePlacementHelpers.TryPrecisePlace( tileX + 5, tileY, TileID.Campfire );
			Main.tile[tileX + 5, tileY - 1].frameY = 288;       // this is dumb
			Main.tile[tileX + 5, tileY].frameY = 288 + 18;    // this is dumb
			Main.tile[tileX + 6, tileY - 1].frameY = 288;     // this is dumb
			Main.tile[tileX + 6, tileY].frameY = 288 + 18;    // this is dumb

			int chestTileX = tileX + 9;

			if( rememberLocation ) {
				var myworld = ModContent.GetInstance<AMLWorld>();
				myworld.FailedExpeditions.Add( (tileX, tileY) );
			}

			// Barrel
			return WorldGen.PlaceChest( chestTileX, tileY, TileID.Containers, false, 5 );
		}

		////

		private void PaveCampAt( int tileX, int tileY, int tileType ) {
			var needsFill = new List<(int, int)>();
			Tile tile = Main.tile[tileX, tileY];
			int depth = 0;

			while( tile?.active() != true || !Main.tileSolid[tile.type] ) {
				if( tile?.active() == true ) {
					WorldGen.KillTile( tileX, tileY );
				}

				needsFill.Add( (tileX, tileY) );

				tile = Main.tile[tileX, ++tileY];

				if( depth++ >= 6 ) {
					break;
				}
			}

			Main.tile[tileX, tileY]?.slope( 0 );

			for( int i=needsFill.Count-1; i>=0; i-- ) {
				(int x, int y) fillAt = needsFill[i];

				WorldGen.PlaceTile( fillAt.x, fillAt.y, tileType );
			}
		}
	}
}
