using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_Trickster_PreReq() {
			return TheTrickster.TheTricksterAPI.GetTricksterDefeatLocations().Count > 0;
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_Trickster() {
			bool PreReq() {
				if( ModLoader.GetMod("PKEMeter") == null ) {
					return false;
				}
				if( ModLoader.GetMod("TheTrickster") == null ) {
					return false;
				}

				return GeneralLoreEventDefinitions.Event_Radio_Trickster_PreReq();
			}

			//

			string msgId = "AML_Radio_Trickster";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"That thing you just met must be what the other island visitors are after. What a crazy monstrosity it is!"
				+" Who knows its motives? It's certainly unusually powerful..."
				+"\n \n"
				+"In any case, it's just curious enough of a thing that it may be worth investigating, if only to determine if it"
				+" pertains to our quest. There is only one piece of information I can give of any use: It likely won't return to"
				+" an area where it's been driven away from. These will become marked on your map, from now on. If you're looking"
				+" for it, try venturing away from these areas."
				+"\n \n"
				+"Also, needless to say: Be careful! There's no telling what kind of mischief it's capable of, in the wrong times"
				+" and places! Judging by its abilities, it seems fully able to create surprise ambushes..."
			);

			return new GeneralLoreEvent(
				name: "Radio - Trickster",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About the Trickster",
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
