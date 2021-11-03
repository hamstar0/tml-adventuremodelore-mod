using System;
using Terraria;
using Terraria.World.Generation;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ReadableBooks.Items.ReadableBook;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private static Item CreateLoreNoteItem( int currentNodeIdx ) {
			return ReadableBookItem.CreateBook(
				LostExpeditionsGen.LoreNotes[currentNodeIdx].title,
				LostExpeditionsGen.LoreNotes[currentNodeIdx].pages
			);
		}


		////

		private static Item CreateSpeedloaderItem() {
			if( ModLoader.GetMod("TheMadRanger") != null ) {
				return LostExpeditionsGen.CreateSpeedloaderItem_WeakRef();
			}
			return default;
		}

		private static Item CreateSpeedloaderItem_WeakRef() {
			var item = new Item();
			item.SetDefaults( ModContent.ItemType<TheMadRanger.Items.SpeedloaderItem>(), true );

			return item;
		}


		////

		private static Item CreateOrbItem() {
			if( ModLoader.GetMod("Orbs") != null ) {
				return LostExpeditionsGen.CreateOrbItem_WeakRef();
			}
			return default;
		}
		
		private static Item CreateOrbItem_WeakRef() {
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


		////

		private static Item CreateCanopicJarItem() {
			if( ModLoader.GetMod("Necrotis") != null ) {
				return LostExpeditionsGen.CreateCanopicJarItem_WeakRef();
			}
			return default;
		}

		private static Item CreateCanopicJarItem_WeakRef() {
			var item = new Item();
			item.SetDefaults( ModContent.ItemType<Necrotis.Items.EmptyCanopicJarItem>(), true );

			return item;
		}

		////

		private static Item CreateElixirOfLifeItem() {
			if( ModLoader.GetMod("Necrotis") != null ) {
				return LostExpeditionsGen.CreateElixirOfLifeItem_WeakRef();
			}
			return default;
		}

		private static Item CreateElixirOfLifeItem_WeakRef() {
			var item = new Item();
			item.SetDefaults( ModContent.ItemType<Necrotis.Items.ElixirOfLifeItem>(), true );

			return item;
		}

		////

		private static Item CreateMountedMirrorItem() {
			if( ModLoader.GetMod("MountedMagicMirrors") != null ) {
				return LostExpeditionsGen.CreateMountedMirrorItem_WeakRef();
			}
			return default;
		}

		private static Item CreateMountedMirrorItem_WeakRef() {
			var item = new Item();
			item.SetDefaults( ModContent.ItemType<MountedMagicMirrors.Items.MountableMagicMirrorTileItem>(), true );

			return item;
		}

		////

		private static (Item meterItem, Item missionItem, Item manualItem)? CreatePKEMeterItem() {
			if( ModLoader.GetMod("PKEMeter") != null ) {
				return LostExpeditionsGen.CreatePKEMeterItem_WeakRef();
			}
			return default;
		}

		private static (Item meterItem, Item missionItem, Item manualItem)? CreatePKEMeterItem_WeakRef() {
			var meterItem = new Item();
			meterItem.SetDefaults( ModContent.ItemType<PKEMeter.Items.PKEMeterItem>(), true );

			Item missionItem = ReadableBookItem.CreateBook(
				LostExpeditionsGen.MissionBriefingBookInfo.title,
				LostExpeditionsGen.MissionBriefingBookInfo.pages
			);

			Item manualItem = ReadableBookItem.CreateBook(
				LostExpeditionsGen.PKEBookInfo.title,
				LostExpeditionsGen.PKEBookInfo.pages
			);

			return (meterItem, missionItem, manualItem);
		}


		////

		private static Item CreateShadowMirrorItem() {
			if( ModLoader.GetMod("SpiritWalking") != null ) {
				return LostExpeditionsGen.CreateShadowMirrorItem_WeakRef();
			}
			return default;
		}

		private static Item CreateShadowMirrorItem_WeakRef() {
			var mirrorItem = new Item();
			mirrorItem.SetDefaults( ModContent.ItemType<SpiritWalking.Items.ShadowMirrorItem>(), true );

			return mirrorItem;
		}


		////

		private static Item CreateDarkHeartPieceItem() {
			if( ModLoader.GetMod("LockedAbilities") != null ) {
				return LostExpeditionsGen.CreateDarkHeartPieceItem_WeakRef();
			}
			return default;
		}

		private static Item CreateDarkHeartPieceItem_WeakRef() {
			var mirrorItem = new Item();
			mirrorItem.SetDefaults( ModContent.ItemType<LockedAbilities.Items.Consumable.DarkHeartPieceItem>(), true );

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
					int elixirCount,
					int mountedMirrorsCount,
					bool hasPKEMeter,
					bool hasShadowMirror,
					int darkHeartPieceCount ) {
			if( chestIdx == -1 ) {
				LogLibraries.Warn( "Could not fill 'lost expedition' barrel at "+tileX+", "+nearFloorTileY+"." );
				return;
			}

			Item[] chest = Main.chest[chestIdx].item;
			int itemIdx = 0;

			if( hasLoreNote ) {
				Item newItem = LostExpeditionsGen.CreateLoreNoteItem( this.CurrentLoreNote );
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<speedloaderCount; i++ ) {
				Item newItem = LostExpeditionsGen.CreateSpeedloaderItem_WeakRef();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<orbCount; i++ ) {
				Item newItem = LostExpeditionsGen.CreateOrbItem_WeakRef();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<canopicJarCount; i++ ) {
				Item newItem = LostExpeditionsGen.CreateCanopicJarItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<elixirCount; i++ ) {
				Item newItem = LostExpeditionsGen.CreateElixirOfLifeItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<mountedMirrorsCount; i++ ) {
				Item newItem = LostExpeditionsGen.CreateMountedMirrorItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			if( hasPKEMeter ) {
				(Item meterItem, Item missionItem, Item manualItem)? pkeItems = LostExpeditionsGen.CreatePKEMeterItem();
				if( pkeItems.HasValue ) {
					chest[itemIdx++] = pkeItems.Value.meterItem;
					chest[itemIdx++] = pkeItems.Value.missionItem;
					chest[itemIdx++] = pkeItems.Value.manualItem;
				}
			}
			if( hasShadowMirror ) {
				Item mirrorItem = LostExpeditionsGen.CreateShadowMirrorItem();
				if( mirrorItem != null ) {
					chest[itemIdx++] = mirrorItem;
				}
			}
			for( int i=0; i< darkHeartPieceCount; i++ ) {
				Item newItem = LostExpeditionsGen.CreateDarkHeartPieceItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}

			if( hasLoreNote ) {
				this.CurrentLoreNote = (this.CurrentLoreNote + 1) % LostExpeditionsGen.LoreNotes.Length;
			}
		}
	}
}
