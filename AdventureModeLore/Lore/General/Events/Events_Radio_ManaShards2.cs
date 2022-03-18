using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;
using Objectives;
using Messages;
using Messages.Definitions;
using AdventureModeLore.Lore.Dialogues.Events;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_ManaShardHints2_PreReq() {
			bool hasMerch = ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(
				DialogueLoreEventDefinitions.ObjectiveTitle_FindMerchant
			);
			if( !hasMerch ) {
				return false;
			}

			int pkeType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();
			return Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == pkeType );
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_ManaShardHints2() {
			string msgId = "AML_Radio_ManaShards2";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Listen. I've discovered something important: You remember that PKE meter you found earlier?"
				+" I've noticed its blue gauge matches the resonance detection behavior of your binoculars!"
				+" We've been using special binoculars like those in previous expeditions to this island to"
				+" detect spiritual phenomena. It seems like those cyborgs had something of the same idea!"
				+"\n \n"
				+"Anyway, to spell it out: Binoculars will occasionally detect certain magical phenomena in"
				+" the environment by way of a sparkling effect (especially while being focused). These appear"
				+" to be associated with some hidden magical property or even substance within the land."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Listen. I've discovered something important: You remember that PKE meter you found earlier? I've noticed its"
				+" [c/88FF88:blue gauge matches a resonance detection behavior of your binoculars]! We've been using special"
				+" binoculars like those in previous expeditions to this island to detect spiritual phenomena. It seems like"
				+" those cyborgs had something of the same idea!"
				+"\n \n"
				+"Anyway, to spell it out: [c/88FF88:Binoculars will occasionally detect certain magical phenomena in the"
				+" environment by way of a sparkling effect]. They appear to be associated with some hidden magical property or"
				+" substance within the island. It may aid in our quest to locate these phenomena."
			);*/

			bool PreReq() {
				if( ModLoader.GetMod("PKEMeter") == null ) {
					return false;
				}
				if( ModLoader.GetMod("FindableManaCrystals") == null ) {
					return false;
				}

				return GeneralLoreEventDefinitions.Event_Radio_ManaShardHints2_PreReq();
			}

			//

			return new GeneralLoreEvent(
				name: "Radio - Mana Shards 2",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
							MessagesAPI.AddMessage(
								title: "About natural magical phenomena",
								description: msg,
								modOfOrigin: AMLMod.Instance,
								alertPlayer: MessagesAPI.IsUnread(msgId),
								isImportant: true,
								parentMessage: MessagesAPI.EventsCategoryMsg,
								id: msgId
							);
						} );
						return false;
					} );
				},
				isRepeatable: false
			);
		}
	}
}
