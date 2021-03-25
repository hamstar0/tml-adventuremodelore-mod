using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private void CreateAtMidMapFE( int campWidth ) {
			(int x, int nearFloorY)? expedPointRaw;
			int x, nearFloorY;
			int paveTileType = TileID.Dirt;
			int chestIdx;
			string err;

			expedPointRaw = this.FindMiddleSurfaceExpeditionLocation( campWidth, out paveTileType );
			if( !expedPointRaw.HasValue ) {
				throw new ModHelpersException( "Could not find a place to generate first 'failed expedition'" );
			}

			x = expedPointRaw.Value.x;
			nearFloorY = expedPointRaw.Value.nearFloorY;
			int[] customTiles = new int[] { ModContent.TileType<FallenCyborgTile>() };

			bool createdCamp = this.CreateExpeditionAt(
				leftTileX: x,
				nearFloorTileY: nearFloorY,
				campWidth: campWidth + 2,
				customTiles: customTiles,
				paveTileType: paveTileType,
				rememberLocation: true,
				chestIdx: out chestIdx,
				result: out err
			);
			if( !createdCamp ) {
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
	}
}
