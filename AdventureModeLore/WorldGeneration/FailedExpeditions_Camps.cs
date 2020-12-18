using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private void CreateCamps( GenerationProgress progress, int count, int campWidth ) {
			this.CreateMidMapPKECamp( campWidth );
			progress.Value = 1f / ((float)count + 2f);

			this.CreateJungleOceanCamp( campWidth );
			progress.Value = 1f / ((float)count + 2f);

			this.CreateUndergroundCamps( progress, count, campWidth );
		}


		////////////////

		private void CreateMidMapPKECamp( int campWidth ) {
			(int x, int nearFloorY)? expedPointRaw;
			int x, nearFloorY;
			int paveTileType = TileID.Dirt;
			int chestIdx;
			string err;

			expedPointRaw = this.FindMiddleSurfaceExpeditionLocation( campWidth, out paveTileType );
			if( !expedPointRaw.HasValue ) {
				LogHelpers.Log( "Could not find a place to generate first 'failed expedition'" );
				return;
			}

			x = expedPointRaw.Value.x;
			nearFloorY = expedPointRaw.Value.nearFloorY;
			if( !this.CreateExpeditionAt(x, nearFloorY, campWidth, paveTileType, false, out chestIdx, out err) ) {
				LogHelpers.Log( "Could not generate first 'failed expedition': "+err );
				return;
			}

			this.FillExpeditionBarrel(
				tileX: expedPointRaw.Value.x,
				nearFloorTileY: expedPointRaw.Value.nearFloorY,
				chestIdx: chestIdx,
				hasLoreNote: false,
				speedloaderCount: 0,
				orbCount: 0,
				canopicJarCount: 0,
				hasPKEMeter: true
			);
		}

		private void CreateJungleOceanCamp( int campWidth ) {
			(int x, int nearFloorY)? expedPointRaw;
			int x, nearFloorY;
			int paveTileType = TileID.Dirt;
			int chestIdx;
			string err;

			expedPointRaw = this.FindJungleSideBeachExpeditionLocation( campWidth, out paveTileType );
			if( !expedPointRaw.HasValue ) {
				LogHelpers.Log( "Could not find a place to generate jungle 'failed expedition'" );
				return;
			}

			x = expedPointRaw.Value.x;
			nearFloorY = expedPointRaw.Value.nearFloorY;
			if( !this.CreateExpeditionAt(x, nearFloorY, campWidth, paveTileType, true, out chestIdx, out err) ) {
				LogHelpers.Log( "Could not generate jungle 'failed expedition': "+err );
				return;
			}

			this.FillExpeditionBarrel(
				tileX: expedPointRaw.Value.x,
				nearFloorTileY: expedPointRaw.Value.nearFloorY,
				chestIdx: chestIdx,
				hasLoreNote: true,
				speedloaderCount: WorldGen.genRand.NextFloat() < (2f/3f) ? 1 : 0,
				orbCount: WorldGen.genRand.Next( 1, 4 ),
				canopicJarCount: WorldGen.genRand.Next( 1, 3 ),
				hasPKEMeter: false
			);
		}

		private void CreateUndergroundCamps( GenerationProgress progress, int count, int campWidth ) {
			(int x, int nearFloorY)? expedPointRaw;
			int x, nearFloorY;
			int paveTileType = TileID.Dirt;
			int chestIdx;
			string err;

			for( int expedNum=1; expedNum<count; expedNum++ ) {
				expedPointRaw = this.FindRandomExpeditionLocation( campWidth, out paveTileType );
				if( !expedPointRaw.HasValue ) {
					LogHelpers.Log( "Could not finish finding places to generate all 'failed expeditions' ("+expedNum+" of "+count+")" );
					break;
				}

				x = expedPointRaw.Value.x;
				nearFloorY = expedPointRaw.Value.nearFloorY;
				if( !this.CreateExpeditionAt(x, nearFloorY, campWidth, paveTileType, true, out chestIdx, out err) ) {
					LogHelpers.Log( "Could not finish generating all 'failed expeditions' ("+expedNum+" of "+count+"): "+err );
					break;
				}

				this.FillExpeditionBarrel(
					tileX: expedPointRaw.Value.x,
					nearFloorTileY: expedPointRaw.Value.nearFloorY,
					chestIdx: chestIdx,
					hasLoreNote: true,
					speedloaderCount: WorldGen.genRand.NextFloat() < (2f/3f) ? 1 : 0,
					orbCount: WorldGen.genRand.Next( 1, 4 ),
					canopicJarCount: WorldGen.genRand.Next( 1, 3 ),
					hasPKEMeter: false
				);
				
				progress.Value = (float)expedNum / ((float)count + 2f);
			}
		}
	}
}
