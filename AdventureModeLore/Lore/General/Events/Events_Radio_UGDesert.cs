﻿using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_UGDesert() {
			bool PreReq() {
				return NPC.downedGoblins
					&& !NPC.savedGoblin
					&& Main.LocalPlayer.ZoneUndergroundDesert;
			}

			//

			string msgId = "AML_Radio_UGDesert";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I've just gotten a report that neutrally-aligned goblin folk may be found somewhere in your current"
				+" vicinity. What an unusual place to find any civilized being... even one such as a goblin."
				+"\n \n"
				+"Anyway, if you want to gain access to the depths of this desert, you'll need some tools. If you have a supply"
				+" of orbs and bombs, you can use these to craft a special explosive designed for destroying"
				+" hardened sand. There are variations of these explosives available for other uses, also. Don't"
				+" squander your orbs, though."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I've just gotten a report that [c/88FF88:neutrally-aligned goblin folk may be found somewhere in your current"
				+" vicinity]. What an unusual place to find any civilized being... even one such as a goblin."
				+"\n \n"
				+"Anyway, if you want to gain access to the depths of this desert, you'll need some tools. If you have a supply"
				+" of [c/88FF88:orbs and bombs, you can use these to craft a special explosive designed for destroying"
				+" hardened sand]. There are variations of these explosives available for other uses, also. [c/88FF88:Don't"
				+" squander your orbs, though]."
			);*/

			return new GeneralLoreEvent(
				name: "Radio - Underground Desert",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About the Underground Desert",
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
