using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private (int x, int y)? FindExpeditionLocation( int campWidth, out int mostCommonTileType ) {
			for( int i=0; i<1000; i++ ) {
				int tileX = WorldGen.genRand.Next( 1, Main.maxTilesX - 1 );
				int tileY = WorldGen.genRand.Next( 1, Main.maxTilesY - 1 );

				if( !this.IsTileValid(tileX, tileY, Main.maxTilesY-1, out tileY) ) {
					continue;
				}

				if( this.ScanFromTile(tileX, tileY, campWidth, out (int, int) scanPos, out mostCommonTileType) ) {
					return scanPos;
				}
			}

			mostCommonTileType = -1;
			return null;
		}


		////////////////

		private bool ScanFromTile(
					int tileX,
					int tileY,
					int campWidth,
					out (int x, int y) campStartPos,
					out int mostCommonTileType ) {
			var tileTypes = new Dictionary<ushort, int>();

			int floorY = 0;
			int width = 0;
			int checkLeftX = tileX - 1;
			int checkRightX = tileX + 1;
			int bot = tileY + 6;

			bool isChecked;
			do {
				isChecked = false;

				if( this.IsTileValid(checkLeftX, tileY, bot, out floorY) ) {
					tileTypes.AddOrSet( Main.tile[checkLeftX, floorY+1].type, 1 );

					isChecked = true;
					width++;
					checkLeftX--;
				}
				if( this.IsTileValid(checkRightX, tileY, bot, out floorY) ) {
					tileTypes.AddOrSet( Main.tile[checkRightX, floorY+1].type, 1 );

					isChecked = true;
					width++;
					checkRightX++;
				}
			} while( isChecked && width < campWidth );

			campStartPos = (checkLeftX, tileY);

			if( tileTypes.Count > 0 ) {
				mostCommonTileType = tileTypes.Aggregate( ( kv1, kv2 ) => kv1.Value > kv2.Value ? kv1 : kv2 ).Key;
			} else {
				mostCommonTileType = -1;
			}

			return width >= campWidth;
		}


		////////////////

		private bool IsTileValid( int tileX, int tileY, int bottom, out int floorY ) {
			if( tileX < 0 || tileX >= Main.maxTilesX || tileY < 0 || tileY >= Main.maxTilesY ) {
				floorY = tileY;
				return false;
			}

			//

			bool isValid( Tile mytile ) {
				if( (mytile?.liquid ?? 0) > 0 ) {
					return false;
				}

				if( mytile?.active() == true ) {
					int type = mytile.type;
					if( type != TileID.Stalactite && (type < 179 || type > 187) ) {
						return false;
					}
				}

				switch( mytile?.wall ?? 0 ) {
				case WallID.BlueDungeonSlabUnsafe:
				case WallID.BlueDungeonTileUnsafe:
				case WallID.BlueDungeonUnsafe:
				case WallID.GreenDungeonSlabUnsafe:
				case WallID.GreenDungeonTileUnsafe:
				case WallID.GreenDungeonUnsafe:
				case WallID.PinkDungeonSlabUnsafe:
				case WallID.PinkDungeonTileUnsafe:
				case WallID.PinkDungeonUnsafe:
				case WallID.LihzahrdBrickUnsafe:
				case WallID.CorruptionUnsafe1:
				case WallID.CorruptionUnsafe2:
				case WallID.CorruptionUnsafe3:
				case WallID.CorruptionUnsafe4:
				case WallID.CrimsonUnsafe1:
				case WallID.CrimsonUnsafe2:
				case WallID.CrimsonUnsafe3:
				case WallID.CrimsonUnsafe4:
				case WallID.CrimstoneUnsafe:
				case WallID.EbonstoneUnsafe:
				case WallID.HiveUnsafe:
				case WallID.SpiderUnsafe:
					return false;
				}

				return true;
			}

			bool isValidFloor( Tile mytile ) {
				return mytile?.active() == true
					&& Main.tileSolid[mytile.type]
					&& !Main.tileSolidTop[mytile.type];
			}

			//

			bool found = false;
			floorY = tileY;

			for( Tile tile = Main.tile[tileX, tileY]; isValid(tile); tile = Main.tile[tileX, floorY] ) {
				found = true;
				floorY++;

				if( floorY >= bottom ) {
					break;
				}
			}

			if( found ) {
				floorY--;
			}

			return found && isValidFloor( Main.tile[tileX, floorY+1] );
		}
	}
}
