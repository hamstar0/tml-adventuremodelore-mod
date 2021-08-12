using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private bool ScanFromTileForCamp(
					int tileX,
					int nearCampFloortileY,
					int campWidth,
					int floorPavingThickness,
					int neededEmptySpaceAbove,
					out (int tileX, int nearFloorTileY) campStartPos,
					out int mostCommonTileType ) {
			var floorTileTypes = new Dictionary<ushort, int>();

			int width = 0;
			int checkLeftX = tileX - 1;
			int checkRightX = tileX + 1;
			int botY = nearCampFloortileY + floorPavingThickness;

			nearCampFloortileY--;

			bool foundValidFloor;
			do {
				foundValidFloor = false;

				int nearCampFloorTileY2 = 0;

				if( this.FindValidNearFloorTileAt(checkLeftX, nearCampFloortileY, botY, neededEmptySpaceAbove, out nearCampFloorTileY2) ) {
					Tile floorTile = Main.tile[ checkLeftX, nearCampFloorTileY2 + 1 ];
					floorTileTypes.AddOrSet( floorTile.type, 1 );

					foundValidFloor = true;
					width++;
					checkLeftX--;
				}
				if( this.FindValidNearFloorTileAt(checkRightX, nearCampFloortileY, botY, neededEmptySpaceAbove, out nearCampFloorTileY2) ) {
					Tile floorTile = Main.tile[ checkRightX, nearCampFloorTileY2 + 1 ];
					floorTileTypes.AddOrSet( floorTile.type, 1 );

					foundValidFloor = true;
					width++;
					checkRightX++;
				}
			} while( foundValidFloor && width < campWidth );

			// Widen to the left by 1, if possible
			if( !this.FindValidNearFloorTileAt(checkLeftX, nearCampFloortileY, botY, neededEmptySpaceAbove, out _ ) ) {
				checkLeftX++;
			}

			campStartPos = (checkLeftX, nearCampFloortileY);

			if( floorTileTypes.Count > 0 ) {
				mostCommonTileType = floorTileTypes.Aggregate( (kv1, kv2) => kv1.Value > kv2.Value ? kv1 : kv2 ).Key;
			} else {
				mostCommonTileType = -1;
			}

			return mostCommonTileType != -1 && width >= campWidth;
		}


		////////////////

		private bool FindValidNearFloorTileAt(
					int tileX,
					int topTileY,
					int botTileY,
					int neededEmptySpaceAbove,
					out int nearFloorTileY ) {
			if( tileX < 0 || tileX >= Main.maxTilesX || topTileY < 0 || topTileY >= Main.maxTilesY ) {
				nearFloorTileY = topTileY;
				return false;
			}

			int findFloorY = topTileY;

			// Find floor
			bool hasEmptySpace = false;
			for( Tile tile = Main.tile[ tileX, findFloorY ];
						findFloorY < botTileY && this.IsValidEmptyTile( tile );
						tile = Main.tile[ tileX, ++findFloorY ] ) {
				hasEmptySpace = true;
			}
			
			nearFloorTileY = findFloorY - 1;

			// Confirm floor
			Tile floorTile = Main.tile[ tileX, findFloorY ];
			if( !hasEmptySpace || !this.IsValidFloorTile(floorTile) ) {
				return false;
			}

			// Verify if valid "empty" space above floor
			for( int i=0; i<neededEmptySpaceAbove; i++ ) {
				if( (nearFloorTileY - i) < 0 ) {
					return false;
				}

				Tile tile = Main.tile[ tileX, nearFloorTileY - i ];
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
			if( !WorldGen.SolidTile3(mytile) ) {
				return false;
			}

			switch( mytile.type ) {
			case TileID.BreakableIce:
			case TileID.Obsidian:
			case TileID.HoneyBlock:
			case TileID.CrispyHoneyBlock:
			case TileID.LeafBlock:
			case TileID.LivingMahoganyLeaves:
				return false;
			default:
				return true;
			}
		}
	}
}
