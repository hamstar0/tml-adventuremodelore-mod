using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_HouseFurnished() {
			bool PreReq() {
				if( ModLoader.GetMod("Ergophobia") == null ) {
					return false;
				}
				return AMLMod.Instance.HasFurnishedAHouse;
			}

			//

			string msgId = "AML_Radio_HouseFurnished";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"We'll need to establish more houses if we plan to have the support we need. Each house has its own"
				+" mirror you can use for travel. We'll need to spread these around as much as possible to"
				+" to accomplish our missions. These kits aren't cheap, so use them wisely!"
			);

			return new GeneralLoreEvent(
				name: "Radio - House Furnished",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About house networking",
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
