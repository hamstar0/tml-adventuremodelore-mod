using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private (int x, int y)? FindExpeditionLocation() {
			for( int i=0; i<1000; i++ ) {
				int tileX = WorldGen.genRand.Next( 1, Main.maxTilesX - 1 );
				int tileY = WorldGen.genRand.Next( 1, Main.maxTilesY - 1 );

				if( !this.IsTileValid(tileX, tileY, Main.maxTilesY-1, out tileY) ) {
					continue;
				}

				if( this.ScanFromTile(tileX, tileY, out (int, int) scanPos) ) {
					return scanPos;
				}
			}

			return null;
		}


		////////////////

		private bool ScanFromTile( int tileX, int tileY, out (int x, int y) campStartPos ) {
			int campWidth = 10;

			int _ = 0;
			int width = 0;
			int checkLeftX = tileX - 1;
			int checkRightX = tileX + 1;
			int bot = tileY + 6;

			bool isChecked;
			do {
				isChecked = false;

				if( this.IsTileValid(checkLeftX, tileY, bot, out _) ) {
					isChecked = true;
					width++;
					checkLeftX--;
				}
				if( this.IsTileValid(checkRightX, tileY, bot, out _) ) {
					isChecked = true;
					width++;
					checkRightX++;
				}
			} while( isChecked && width < campWidth );

			campStartPos = (checkLeftX, tileY);
			return width >= campWidth;
		}


		////////////////

		private bool IsTileValid( int tileX, int tileY, int bottom, out int floorY ) {
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

			return found;
		}
	}
}
