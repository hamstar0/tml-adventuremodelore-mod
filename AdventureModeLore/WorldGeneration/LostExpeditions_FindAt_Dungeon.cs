using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private (int TileX, int NearFloorTileY)? FindDungeonExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			int dir = Main.dungeonX > ( Main.maxTilesX / 2 )
				? -1
				: 1;

			int tileX = Main.dungeonX;
			int tileY = Main.dungeonY;
			int nearFloorY = tileY;

			//

			// Scan until open air
			while( Main.tile[tileX, nearFloorY].active() && Main.tile[tileX, nearFloorY].type != TileID.Pots ) {
				nearFloorY--;	// go up
			}
			while( !Main.tile[tileX, nearFloorY].active() || Main.tile[tileX, nearFloorY].type == TileID.Pots ) {
				nearFloorY++;	// go down
			}
			nearFloorY--;	// air tile

			//

			// Scan floor until far edge
			int offsetX;
			for( offsetX = 0; offsetX < 256; offsetX++ ) {
				// scan towards map center
				int x = tileX + (offsetX * dir);

				Tile floorTile = Framing.GetTileSafely( x, nearFloorY+1 );
				if( !floorTile.active() ) {
					break;
				}
				if( floorTile.type != TileID.BlueDungeonBrick
						&& floorTile.type != TileID.GreenDungeonBrick
						&& floorTile.type != TileID.PinkDungeonBrick ) {
					break;
				}
			}
			if( offsetX >= 256 ) {
				mostCommonTileType = -1;
				return null;
			}

			// sub-far edge tile
			tileX += (offsetX - 1) * dir;

			//

			(int, int)? scanPos;

			for( int i = 1; i < 40; i++ ) {
				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: tileX - i,
					tileY: nearFloorY,
					maxTileY: WorldLocationLibraries.DirtLayerTopTileY - 1,
					campWidth: campWidth,
					floorPavingDepth: 2,
					emptySpaceNeededAbove: 3,
					permitDungeonWalls: true,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos.HasValue ) {
					return scanPos;
				}

				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: tileX + i,
					tileY: nearFloorY,
					maxTileY: WorldLocationLibraries.DirtLayerTopTileY - 1,
					campWidth: campWidth,
					floorPavingDepth: 2,
					emptySpaceNeededAbove: 3,
					permitDungeonWalls: true,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos.HasValue ) {
					return scanPos;
				}
			}

			//

			mostCommonTileType = -1;
			return null;
		}
	}
}
