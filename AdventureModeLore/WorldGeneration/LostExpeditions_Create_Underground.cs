using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private void CreateAllUndergroundLEs( GenerationProgress progress, int count, int campWidth ) {
			var expedList = new List<(int tileX, int nearFloorTileY, int paveTileType)>();

			int retries = 0;
			for( int expedNum = 1; expedNum < count; expedNum++ ) {
				(int tileX, int nearFloorTileY)? expedPointRaw = this.FindRandomExpeditionLocation(
					campWidth,
					out int paveTileType
				);
				if( !expedPointRaw.HasValue ) {
					LogLibraries.Log( "Could not finish finding places to generate all 'lost expeditions' (" + expedNum+" of "+count+")" );
					break;
				}

				if( this.IsNearAnotherExpedition(expedList, expedPointRaw.Value) ) {
					if( retries++ > 1000 ) {
						LogLibraries.Log( "Could not finish finding open places to generate all 'lost expeditions' (" + expedNum+" of "+count+")" );
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
				if( !this.CreateUndergroundLE( tileX, nearFloorTileY, paveTileType, campWidth, out string result ) ) {
					LogLibraries.Log( "Could not finish generating all 'lost expeditions' (" + i + " of " + expedList.Count + "): " + result );
					break;
				}

				i++;
				progress.Value = (float)i / ( (float)expedList.Count + 2f );
			}
		}


		////

		private bool CreateUndergroundLE( int tileX, int nearFloorTileY, int paveTileType, int campWidth, out string result ) {
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
				speedloaderCount: WorldGen.genRand.NextFloat() < (2f / 3f) ? 1 : 0,
				randomOrbCount: 0,
				whiteOrbCount: WorldGen.genRand.Next( 1, 4 ),
				canopicJarCount: WorldGen.genRand.Next( 1, 3 ),
				elixirCount: WorldGen.genRand.Next( -1, 2 ),
				mountedMirrorsCount: WorldGen.genRand.Next( -1, 2 ),
				PKEMeterCount: 0,
				hasShadowMirror: WorldGen.genRand.NextBool(),
				darkHeartPieceCount: WorldGen.genRand.Next( -1, 2 )
			);

			result = "Success.";
			return true;
		}


		////

		private bool IsNearAnotherExpedition(
					IList<(int tileX, int tileY, int paveTileType)> expeds,
					(int tileX, int tileY) proposed ) {
			var config = AMLConfig.Instance;
			int minDist = config.Get<int>( nameof(config.MinimumTileDistanceBetweenLostExpeditions) );
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
