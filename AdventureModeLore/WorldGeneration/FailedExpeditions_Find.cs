using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private (int x, int nearFloorY)? FindMiddleSurfaceExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int max = (Main.maxTilesX / 2) - 1;
			int tileY = WorldHelpers.SurfaceLayerTopTileY;
			int maxTileY = WorldHelpers.RockLayerBottomTileY;

			for( int i = 0; i < max; i++ ) {
				int tileX = max + i;

				scanPos = this.FindExpeditionFutureFloorArea( tileX, tileY, maxTileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}

				tileX = max - i;
				scanPos = this.FindExpeditionFutureFloorArea( tileX, tileY, maxTileY, campWidth, out mostCommonTileType );
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
			int minTileY = WorldHelpers.SkyLayerBottomTileY + 1;
			int maxTileY = WorldHelpers.DirtLayerTopTileY - 1;

			if( Main.dungeonX > (Main.maxTilesX / 2) ) {
				tileX = 40;
				dir = 1;
			} else {
				tileX = Main.maxTilesX - 40;
				dir = -1;
			}

			for( int i=0; i<1000; i++ ) {
				int x = tileX + (i * dir);

				scanPos = this.FindExpeditionFutureFloorArea( x, minTileY, maxTileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


		private (int x, int y)? FindRandomExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			int maxTileY = WorldHelpers.RockLayerBottomTileY;

			for( int i=0; i<2000; i++ ) {
				int tileX = WorldGen.genRand.Next( WorldHelpers.BeachWestTileX, WorldHelpers.BeachEastTileX );
				int tileY = WorldGen.genRand.Next( WorldHelpers.DirtLayerTopTileY, maxTileY );

				(int, int)? scanPos = this.FindExpeditionFutureFloorArea( tileX, tileY, maxTileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


		////////////////

		private (int x, int nearFloorY)? FindExpeditionFutureFloorArea(
					int tileX,
					int tileY,
					int maxTileY,
					int campWidth,
					out int mostCommonTileType ) {
			if( this.FindValidNearFloorTileAt(tileX, tileY, maxTileY, out tileY) ) {
				if( this.ScanFromTile(tileX, tileY, campWidth, out (int, int) scanPos, out mostCommonTileType) ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}
	}
}
