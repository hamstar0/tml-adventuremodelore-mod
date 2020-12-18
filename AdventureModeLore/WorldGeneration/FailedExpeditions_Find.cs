using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private (int x, int nearFloorY)? FindMiddleSurfaceExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int max = Main.maxTilesX / 2;
			int tileY = WorldHelpers.SurfaceLayerTopTileY;

			for( int i = 0; i < max; i++ ) {
				int tileX = max + i;

				scanPos = this.ValidateExpeditionAt( tileX, tileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}

				tileX = max - i;
				scanPos = this.ValidateExpeditionAt( tileX, tileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}

		
		private (int x, int y)? FindJungleSideBeachExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int tileX = 1;
			int tileY = WorldHelpers.SurfaceLayerTopTileY;
			int dir = 0;

			if( Main.dungeonX > (Main.maxTilesX / 2) ) {
				tileX = 40;
				dir = 1;
			} else {
				tileX = Main.maxTilesX - 40;
				dir = -1;
			}

			for( int i=0; i<1000; i++ ) {
				int x = tileX + (i * dir);

				scanPos = this.ValidateExpeditionAt( x, tileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


		private (int x, int y)? FindRandomExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			for( int i=0; i<2000; i++ ) {
				int tileX = WorldGen.genRand.Next( WorldHelpers.BeachWestTileX, WorldHelpers.BeachEastTileX );
				int tileY = WorldGen.genRand.Next( WorldHelpers.DirtLayerTopTileY, WorldHelpers.RockLayerBottomTileY );

				(int, int)? scanPos = this.ValidateExpeditionAt( tileX, tileY, campWidth, out mostCommonTileType );
				if( scanPos != null ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


		////////////////

		private (int x, int nearFloorY)? ValidateExpeditionAt( int tileX, int tileY, int campWidth, out int mostCommonTileType ) {
			if( this.FindValidNearFloorTileAt(tileX, tileY, WorldHelpers.RockLayerBottomTileY, out tileY) ) {
				if( this.ScanFromTile(tileX, tileY, campWidth, out (int, int) scanPos, out mostCommonTileType) ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}
	}
}
