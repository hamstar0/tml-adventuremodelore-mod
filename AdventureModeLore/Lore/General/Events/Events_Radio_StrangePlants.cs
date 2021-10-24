using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_StrangePlants() {
			bool PreReq() {
				return Main.LocalPlayer.inventory
					.Any( i => {
						if( i?.active != true ) {
							return false;
						}
						switch( i.type ) {
						case ItemID.StrangePlant1:
						case ItemID.StrangePlant2:
						case ItemID.StrangePlant3:
						case ItemID.StrangePlant4:
							return true;
						}
						return false;
					} );
			}

			//

			string msgId = "AML_Radio_StrangePlants";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Certain strange plants seem to have inherited some metaphysical properties from their environment. It is"
				+" rare to see such a completely passive manifestation of spiritual energy, but I guess it's statistically"
				+" plausible, considering all the other types of manifestation we've seen. In any case, try to harvest these"
				+" when you can. They're just brimming with hidden powers!"
			);

			return new GeneralLoreEvent(
				name: "Radio - Strange Plants",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About the Strange Plants",
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
