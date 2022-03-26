using System;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_MagicItems() {
			string msgId = "Scannable_MagicItems";
			string msgTitle = "Use magic to find magic";
			string msg = Message.RenderFormattedDescription( NPCID.OldMan,
				"Here's something helpful to remember: You can use most magic-using items to help detect"
				+" other magical things hidden within the land."
			);

			int[] anyOfItemTypes = new int[] {
				ItemID.WaterBolt,
				ItemID.Vilethorn,
				ItemID.CrimsonRod,
				ItemID.WandofSparking,
				ItemID.AmethystStaff,
				ItemID.SapphireStaff,
				ItemID.TopazStaff,
				ItemID.EmeraldStaff,
				ItemID.RubyStaff,
				ItemID.DiamondStaff,
				ItemID.AmberStaff,
			};

			//

			var scannable = new PKEScannable(
				onScanCompleteAction: () => Scannables.CreateMessage(
					msgId,
					msgTitle,
					msg,
					false,
					Messages.MessagesAPI.HintsTipsCategoryMsg
				),
				anyOfItemTypes: anyOfItemTypes
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
