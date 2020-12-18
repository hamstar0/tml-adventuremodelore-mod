using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private bool CreateExpeditionAt(
					int leftTileX,
					int nearFloorTileY,
					int campWidth,
					int paveTileType,
					bool rememberLocation,
					out int chestIdx,
					out string result ) {
			int toTileX = leftTileX + campWidth;

			for( int i = leftTileX; i < toTileX; i++ ) {
				this.PaveCampAt( leftTileX, nearFloorTileY, paveTileType );
			}

			// Tent
			int tentTileX = leftTileX + 1;

			if( !TilePlacementHelpers.TryPrecisePlace(tentTileX, nearFloorTileY, TileID.LargePiles2, 26) ) {
				chestIdx = -1;
				result = "Could not place tent at "+tentTileX+","+nearFloorTileY;
				return false;
			}

			// Campfire
			int fireTileX = leftTileX + 4;

			if( !TilePlacementHelpers.TryPrecisePlace(fireTileX, nearFloorTileY, TileID.Campfire) ) {
				chestIdx = -1;
				result = "Could not place campfire at "+fireTileX+","+nearFloorTileY;
				return false;
			}
			Main.tile[fireTileX, nearFloorTileY - 1].frameY += 36;//= 288;   // this is dumb
			Main.tile[fireTileX, nearFloorTileY].frameY += 36;//288 + 18;    // this is dumb
			Main.tile[fireTileX+1, nearFloorTileY - 1].frameY += 36;//288;     // this is dumb
			Main.tile[fireTileX+1, nearFloorTileY].frameY += 36;//288 + 18;    // this is dumb

			// Barrel
			int chestTileX = leftTileX + 8;

			chestIdx = WorldGen.PlaceChest( chestTileX, nearFloorTileY, TileID.Containers, false, 5 );
			if( chestIdx == -1 ) {
				result = "Could not place barrel at "+chestTileX+","+nearFloorTileY;
				return false;
			}

			if( rememberLocation ) {
				var myworld = ModContent.GetInstance<AMLWorld>();
				myworld.FailedExpeditions.Add( (chestTileX, nearFloorTileY) );
			}

			result = "Success.";
			return true;
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
