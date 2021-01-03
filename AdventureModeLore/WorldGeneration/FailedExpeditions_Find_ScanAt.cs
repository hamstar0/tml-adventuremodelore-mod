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
		private bool ScanFromTile(
					int tileX,
					int tileY,
					int campWidth,
					int floorPavingThickness,
					out (int x, int nearFloorY) campStartPos,
					out int mostCommonTileType ) {
			var floorTileTypes = new Dictionary<ushort, int>();

			int nearFloorY = 0;
			int width = 0;
			int checkLeftX = tileX - 1;
			int checkRightX = tileX + 1;
			int bot = tileY + floorPavingThickness;

			bool isChecked;
			do {
				isChecked = false;

				if( this.FindValidNearFloorTileAt(checkLeftX, tileY, bot, out nearFloorY) ) {
					Tile floorTile = Main.tile[ checkLeftX, nearFloorY + 1 ];
					floorTileTypes.AddOrSet( floorTile.type, 1 );

					isChecked = true;
					width++;
					checkLeftX--;
				}
				if( this.FindValidNearFloorTileAt(checkRightX, tileY, bot, out nearFloorY) ) {
					Tile floorTile = Main.tile[ checkRightX, nearFloorY + 1 ];
					floorTileTypes.AddOrSet( floorTile.type, 1 );

					isChecked = true;
					width++;
					checkRightX++;
				}
			} while( isChecked && width < campWidth );

			if( !this.FindValidNearFloorTileAt(checkLeftX, tileY, bot, out _) ) {
				checkLeftX++;
			}

			campStartPos = (checkLeftX, tileY);

			if( floorTileTypes.Count > 0 ) {
				mostCommonTileType = floorTileTypes.Aggregate( (kv1, kv2) => kv1.Value > kv2.Value ? kv1 : kv2 ).Key;
			} else {
				mostCommonTileType = -1;
			}

			return width >= campWidth;
		}


		////////////////

		private bool FindValidNearFloorTileAt( int tileX, int tileY, int maxY, out int nearFloorY ) {
			if( tileX < 0 || tileX >= Main.maxTilesX || tileY < 0 || tileY >= Main.maxTilesY ) {
				nearFloorY = tileY;
				return false;
			}

			int findFloorY = tileY;

			// Find floor
			bool hasEmptySpace = false;
			for( Tile tile = Main.tile[ tileX, findFloorY ];
						findFloorY < maxY && this.IsValidEmptyTile(tile);
						tile = Main.tile[tileX, ++findFloorY] ) {
				hasEmptySpace = true;
			}
			
			nearFloorY = findFloorY - 1;

			// Confirm floor
			Tile floorTile = Main.tile[ tileX, findFloorY ];
			if( !hasEmptySpace || !this.IsValidFloorTile(floorTile) ) {
				return false;
			}

			// Verify if valid "empty" space above floor
			for( int i=0; i<3; i++ ) {
				if( (nearFloorY - i) < 0 ) {
					return false;
				}

				Tile tile = Main.tile[ tileX, nearFloorY - i ];
				if( !this.IsValidEmptyTile(tile) ) {
					return false;
				}
			}

			return true;
		}


		////////////////

		private bool IsValidEmptyTile( Tile mytile ) {
			if( (mytile?.liquid ?? 0) > 0 ) {
				return false;
			}

			if( mytile?.active() == true ) {
				switch( mytile.type ) {
				case TileID.Stalactite:
				case TileID.GreenMoss:
				case TileID.BrownMoss:
				case TileID.RedMoss:
				case TileID.BlueMoss:
				case TileID.PurpleMoss:
				case TileID.LongMoss:
				case TileID.SmallPiles:
				case TileID.LargePiles:
				case TileID.LargePiles2:
				case TileID.LavaMoss:
				case TileID.Grass:
				case TileID.CorruptGrass:
				case TileID.FleshGrass:
				case TileID.HallowedGrass:
				case TileID.JungleGrass:
				case TileID.MushroomGrass:
					break;
				default:
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

		private bool IsValidFloorTile( Tile mytile ) {
			return WorldGen.SolidTile3( mytile );
		}
	}
}
