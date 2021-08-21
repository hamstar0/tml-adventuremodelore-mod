using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_UGDesert_PreReq() {
			int pkeType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();

			return Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == pkeType );
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_UGDesert() {
			bool PreReq() {
				return NPC.downedGoblins
					&& Main.LocalPlayer.ZoneUndergroundDesert
					&& !NPC.savedGoblin;
			}

			//
			
			return new GeneralLoreEvent(
				name: "Radio - Underground Desert",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About the Underground Desert",
						description: "Guide: \"I've just gotten a report that neutrally-aligned goblin folk"
						+" may be found somewhere in your current vicinity. What an unusual place to find any"
						+" civilized being... even one such as a goblin.\""
						+"\n \n\"Anyway, if you want to gain access to the depths of this place, you'll need some"
						+" tools. If you have a supply or orbs and bombs, you can use these to craft a special"
						+" explosive designed for destroying hardened sand. There are variations of these"
						+" explosives available for other uses, also. Don't squander your orbs, though.\"",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_UGDesert"
					);
				},
				isRepeatable: false
			);
		}
	}
}
