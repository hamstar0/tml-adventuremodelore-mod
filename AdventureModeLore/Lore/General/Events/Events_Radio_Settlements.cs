﻿using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_Settlements() {
			int duration = 0;
			int minY = WorldLocationLibraries.SurfaceLayerBottomTileY * 16;

			bool PreReq() {
				if( !Main.LocalPlayer.dead && Main.LocalPlayer.MountedCenter.Y > minY ) {
					duration++;
				}
				return duration > 60 * 60;
			}

			//

			return new GeneralLoreEvent(
				name: "Radio - Settlements",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About settlements and travel",
						description: "Guide: \"So many caves in this land. And so many burial urns just lying"
							+" around. I can't tell if these are more caverns or catacombs...\""
							+"\n \n\"I have it on good authority that we'll need to search these as deeply as"
							+" possible. We'll need transport, lodging, and amenities as we go. Remember those"
							+" Furnishing Kits and mountable mirrors? I recommend using those every so often here."
							+" You may find signs of former inhabitants in these depths, and I bet they'll have"
							+" left dwellings that can work for our needs. Those mirrors are also a great tool"
							+" for establishing progress checkpoints. Just another of the fun magical artifacts our"
							+" research of this island has uncovered.\""
							+"\n \n\"Neither of these options are cheap, though, but you should be able to get"
							+" more. We'll need a large, thorough mirror network if our quest is going to"
							+" succeed.\"",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_Settlements"
					);
				},
				isRepeatable: false
			);
		}
	}
}