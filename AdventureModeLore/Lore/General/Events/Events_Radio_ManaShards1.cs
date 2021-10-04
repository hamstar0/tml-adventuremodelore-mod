﻿using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Messages;
using Messages.Definitions;
using ModLibsCore.Services.Timers;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_ManaShardHints1() {
			bool PreReq() {
				if( ModLoader.GetMod("FindableManaCrystals") == null ) {
					return false;
				}

				var myworld = ModContent.GetInstance<AMLWorld>();
				return myworld.LostExpeditions.Count(kv => kv.Value) >= 3;
			}

			//

			string msgId = "AML_Radio_ManaShards1";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I have news that might help your progress! In case you didn't already know, your P.B.G device isn't strong enough"
				+" to create access to certain areas of this island. This is because your magical proficiency isn't high enough to"
				+" give it the [c/88FF88:power it needs]."
				+"\n \n"
				+"Fret not! If the ancient civilizations of this land were able to discover and master magic, you may be able to as"
				+" well. The secret to doing so must be somewhere in this land... or maybe even the land itself. You might try"
				+" seaching for [c/88FF88:hidden magical phenomena] within caves and grottos. Keep your eyes open for [c/88FF88:ways"
				+" to detect such things]."
			);

			return new GeneralLoreEvent(
				name: "Radio - Mana Shards 1",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About Mana Shards",
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