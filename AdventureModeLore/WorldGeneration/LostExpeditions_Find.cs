using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private (int tileX, int nearFloorTileY)? FindExpeditionFutureFloorArea(
					int tileX,
					int tileY,
					int maxTileY,
					int campWidth,
					int floorPavingDepth,
					int emptySpaceNeededAbove,
					bool permitDungeonWalls,
					out int mostCommonTileType ) {
			//int oldTileY = tileY;
			bool isValidFloor = this.FindValidNearFloorTileAt(
				tileX: tileX,
				topTileY: tileY,
				botTileY: maxTileY,
				neededEmptySpaceAbove: emptySpaceNeededAbove,
				permitDungeonWalls: permitDungeonWalls,
				nearFloorTileY: out tileY
			);
			if( !isValidFloor ) {
//LogLibraries.Log( "1 "+oldTileY+", "+tileY );
				mostCommonTileType = -1;
				return null;
			}

			bool scan = this.ScanFromTileForCamp(
				tileX: tileX,
				nearCampFloortileY: tileY,
				campWidth: campWidth,
				floorPavingThickness: floorPavingDepth,
				neededEmptySpaceAbove: emptySpaceNeededAbove,
				permitDungeonWalls: permitDungeonWalls,
				campStartPos: out (int, int) scanPos,
				mostCommonTileType: out mostCommonTileType
			);

			if( !scan ) {
				mostCommonTileType = -1;
				return null;
			}

			return scanPos;
		}
	}
}
