using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private bool CreateExpeditionAt(
					int leftTileX,
					int nearFloorTileY,
					int campWidth,
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

			if( !TilePlacementHelpers.TryPrecisePlace(tentTileX, nearFloorTileY, TileID.LargePiles2, 26) ) {
				chestIdx = -1;
				result = "Could not place tent at "+tentTileX+","+nearFloorTileY;
				return false;
			}

			// Campfire
			int fireTileX = leftTileX + 4;

			if( !TilePlacementHelpers.TryPrecisePlace(fireTileX, nearFloorTileY, TileID.Campfire) ) {
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

			if( rememberLocation ) {
				var myworld = ModContent.GetInstance<AMLWorld>();
				myworld.FailedExpeditions.Add( (chestTileX, nearFloorTileY) );
			}

			result = "Success.";
			return true;
		}

		////////////////

		private void PaveCampAt( int tileX, int nearFloorTileY, int tileType ) {
			var needsFill = new List<(int, int)>();

			// Clear space above camp
			int aboveY = nearFloorTileY;
			for( Tile tile = Framing.GetTileSafely(tileX, aboveY);
						aboveY > (nearFloorTileY - 3);
						tile = Framing.GetTileSafely(tileX, --aboveY) ) {
				try {
					if( tile.active() ) {
						WorldGen.KillTile( tileX, aboveY );
					}
				} catch { }

				if( tile.active() ) {
					tile.ClearEverything();
				}
			}

			// Clear space below and find 'floor'
			int belowY = nearFloorTileY + 1;
			for( Tile tile = Framing.GetTileSafely(tileX, belowY);
						belowY < (nearFloorTileY + 7) && !this.IsValidFloorTile(tile);
						tile = Framing.GetTileSafely(tileX, ++belowY) ) {
				try {
					if( tile.active() ) {
						WorldGen.KillTile( tileX, belowY );
					}
				} catch { }

				if( tile.active() ) {
					tile.ClearEverything();
				}

				needsFill.Add( (tileX, belowY) );
			}

			Main.tile[ tileX, belowY ]?.slope( 0 );
			Main.tile[ tileX, belowY ]?.halfBrick( false );

			// Raise floor to camp's level
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

					if( !this.IsValidFloorTile( tile ) ) {
						LogHelpers.Log( "Could not fill camp floor tile (type: "+tileType+") at "+fillAt.x+", "+fillAt.y+": "+tile.ToString() );
					}
				}
			}
		}
	}
}
