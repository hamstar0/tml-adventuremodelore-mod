using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_Dungeon() {
			bool PreReq() {
				return Main.LocalPlayer.ZoneDungeon && NPC.downedBoss3;
			}

			//

			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I see you've obtained safe access to that gigantic dungeon building."
				+" And I thought it looked big on the outside! Who knows what the ancient civilizations"
				+" used this monstrous building for..."
				+"\n \n"
				+"YOU'RE here to use this building for getting answers to this plague business."
				+" Having confirmed a massive PKE reading from this structure, it's safe to assume there's"
				+" going to be something to learn from here... and to fear! This place is absolutely"
				+" teaming with dark energies, and I swear there's noises coming from within, as well."
				+" The powerful spiritual presence of this place may even adversely affect your own"
				+" spiritual state. Do not linger longer than needed!"
			);
			return new GeneralLoreEvent(
				name: "Radio - Dungeon",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About the Dungeon",
						description: msg,
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_Dungeon"
					);
				},
				isRepeatable: false
			);
		}
	}
}
