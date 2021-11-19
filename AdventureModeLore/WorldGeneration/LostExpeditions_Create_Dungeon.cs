using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private void CreateAtDungeonLE( int campWidth ) {
			(int x, int nearFloorY)? expedPointRaw;
			int x, nearFloorY;
			int paveTileType = TileID.Dirt;
			int chestIdx;
			string err;

			expedPointRaw = this.FindDungeonExpeditionLocation( campWidth, out paveTileType );
			if( !expedPointRaw.HasValue ) {
				throw new ModLibsException( "Could not find a place to generate dungeon 'lost expedition'" );
			}

			x = expedPointRaw.Value.x;
			nearFloorY = expedPointRaw.Value.nearFloorY;

			bool createdCamp = this.CreateExpeditionAt(
				leftTileX: x,
				nearFloorTileY: nearFloorY,
				campWidth: campWidth + 2,
				customTiles: new int[0],
				paveTileType: paveTileType,
				rememberLocation: true,
				chestIdx: out chestIdx,
				result: out err
			);
			if( !createdCamp ) {
				LogLibraries.Log( "Could not generate dungeon 'lost expedition': " + err );
				return;
			}

			this.FillExpeditionBarrel(
				tileX: expedPointRaw.Value.x,
				nearFloorTileY: expedPointRaw.Value.nearFloorY,
				chestIdx: chestIdx,
				hasLoreNote: false,
				speedloaderCount: 0,
				randomOrbCount: 4,
				whiteOrbCount: 0,
				canopicJarCount: 0,
				elixirCount: 0,
				mountedMirrorsCount: 0,
				PKEMeterCount: 0,
				hasShadowMirror: false,
				darkHeartPieceCount: 1
			);
		}
	}
}
