using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using Messages;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_Scaffolds_If() {
			string msgId = "Scannable_Scaffolds";
			string msgTitle = "About Scaffolds";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Scaffold Erecting Kits may not seem like much, but they're very useful for creating safety and"
				+" accessability structures. More importantly, in case of any protracted battles against many of"
				+" the island's brutish enemies, these will all but essential for shaping pieces of your environment"
				+" for strategic use and manueverability. Don't try to fight the big ones without them!"
			);

			//

			if( !MessagesAPI.IsUnread( msgId ) ) {
				return;
			}

			//

			int[] anyOfItemTypes = new int[] {
				ModContent.ItemType<Ergophobia.Items.ScaffoldingKit.ScaffoldingErectorKitItem>(),
			};

			//

			var scannable = new PKEScannable(
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg ),
				anyOfItemTypes: anyOfItemTypes
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
