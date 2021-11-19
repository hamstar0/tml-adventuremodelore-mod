using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private (int x, int y)? FindDungeonExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			int dir = Main.dungeonX > ( Main.maxTilesX / 2 )
				? -1
				: 1;

			int tileX = Main.dungeonX;
			int tileY = Main.dungeonY;

			//

			// Scan until open air
			while( Framing.GetTileSafely(tileX, tileY).active() && Main.tile[tileX, tileY].type != TileID.Pots ) {
				tileY--;
			}
			while( !Framing.GetTileSafely(tileX, tileY).active() || Main.tile[tileX, tileY].type == TileID.Pots ) {
				tileY++;
			}
			tileY--;	// air tile

			//

			// Scan floor until far edge
			int offsetX;
			for( offsetX = 0; offsetX < 256; offsetX++ ) {
				// scan towards map center
				int x = tileX + (offsetX * dir);

				Tile tile = Framing.GetTileSafely( x, tileY+1 );
				if( !tile.active() ) {
					break;
				}
				if( tile.type != TileID.BlueDungeonBrick
						&& tile.type != TileID.GreenDungeonBrick
						&& tile.type != TileID.PinkDungeonBrick ) {
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
					tileY: tileY - 1,
					maxTileY: WorldLocationLibraries.DirtLayerTopTileY - 1,
					campWidth: campWidth,
					floorPavingDepth: 2,
					emptySpaceNeededAbove: 3,
					permitDungeonWalls: true,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos != null ) {
					return scanPos;
				}

				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: tileX + i,
					tileY: tileY - 1,
					maxTileY: WorldLocationLibraries.DirtLayerTopTileY - 1,
					campWidth: campWidth,
					floorPavingDepth: 2,
					emptySpaceNeededAbove: 3,
					permitDungeonWalls: true,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos != null ) {
					return scanPos;
				}
			}

			//

			mostCommonTileType = -1;
			return null;
		}
	}
}
