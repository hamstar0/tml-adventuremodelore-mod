using System;
using Terraria;
using Terraria.World.Generation;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ReadableBooks.Items.ReadableBook;


namespace AdventureModeLore.WorldGeneration {
	partial class AbandonedExpeditionsGen : GenPass {
		private static Item CreateLoreNoteItem( int currentNodeIdx ) {
			return ReadableBookItem.CreateBook(
				AbandonedExpeditionsGen.LoreNotes[currentNodeIdx].title,
				AbandonedExpeditionsGen.LoreNotes[currentNodeIdx].pages
			);
		}

		private static Item CreateSpeedloaderItem() {
			if( ModLoader.GetMod( "TheMadRanger" ) == null ) {
				return null;
			}

			var item = new Item();
			item.SetDefaults( ModContent.ItemType<TheMadRanger.Items.SpeedloaderItem>(), true );

			return item;
		}

		private static Item CreateOrbItem() {
			if( ModLoader.GetMod( "Orbs" ) == null ) {
				return null;
			}

			var item = new Item();
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
			if( ModLoader.GetMod("Necrotis") == null ) {
				return null;
			}

			var item = new Item();
			item.SetDefaults( ModContent.ItemType<Necrotis.Items.EmptyCanopicJarItem>(), true );

			return item;
		}

		private static (Item meterItem, Item missionItem, Item manualItem)? CreatePKEMeterItem() {
			if( ModLoader.GetMod( "PKEMeter" ) == null ) {
				return null;
			}

			var meterItem = new Item();
			meterItem.SetDefaults( ModContent.ItemType<PKEMeter.Items.PKEMeterItem>(), true );

			Item missionItem = ReadableBookItem.CreateBook(
				AbandonedExpeditionsGen.MissionBriefingBookInfo.title,
				AbandonedExpeditionsGen.MissionBriefingBookInfo.pages
			);

			Item manualItem = ReadableBookItem.CreateBook(
				AbandonedExpeditionsGen.PKEBookInfo.title,
				AbandonedExpeditionsGen.PKEBookInfo.pages
			);

			return (meterItem, missionItem, manualItem);
		}

		private static Item CreateShadowMirrorItem() {
			if( ModLoader.GetMod( "SpiritWalking" ) == null ) {
				return null;
			}

			var mirrorItem = new Item();
			mirrorItem.SetDefaults( ModContent.ItemType<SpiritWalking.Items.ShadowMirrorItem>(), true );

			return mirrorItem;
		}



		////////////////

		private void FillExpeditionBarrel(
					int tileX,
					int nearFloorTileY,
					int chestIdx,
					bool hasLoreNote,
					int speedloaderCount,
					int orbCount,
					int canopicJarCount,
					bool hasPKEMeter,
					bool hasShadowMirror ) {
			if( chestIdx == -1 ) {
				LogLibraries.Warn( "Could not fill 'abandoned expedition' barrel at " + tileX+", "+nearFloorTileY+"." );
				return;
			}

			Item[] chest = Main.chest[chestIdx].item;
			int itemIdx = 0;

			if( hasLoreNote ) {
				Item newItem = AbandonedExpeditionsGen.CreateLoreNoteItem( this.CurrentLoreNote );
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<speedloaderCount; i++ ) {
				Item newItem = AbandonedExpeditionsGen.CreateSpeedloaderItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<orbCount; i++ ) {
				Item newItem = AbandonedExpeditionsGen.CreateOrbItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<canopicJarCount; i++ ) {
				Item newItem = AbandonedExpeditionsGen.CreateCanopicJarItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			if( hasPKEMeter ) {
				(Item meterItem, Item missionItem, Item manualItem)? pkeItems = AbandonedExpeditionsGen.CreatePKEMeterItem();
				if( pkeItems.HasValue ) {
					chest[itemIdx++] = pkeItems.Value.meterItem;
					chest[itemIdx++] = pkeItems.Value.missionItem;
					chest[itemIdx++] = pkeItems.Value.manualItem;
				}
			}
			if( hasShadowMirror ) {
				Item mirrorItem = AbandonedExpeditionsGen.CreateShadowMirrorItem();
				if( mirrorItem != null ) {
					chest[itemIdx++] = mirrorItem;
				}
			}

			if( hasLoreNote ) {
				this.CurrentLoreNote = (this.CurrentLoreNote + 1) % AbandonedExpeditionsGen.LoreNotes.Length;
			}
		}
	}
}
