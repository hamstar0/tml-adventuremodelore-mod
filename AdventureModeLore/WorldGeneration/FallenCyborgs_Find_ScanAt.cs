using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles.Walls;


namespace AdventureModeLore.WorldGeneration {
	partial class FallenCyborgsGen : GenPass {
		private bool FindValidNearFloorTileSpaceAt( int tileX, int topTileY, int botTileY, out int nearFloorY ) {
			if( !this.FindValidNearFloorTileAt(tileX, topTileY, botTileY, out nearFloorY) ) {
				return false;
			}

			int nextNearFloor;
			if( !this.FindValidNearFloorTileAt(tileX, nearFloorY, nearFloorY+2, out nextNearFloor) ) {
				return false;
			}

			return nextNearFloor == nearFloorY;
		}


		private bool FindValidNearFloorTileAt( int tileX, int topTileY, int botTileY, out int nearFloorY ) {
			if( !WorldGen.InWorld(tileX, topTileY) ) {
				nearFloorY = topTileY;
				return false;
			}

			int findFloorY = topTileY;

			// Find floor
			bool hasEmptySpace = false;
			for( Tile tile = Main.tile[ tileX, findFloorY ];
						findFloorY < botTileY && !tile.active();
						tile = Main.tile[ tileX, ++findFloorY ] ) {
				hasEmptySpace = true;
			}
			
			nearFloorY = findFloorY - 1;

			// Confirm floor
			Tile floorTile = Main.tile[ tileX, findFloorY ];
			if( !hasEmptySpace || !this.IsValidFloorTile(floorTile) ) {
				return false;
			}

			// Verify if valid "empty" space above floor
			for( int i=0; i<2; i++ ) {
				if( (nearFloorY - i) < 0 ) {
					return false;
				}

				Tile tile = Main.tile[ tileX, nearFloorY - i ];
				if( tile?.active() == false || TileWallGroupIdentityHelpers.IsDungeon(tile, out bool _) ) {
					return false;
				}
			}

			return true;
		}


		////////////////

		private bool IsValidFloorTile( Tile mytile ) {
			return WorldGen.SolidTile3( mytile )
				&& mytile.type != TileID.BreakableIce
				&& mytile.type != TileID.LeafBlock;
		}
	}
}
