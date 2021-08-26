using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_ShadowMirror_PreReq() {
			int smType = ModContent.ItemType<SpiritWalking.Items.ShadowMirrorItem>();

			return Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == smType );
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_ShadowMirror() {
			bool PreReq() {
				if( ModLoader.GetMod("SpiritWalking") == null ) {
					return false;
				}
				return GeneralLoreEventDefinitions.Event_Radio_ShadowMirror_PreReq();
			}

			//

			string id = "AML_Radio_ShadowMirror";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Oh cool! You found a Shadow Mirror. In ancient times, these were used"
				+" by shamen to gaze into world of spirits to try to learn secrets and know of their"
				+" fortunes. YOU, however, have a special gift that lets you use them to actually enter"
				+" the spirit world; body and soul. Be warned: Entering that dark world may come at a"
				+" cost to your soul!"
			);
			return new GeneralLoreEvent(
				name: "Radio - Shadow Mirror",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About the Shadow Mirror",
						description: msg,
						modOfOrigin: AMLMod.Instance,
						alertPlayer: MessagesAPI.IsUnread(id),
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: id
					);
				},
				isRepeatable: false
			);
		}
	}
}
