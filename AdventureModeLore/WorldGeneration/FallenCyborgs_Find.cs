using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FallenCyborgsGen : GenPass {
		private (int x, int y)? FindRandomLocationForACyborg() {
			int minTileY = WorldHelpers.DirtLayerTopTileY;
			int maxTileY = WorldHelpers.RockLayerBottomTileY;

			for( int i=0; i<2000; i++ ) {
				int tileX = WorldGen.genRand.Next( WorldHelpers.BeachWestTileX, WorldHelpers.BeachEastTileX );
				int tileY = WorldGen.genRand.Next( minTileY, maxTileY );

				(int, int)? scanPos = this.FindCyborgFutureFloorArea(
					tileX: tileX,
					tileY: tileY,
					maxTileY: maxTileY
				);
				if( scanPos != null ) {
					return scanPos;
				}
			}

			return null;
		}


		////////////////

		private (int x, int nearFloorY)? FindCyborgFutureFloorArea(
					int tileX,
					int tileY,
					int maxTileY ) {
			if( this.FindValidNearFloorTileSpaceAt(tileX, tileY, maxTileY, out tileY) ) {
				return (tileX, tileY);
			}

			return null;
		}
	}
}
