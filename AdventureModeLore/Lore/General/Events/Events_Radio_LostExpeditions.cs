using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_LostExpeditions_PreReq() {
			int pkeType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();
			bool hasPke = Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == pkeType );

			if( !hasPke ) {
				return false;
			}

			PKEMeter.Logic.PKEGaugeValues gauge = PKEMeter.PKEMeterAPI.GetGauge()?
				.Invoke( Main.LocalPlayer, Main.LocalPlayer.MountedCenter );
			return gauge != null
				? gauge.GreenPercent >= 0.75f
				: false;
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_LostExpeditions() {
			bool PreReq() {
				if( ModLoader.GetMod("PKEMeter") == null ) {
					return false;
				}

				return GeneralLoreEventDefinitions.Event_Radio_LostExpeditions_PreReq();
			}

			//

			string msgId = "AML_Radio_LostExpeditions";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"According to your PKE meter, you're getting close to something. Observe your green meter, and follow"
				+" it's signal. If my hunch is correct, you may find something important to your mission!"
			);

			return new GeneralLoreEvent(
				name: "Radio - Lost Expeditions",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About Lost Expeditions",
							description: msg,
							modOfOrigin: AMLMod.Instance,
							alertPlayer: MessagesAPI.IsUnread(msgId),
							isImportant: true,
							parentMessage: MessagesAPI.EventsCategoryMsg,
							id: msgId
						);
						return false;
					} );
				},
				isRepeatable: false
			);
		}
	}
}
