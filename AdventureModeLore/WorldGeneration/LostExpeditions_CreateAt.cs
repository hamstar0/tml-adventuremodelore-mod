using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private bool CreateExpeditionAt(
					int leftTileX,
					int nearFloorTileY,
					int campWidth,
					int[] customTiles,
					int paveTileType,
					bool rememberLocation,
					out int chestIdx,
					out string result ) {
			int toTileX = leftTileX + campWidth;

			for( int i = leftTileX; i < toTileX; i++ ) {
				this.PaveCampAt( i, nearFloorTileY, paveTileType );
			}

			// Tent
			int tentTileX = leftTileX + 1;

			if( !TilePlacementLibraries.TryPrecisePlace(tentTileX, nearFloorTileY, TileID.LargePiles2, 26) ) {
				chestIdx = -1;
				result = "Could not place tent at "+tentTileX+","+nearFloorTileY;
				return false;
			}

			// Campfire
			int fireTileX = leftTileX + 4;

			if( !TilePlacementLibraries.TryPrecisePlace(fireTileX, nearFloorTileY, TileID.Campfire) ) {
				chestIdx = -1;
				result = "Could not place campfire at "+fireTileX+","+nearFloorTileY;
				return false;
			}
			Main.tile[fireTileX, nearFloorTileY - 1].frameY += 36;//= 288;   // this is dumb
			Main.tile[fireTileX, nearFloorTileY].frameY += 36;//288 + 18;    // this is dumb
			Main.tile[fireTileX+1, nearFloorTileY - 1].frameY += 36;//288;     // this is dumb
			Main.tile[fireTileX+1, nearFloorTileY].frameY += 36;//288 + 18;    // this is dumb
			Main.tile[fireTileX+2, nearFloorTileY - 1].frameY += 36;//288;     // this is dumb
			Main.tile[fireTileX+2, nearFloorTileY].frameY += 36;//288 + 18;    // this is dumb

			// Barrel
			int chestTileX = leftTileX + 8;

			chestIdx = WorldGen.PlaceChest( chestTileX, nearFloorTileY, TileID.Containers, false, 5 );
			if( chestIdx == -1 ) {
				result = "Could not place barrel at "+chestTileX+","+nearFloorTileY;
					/*+"\n"+Main.tile[chestTileX-1, nearFloorTileY-1].ToString()
					+"\n"+Main.tile[chestTileX, nearFloorTileY-1].ToString()
					+"\n"+Main.tile[chestTileX+1, nearFloorTileY-1].ToString()
					+"\n"+Main.tile[chestTileX-1, nearFloorTileY].ToString()
					+"\n"+Main.tile[chestTileX, nearFloorTileY].ToString()
					+"\n"+Main.tile[chestTileX+1, nearFloorTileY].ToString()
					+"\n"+Main.tile[chestTileX-1, nearFloorTileY+1].ToString()
					+"\n"+Main.tile[chestTileX, nearFloorTileY+1].ToString()
					+"\n"+Main.tile[chestTileX+1, nearFloorTileY+1].ToString();*/
				return false;
			}

			//

			int customTileX = leftTileX + 11;
			foreach( int customTile in customTiles ) {
				if( TilePlacementLibraries.TryPrecisePlace(customTileX, nearFloorTileY, (ushort)customTile) ) {
					customTileX += 2;
				} else {
					result = "Could not place custom tile "+customTile+" at "+customTileX+","+nearFloorTileY;
					return false;
				}
			}

			//

			if( rememberLocation ) {
				var myworld = ModContent.GetInstance<AMLWorld>();
				myworld.LostExpeditions[ (chestTileX, nearFloorTileY) ] = false;
			}

			result = "Success.";
			return true;
		}


		////////////////

		private void PaveCampAt( int tileX, int nearFloorTileY, int tileType ) {
			// Clear space above camp
			int minAirY = nearFloorTileY - 3;
			int maxAirY = nearFloorTileY;
			int airY = maxAirY;

			for( Tile tile = Framing.GetTileSafely(tileX, airY);
						airY > minAirY;
						tile = Framing.GetTileSafely(tileX, --airY) ) {
				try {
					if( tile.active() ) {
						WorldGen.KillTile( tileX, airY );
					}
				} catch { }

				if( tile.active() ) {
					tile.ClearEverything();
				}
			}

			//

			var needsFill = new List<(int, int)>();

			// Clear bogus floor material away
			int minSolidY = nearFloorTileY + 1;
			int maxSolidY = nearFloorTileY + 7;
			int solidY = minSolidY;

			for( Tile tile = Framing.GetTileSafely(tileX, solidY);
						solidY < maxSolidY && !this.IsValidFloorTile(tile);
						tile = Framing.GetTileSafely(tileX, ++solidY) ) {
				try {
					if( tile.active() ) {
						WorldGen.KillTile( tileX, solidY );
					}
				} catch { }

				if( tile.active() ) {
					tile.ClearEverything();
				}

				needsFill.Add( (tileX, solidY) );
			}

			//

			Main.tile[ tileX, solidY ]?.slope( 0 );
			Main.tile[ tileX, solidY ]?.halfBrick( false );

			// Refill removed floor material
			for( int i=needsFill.Count-1; i>=0; i-- ) {
				(int x, int y) fillAt = needsFill[i];

				try {
					WorldGen.PlaceTile( fillAt.x, fillAt.y, tileType, false, true );
				} catch { }

				Tile tile = Framing.GetTileSafely( fillAt.x, fillAt.y );

				if( !this.IsValidFloorTile(tile) ) {
					tile.ClearEverything();

					tile.active( true );
					tile.type = (ushort)tileType;
					WorldGen.SquareTileFrame( fillAt.x, fillAt.y );

					if( !this.IsValidFloorTile(tile) ) {
						LogLibraries.Log( "Could not fill camp floor tile (type: "+tileType+") at "+fillAt.x+", "+fillAt.y
							+": "+tile.ToString() );
					}
				}
			}
		}
	}
}
