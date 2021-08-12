using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureModeLore.WorldGeneration {
	partial class AbandonedExpeditionsGen : GenPass {
		private (int x, int nearFloorY)? FindMiddleSurfaceExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int maxX = (Main.maxTilesX / 2) - 1;
			int minTileY = WorldLocationLibraries.SkyLayerBottomTileY + 1;
			int maxTileY = WorldLocationLibraries.RockLayerBottomTileY;
			int tileY = minTileY;

			// Clear any islands
			for( ; tileY < maxTileY; tileY++ ) {
				if( !Framing.GetTileSafely(maxX, tileY).active() ) {
					break;
				}
			}

			for( int i = 1; i < maxX; i++ ) {
				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: maxX + i,
					tileY: tileY,
					maxTileY: maxTileY,
					campWidth: campWidth,
					floorPavingDepth: 8,
					emptySpaceNeededAbove: 3,
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
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}

		
		private (int x, int y)? FindJungleSideBeachExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int dir = 0;
			int tileX = 1;
			int minTileY = WorldLocationLibraries.SkyLayerBottomTileY + 1;
			int maxTileY = WorldLocationLibraries.DirtLayerTopTileY - 1;
			int tileY = minTileY;

			if( Main.dungeonX > (Main.maxTilesX / 2) ) {
				tileX = 40;
				dir = 1;
			} else {
				tileX = Main.maxTilesX - 40;
				dir = -1;
			}

			int maxX = Main.maxTilesX / 2;

			// Clear any islands
			for( ; tileY < maxTileY; tileY++ ) {
				if( !Framing.GetTileSafely( maxX, tileY ).active() ) {
					break;
				}
			}

			for( int i=0; i<maxX; i++ ) {
				int x = tileX + (i * dir);

				scanPos = this.FindExpeditionFutureFloorArea(
					tileX: x,
					tileY: tileY,
					maxTileY: maxTileY,
					campWidth: campWidth,
					floorPavingDepth: 8,
					emptySpaceNeededAbove: 3,
					mostCommonTileType: out mostCommonTileType
				);
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


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
					mostCommonTileType: out mostCommonTileType
				);

				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


		////////////////

		private (int tileX, int nearFloorTileY)? FindExpeditionFutureFloorArea(
					int tileX,
					int tileY,
					int maxTileY,
					int campWidth,
					int floorPavingDepth,
					int emptySpaceNeededAbove,
					out int mostCommonTileType ) {
			if( !this.FindValidNearFloorTileAt(tileX, tileY, maxTileY, emptySpaceNeededAbove, out tileY) ) {
				mostCommonTileType = -1;
				return null;
			}

			bool scan = this.ScanFromTileForCamp(
				tileX,
				tileY,
				campWidth,
				floorPavingDepth,
				emptySpaceNeededAbove,
				out (int, int) scanPos,
				out mostCommonTileType
			);

			if( !scan ) {
				mostCommonTileType = -1;
				return null;
			}

			return scanPos;
		}
	}
}
