using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Messages;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_Warn_Underworld() {
			bool CanScan( int scrX, int scrY ) {
				return Main.LocalPlayer.ZoneUnderworldHeight;
			}

			//

			string msgId = "Scannable_Warn_Underworld";
			string msgTitle = "Message - Underworld PGB Warning";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Underworld warning: Your PBG decays quickly!"
			);

			//

			var scannable = new PKEScannable(
				canScan: CanScan,
				onScanCompleteAction: () => Scannables.CreateMessage(
					msgId: msgId,
					title: msgTitle,
					msg: msg,
					important: false,
					parent: MessagesAPI.GameInfoCategoryMsg
				)
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
