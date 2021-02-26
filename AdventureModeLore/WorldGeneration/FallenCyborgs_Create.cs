﻿using System;
using Terraria;
using Terraria.World.Generation;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class FallenCyborgsGen : GenPass {
		private void CreateCyborgs( GenerationProgress progress, int count ) {
			(int x, int nearFloorY)? cybPointRaw;
			int x, nearFloorY;

			int cybType = ModContent.TileType<FallenCyborgTile>();

			for( int cybCount=1; cybCount<count; cybCount++ ) {
				cybPointRaw = this.FindRandomLocationForACyborg();
				if( !cybPointRaw.HasValue ) {
					LogHelpers.Log( "Could not finish finding places to generate all 'fallen cyborgs' ("+cybCount+" of "+count+")" );
					break;
				}

				x = cybPointRaw.Value.x;
				nearFloorY = cybPointRaw.Value.nearFloorY;

				if( WorldGen.PlaceTile(x, nearFloorY, cybType) ) {
					LogHelpers.Log( "Placed cyborg ("+cybCount+" of "+count+")" );
					progress.Value = (float)cybCount / ( (float)count + 2f );
				} else {
					LogHelpers.Log( "Failed to place cyborg ("+cybCount+" of "+count+")"
					//	+"\n  "+Main.tile[x-1, nearFloorY-1].ToString()+", "+Main.tile[x, nearFloorY-1].ToString()+", "+Main.tile[x+1, nearFloorY-1].ToString()
					//	+"\n  "+Main.tile[x-1, nearFloorY].ToString()+", "+Main.tile[x, nearFloorY].ToString()+", "+Main.tile[x+1, nearFloorY].ToString()
					);
					cybCount--;
				}
			}
		}
	}
}