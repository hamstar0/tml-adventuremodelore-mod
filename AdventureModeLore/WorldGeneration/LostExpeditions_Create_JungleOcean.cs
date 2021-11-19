using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private void CreateJungleOceanLE( int campWidth ) {
			(int x, int nearFloorY)? expedPointRaw;
			int x, nearFloorY;
			int paveTileType = TileID.Dirt;
			int chestIdx;
			string err;

			expedPointRaw = this.FindJungleSideBeachExpeditionLocation( campWidth, out paveTileType );
			if( !expedPointRaw.HasValue ) {
				LogLibraries.Log( "Could not find a place to generate jungle 'lost expedition'" );
				return;
			}

			x = expedPointRaw.Value.x;
			nearFloorY = expedPointRaw.Value.nearFloorY;

			bool createdCamp = this.CreateExpeditionAt(
				leftTileX: x,
				nearFloorTileY: nearFloorY,
				campWidth: campWidth,
				customTiles: new int[0],
				paveTileType: paveTileType,
				rememberLocation: true,
				chestIdx: out chestIdx,
				result: out err
			);
			if( !createdCamp ) {
				LogLibraries.Log( "Could not generate jungle 'lost expedition': " + err );
				return;
			}

			this.FillExpeditionBarrel(
				tileX: expedPointRaw.Value.x,
				nearFloorTileY: expedPointRaw.Value.nearFloorY,
				chestIdx: chestIdx,
				hasLoreNote: true,
				speedloaderCount: WorldGen.genRand.NextFloat() < (2f/3f) ? 1 : 0,
				randomOrbCount: 0,
				whiteOrbCount: WorldGen.genRand.Next( 1, 4 ),
				canopicJarCount: WorldGen.genRand.Next( 1, 3 ),
				elixirCount: WorldGen.genRand.Next( 1, 3 ),
				mountedMirrorsCount: 0,
				PKEMeterCount: 0,
				hasOrbsBooklet: false,
				hasShadowMirror: false,
				darkHeartPieceCount: 1
			);
		}
	}
}
