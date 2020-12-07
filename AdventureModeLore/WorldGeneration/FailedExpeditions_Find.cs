using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private (int x, int y)? FindMiddleSurfaceExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			(int, int)? scanPos;
			int max = Main.maxTilesX / 2;

			for( int i = 0; i < max; i++ ) {
				int tileX = max + i;
				int tileY = WorldHelpers.SurfaceLayerTopTileY;

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

		private (int x, int y)? ValidateExpeditionAt( int tileX, int tileY, int campWidth, out int mostCommonTileType ) {
			if( this.IsTileValid(tileX, tileY, WorldHelpers.RockLayerBottomTileY, out tileY) ) {
				if( this.ScanFromTile(tileX, tileY, campWidth, out (int, int) scanPos, out mostCommonTileType) ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}
	}
}
