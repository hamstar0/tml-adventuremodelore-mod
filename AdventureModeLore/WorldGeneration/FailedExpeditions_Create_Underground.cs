using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class AbandonedExpeditionsGen : GenPass {
		private void CreateAllUndergroundFEs( GenerationProgress progress, int count, int campWidth ) {
			var expedList = new List<(int tileX, int nearFloorTileY, int paveTileType)>();

			int retries = 0;
			for( int expedNum = 1; expedNum < count; expedNum++ ) {
				(int tileX, int nearFloorTileY)? expedPointRaw = this.FindRandomExpeditionLocation(
					campWidth,
					out int paveTileType
				);
				if( !expedPointRaw.HasValue ) {
					LogLibraries.Log( "Could not finish finding places to generate all 'abandoned expeditions' (" + expedNum+" of "+count+")" );
					break;
				}

				if( this.IsNearAnotherExpedition(expedList, expedPointRaw.Value) ) {
					if( retries++ > 1000 ) {
						LogLibraries.Log( "Could not finish finding open places to generate all 'abandoned expeditions' (" + expedNum+" of "+count+")" );
						break;
					}

					expedNum--;
					continue;
				}

				retries = 0;

				expedList.Add( (expedPointRaw.Value.tileX, expedPointRaw.Value.nearFloorTileY, paveTileType) );
			}

			int i = 0;
			foreach( (int tileX, int nearFloorTileY, int paveTileType) in expedList ) {
				if( !this.CreateUndergroundFE( tileX, nearFloorTileY, paveTileType, campWidth, out string result ) ) {
					LogLibraries.Log( "Could not finish generating all 'abandoned expeditions' (" + i + " of " + expedList.Count + "): " + result );
					break;
				}

				i++;
				progress.Value = (float)i / ( (float)expedList.Count + 2f );
			}
		}


		////

		private bool CreateUndergroundFE( int tileX, int nearFloorTileY, int paveTileType, int campWidth, out string result ) {
			int chestIdx;

			bool createdCamp = this.CreateExpeditionAt(
				leftTileX: tileX,
				nearFloorTileY: nearFloorTileY,
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
				tileX: tileX,
				nearFloorTileY: nearFloorTileY,
				chestIdx: chestIdx,
				hasLoreNote: true,
				speedloaderCount: WorldGen.genRand.NextFloat() < ( 2f / 3f ) ? 1 : 0,
				orbCount: WorldGen.genRand.Next( 1, 4 ),
				canopicJarCount: WorldGen.genRand.Next( 1, 3 ),
				hasPKEMeter: false,
				hasShadowMirror: WorldGen.genRand.NextBool()
			);

			result = "Success.";
			return true;
		}


		////

		private bool IsNearAnotherExpedition(
					IList<(int tileX, int tileY, int paveTileType)> expeds,
					(int tileX, int tileY) proposed ) {
			var config = AMLConfig.Instance;
			int minDist = config.Get<int>( nameof(config.MinimumTileDistanceBetweenAbandonedExpeditions) );
			int minDistSqr = minDist * minDist;

			foreach( (int tileX, int tileY, int _) in expeds ) {
				int xDiff = proposed.tileX - tileX;
				int yDiff = proposed.tileY - tileY;
				int distSqr = (xDiff * xDiff) + (yDiff * yDiff);

				if( distSqr < minDistSqr ) {
					return true;
				}
			}
			return false;
		}
	}
}
