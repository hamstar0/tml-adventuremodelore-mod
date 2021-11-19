using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private (int x, int nearFloorY)? FindMiddleSurfaceExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int maxX = (Main.maxTilesX / 2) - 1;
			int minTileY = WorldLocationLibraries.SkyLayerBottomTileY + 1;
			int maxTileY = WorldLocationLibraries.RockLayerBottomTileY;
			int tileY = minTileY;

			//

			// Clear any islands
			for( ; tileY < maxTileY; tileY++ ) {
				if( !Framing.GetTileSafely(maxX, tileY).active() ) {
					break;
				}
			}

			//

			for( int i = 1; i < maxX; i++ ) {
				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: maxX + i,
					tileY: tileY,
					maxTileY: maxTileY,
					campWidth: campWidth,
					floorPavingDepth: 8,
					emptySpaceNeededAbove: 3,
					permitDungeonWalls: false,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos != null ) {
					return scanPos;
				}

				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: maxX - i,
					tileY: tileY,
					maxTileY: maxTileY,
					campWidth: campWidth,
					floorPavingDepth: 8,
					emptySpaceNeededAbove: 3,
					permitDungeonWalls: false,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}
	}
}
