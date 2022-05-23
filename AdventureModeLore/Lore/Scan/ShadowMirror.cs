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
		private static void LoadScannable_ShadowMirror() {
			if( ModLoader.GetMod("SpiritWalking") != null ) {
				Scannables.LoadScannable_ShadowMirror_WeakRef_If();
			}
		}

		private static void LoadScannable_ShadowMirror_WeakRef_If() {
			string msgId = "Scannable_ShadowMirror";
			string msgTitle = "About the Shadow Mirror";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Oh cool! You found a Shadow Mirror. In ancient times, these were used"
				+" by shamen to gaze into world of spirits to try to learn secrets and know of their"
				+" fortunes. YOU, however, have a special gift that lets you use them to actually enter"
				+" the spirit world; body and soul. Be warned: Entering that dark world may come at a"
				+" cost to your soul!"
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Oh cool! You found a Shadow Mirror. In ancient times, these were used"
				+" by shamen to gaze into world of spirits to try to learn secrets and know of their"
				+" fortunes. YOU, however, have a special gift that lets you use them to actually [c/88FF88:enter"
				+" the spirit world; body and soul]. Be warned: Entering that dark world may come at a"
				+" [c/88FF88:cost to your soul]!"
			);*/

			//

			if( !MessagesAPI.IsUnread(msgId) ) {
				return;
			}

			//

			int[] anyOfItemTypes = new int[] {
				ModContent.ItemType<SpiritWalking.Items.ShadowMirrorItem>(),
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
