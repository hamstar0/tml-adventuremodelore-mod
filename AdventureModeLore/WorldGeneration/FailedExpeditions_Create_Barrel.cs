using System;
using Terraria;
using Terraria.World.Generation;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using ReadableBooks.Items.ReadableBook;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private static Item CreateLoreNoteItem( int currentNodeIdx ) {
			return ReadableBookItem.CreateBook(
				FailedExpeditionsGen.LoreNotes[currentNodeIdx].title,
				FailedExpeditionsGen.LoreNotes[currentNodeIdx].pages
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
			if( ModLoader.GetMod( "Necrotis" ) == null ) {
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
				"Mission Briefing",
				new string[] {
					"As you know, the island has become a PKE hotbed in recent months. Readings are on the rise, and it has "
					+ "become unsafe to send in live agents.",
					"Due to previous failed expeditions, we have since sent in agents Fender, Sigma, and Omicron, but they too "
					+ "have ceased signal contact. We can only hope it's due to interference, but we've no time for search "
					+ "and recovery.",
					"Before contact dropped, Sigma gave us a conclusive report that the Specimen has indeed found its way to "
					+ "the island. It MUST be captured ALIVE at all costs!",
					"You've been outfitted with special detection equipment calibrated specifically for locating the Specimen, "
					+ "though due to its nature, even this may be inadequate. The damn thing gets smarter by the minute!",
					"Reports are coming in of other parties taking interest in the island and its recent activities. Avoid any "
					+ "encounters with the locals or any other parties. You are well equipped to handle threats, so your only "
					+ "concern is the mission.",
					"Should you find any information on the whereabouts of the missing expeditions or agents, report to "
					+ "command when convenient, but above all: Find that specimen!"
				}
			);

			Item manualItem = ReadableBookItem.CreateBook(
				"PKE Meter",
				new string[] {
					"Your PKE Detector Mk. VII comes now with separate sensors calibrated specifically for your mission.",
					"Due to conditions on the island, the most important reading is the background PKE, indicated by the red "
					+ "bar. When this reading reaches critical, you must retreat to minimal safe distance off the island, "
					+ "and await further instructions",
					"Safety aside, your mission remains of vital importance. Your mark should be detected on the yellow "
					+ "channel, though precise calibration remains difficult. Do your best.",
					"Other phenomena on the island may warrant separate consideration. It is not in your mission "
					+ "specification, but we would like it if you could track the remains of the other failed expeditions, "
					+ "and also your fellow agents, if possible.",
					"As the expeditions prior were primarily recon, you may be able to locate them by readings from any "
					+ "gathered artifacts from their time on the island. The green channel should be attuned accordingly.",
					"Finally, we have reason to believe a threat may exist on the island other than the usual suspects. "
					+ "They may even be responsible for our missing crews and agents!",
					"We have no leads, but previous agent reports indicate a strange PKE valence not recorded on the "
					+ "usual channels. We've attuned the blue channel to the latest readings. Investigate at your "
					+ "discretion."
				}
			);

			return (meterItem, missionItem, manualItem);
		}



		////////////////

		private void FillExpeditionBarrel(
					int tileX,
					int tileY,
					int chestIdx,
					bool hasLoreNote,
					int speedloaderCount,
					int orbCount,
					int canopicJarCount,
					bool hasPKEMeter ) {
			if( chestIdx == -1 ) {
				LogHelpers.Warn( "Could not fill 'failed expedition' barrel at "+tileX+", "+tileY+"." );
				return;
			}

			Item[] chest = Main.chest[chestIdx].item;
			int itemIdx = 0;

			if( hasLoreNote ) {
				Item newItem = FailedExpeditionsGen.CreateLoreNoteItem( this.CurrentLoreNote );
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<speedloaderCount; i++ ) {
				Item newItem = FailedExpeditionsGen.CreateSpeedloaderItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<orbCount; i++ ) {
				Item newItem = FailedExpeditionsGen.CreateOrbItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			for( int i=0; i<canopicJarCount; i++ ) {
				Item newItem = FailedExpeditionsGen.CreateCanopicJarItem();
				if( newItem != null ) {
					chest[itemIdx++] = newItem;
				}
			}
			if( hasPKEMeter ) {
				(Item meterItem, Item missionItem, Item manualItem)? pkeItems = FailedExpeditionsGen.CreatePKEMeterItem();
				if( pkeItems.HasValue ) {
					chest[itemIdx++] = pkeItems.Value.meterItem;
					chest[itemIdx++] = pkeItems.Value.missionItem;
					chest[itemIdx++] = pkeItems.Value.manualItem;
				}
			}

			if( hasLoreNote ) {
				this.CurrentLoreNote = (this.CurrentLoreNote + 1) % FailedExpeditionsGen.LoreNotes.Length;
			}
		}
	}
}
