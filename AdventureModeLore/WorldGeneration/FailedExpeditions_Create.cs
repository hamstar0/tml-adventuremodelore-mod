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

		private static Item CreateOrbItem() {
			var item = new Item();
			if( ModLoader.GetMod( "Orbs" ) == null ) {
				return item;
			}

			/*switch( WorldGen.genRand.Next(8) ) {
			case 0:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.BlueOrbItem>(), true );
				break;
			case 1:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.BrownOrbItem>(), true );
				break;
			case 2:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.CyanOrbItem>(), true );
				break;
			case 3:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.GreenOrbItem>(), true );
				break;
			case 4:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.PinkOrbItem>(), true );
				break;
			case 5:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.PurpleOrbItem>(), true );
				break;
			case 6:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.RedOrbItem>(), true );
				break;
			case 7:
				item.SetDefaults( ModContent.ItemType<Orbs.Items.YellowOrbItem>(), true );
				break;
			}*/
			item.SetDefaults( ModContent.ItemType<Orbs.Items.WhiteOrbItem>(), true );

			return item;
		}

		private static Item CreateCanopicJarItem() {
			var item = new Item();
			if( ModLoader.GetMod( "Necrotis" ) == null ) {
				return item;
			}

			item.SetDefaults( ModContent.ItemType<Necrotis.Items.EmptyCanopicJarItem>(), true );

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
				Main.chest[chestIdx].item[2] = FailedExpeditionsGen.CreateOrbItem();
				Main.chest[chestIdx].item[3] = FailedExpeditionsGen.CreateOrbItem();
				Main.chest[chestIdx].item[4] = FailedExpeditionsGen.CreateOrbItem();
				Main.chest[chestIdx].item[5] = FailedExpeditionsGen.CreateCanopicJarItem();

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
