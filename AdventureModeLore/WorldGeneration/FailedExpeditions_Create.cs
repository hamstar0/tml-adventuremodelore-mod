using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private static Item CreateLoreNoteItem( int currentNodeIdx ) {
			var item = new Item();
			if( ModLoader.GetMod( "ReadableBooks" ) == null ) {
				return item;
			}
			
			item = ReadableBooks.Items.ReadableBook.ReadableBookItem.CreateBook(
				FailedExpeditionsGen.LoreNotes[ currentNodeIdx ].title,
				FailedExpeditionsGen.LoreNotes[ currentNodeIdx ].pages
			);

			return item;
		}


		private static Item CreateSpeedloaderItem() {
			var item = new Item();
			if( ModLoader.GetMod( "TheMadRanger" ) == null ) {
				return item;
			}

			item.SetDefaults( ModContent.ItemType<TheMadRanger.Items.SpeedloaderItem>(), true );

			return item;
		}



		////////////////

		private void GenerateExpeditionAt( int tileX, int tileY, int campWidth, int paveTileType ) {
			int toTileX = tileX + campWidth;

			for( int i=tileX; i<toTileX; i++ ) {
				this.PaveCampAt( tileX, tileY, paveTileType );
			}

			// Tent
			TilePlacementHelpers.TryPrecisePlace( tileX, tileY, TileID.LargePiles2, 26 );

			// Campfire
			TilePlacementHelpers.TryPrecisePlace( tileX+3, tileY, TileID.Campfire );
			Main.tile[ tileX+3, tileY-1 ].frameY = 288;		// this is dumb
			Main.tile[ tileX+3, tileY ].frameY = 288+18;    // this is dumb
			Main.tile[ tileX+4, tileY-1 ].frameY = 288;     // this is dumb
			Main.tile[ tileX+4, tileY ].frameY = 288+18;    // this is dumb

			// Barrel
			int chestIdx = WorldGen.PlaceChest( tileX + 5, tileY, TileID.Containers, false, 5 );
			if( chestIdx != -1 ) {
				Main.chest[chestIdx].item[0] = FailedExpeditionsGen.CreateLoreNoteItem( this.CurrentLoreNote );
				Main.chest[chestIdx].item[1] = FailedExpeditionsGen.CreateSpeedloaderItem();

				this.CurrentLoreNote = (this.CurrentLoreNote + 1) % FailedExpeditionsGen.LoreNotes.Length;
			}
		}

		private void PaveCampAt( int tileX, int tileY, int tileType ) {
			var needsFill = new List<(int, int)>();
			Tile tile = Main.tile[tileX, tileY];
			int depth = 0;

			while( tile?.active() != true || !Main.tileSolid[tile.type] ) {
				if( tile?.active() == true ) {
					WorldGen.KillTile( tileX, tileY );
				}

				needsFill.Add( (tileX, tileY) );

				tile = Main.tile[tileX, ++tileY];

				if( depth++ >= 6 ) {
					break;
				}
			}

			Main.tile[tileX, tileY]?.slope( 0 );

			for( int i=needsFill.Count-1; i>=0; i-- ) {
				(int x, int y) fillAt = needsFill[i];

				WorldGen.PlaceTile( fillAt.x, fillAt.y, tileType );
			}
		}
	}
}
