using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using System.Collections.Generic;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private void CreateAllUndergroundFEs( GenerationProgress progress, int count, int campWidth ) {
			var expeds = new List<(int x, int nearFloorY, int paveTileType)>();

			int attempts = 0;
			for( int expedNum = 1; expedNum < count; expedNum++ ) {
				(int x, int nearFloorY)? expedPointRaw = this.FindRandomExpeditionLocation( campWidth, out int paveTileType );
				if( !expedPointRaw.HasValue ) {
					LogHelpers.Log( "Could not finish finding places to generate all 'failed expeditions' ("+expedNum+" of "+count+")" );
					break;
				}

				if( this.IsNearAnotherExpedition(expeds, expedPointRaw.Value) ) {
					if( attempts++ > 1000 ) {
						LogHelpers.Log( "Could not finish finding open places to generate all 'failed expeditions' ("+expedNum+" of "+count+")" );
						break;
					}

					expedNum--;
					continue;
				} else {
					attempts = 0;
				}

				expeds.Add( (expedPointRaw.Value.x, expedPointRaw.Value.nearFloorY, paveTileType) );
			}

			int i = 0;
			foreach( (int x, int nearFloorY, int paveTileType) in expeds ) {
				if( !this.CreateUndergroundFE( x, nearFloorY, paveTileType, campWidth, out string result ) ) {
					LogHelpers.Log( "Could not finish generating all 'failed expeditions' (" + i + " of " + expeds.Count + "): " + result );
					break;
				}

				i++;
				progress.Value = (float)i / ( (float)expeds.Count + 2f );
			}
		}


		////

		private bool CreateUndergroundFE( int x, int nearFloorY, int paveTileType, int campWidth, out string result ) {
			int chestIdx;

			bool createdCamp = this.CreateExpeditionAt(
				leftTileX: x,
				nearFloorTileY: nearFloorY,
				campWidth: campWidth,
				customTiles: new int[0],
				paveTileType: paveTileType,
				rememberLocation: true,
				chestIdx: out chestIdx,
				result: out result
			);
			if( !createdCamp ) {
				return false;
			}

			this.FillExpeditionBarrel(
				tileX: x,
				nearFloorTileY: nearFloorY,
				chestIdx: chestIdx,
				hasLoreNote: true,
				speedloaderCount: WorldGen.genRand.NextFloat() < ( 2f / 3f ) ? 1 : 0,
				orbCount: WorldGen.genRand.Next( 1, 4 ),
				canopicJarCount: WorldGen.genRand.Next( 1, 3 ),
				hasPKEMeter: false
			);

			result = "Success.";
			return true;
		}


		////

		private bool IsNearAnotherExpedition( IList<(int x, int y, int paveTileType)> expeds, (int x, int y) proposed ) {
			int minDist = 128 * 16;
			int minDistSqr = minDist * minDist;

			foreach( (int x, int y, int _) in expeds ) {
				int xDiff = proposed.x - x;
				int yDiff = proposed.y - y;

				if( ((xDiff * xDiff) + (yDiff * yDiff)) < minDistSqr ) {
					return false;
				}
			}
			return true;
		}
	}
}
