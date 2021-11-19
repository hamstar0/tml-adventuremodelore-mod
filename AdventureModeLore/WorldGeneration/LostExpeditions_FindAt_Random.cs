using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private (int x, int y)? FindRandomExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			int maxTileY = WorldLocationLibraries.RockLayerBottomTileY;

			for( int i=0; i<2000; i++ ) {
				int tileX = WorldGen.genRand.Next( WorldLocationLibraries.BeachWestTileX, WorldLocationLibraries.BeachEastTileX );
				int tileY = WorldGen.genRand.Next( WorldLocationLibraries.DirtLayerTopTileY, maxTileY );

				(int, int)? scanPos = this.FindExpeditionFutureFloorArea(
					tileX: tileX,
					tileY: tileY,
					maxTileY: maxTileY,
					campWidth: campWidth,
					floorPavingDepth: 6,
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
