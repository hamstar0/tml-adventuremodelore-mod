using System;
using Terraria;
using Terraria.ID;
using Objectives;
using Messages;
using Messages.Definitions;
using ModLibsCore.Services.Timers;
using AdventureModeLore.Lore.Dialogues.Events;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_ManaShardHints2() {
			bool PreReq() {
				return ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(
					DialogueLoreEventDefinitions.ObjectiveTitle_FindMerchant
				);
			}

			//

			string id = "AML_Radio_ManaShards2";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Listen. I've discovered something important: You remember that"
				+" PKE meter you found earlier? I've noticed its blue gauge matches a resonance"
				+" detection behavior of your binoculars! We've been using special binoculars like"
				+" those in previous expeditions to this island to detect spiritual phenomena."
				+" It seems like those cyborgs had something of the same idea!"
				+"\n \n"
				+"Anyway, to spell it out: Binoculars will occasionally detect certain magical"
				+" phenomena in the environment by way of a sparkling effect. They appear to be"
				+" associated with some hidden magical property or substance within the island."
				+" It may aid in our quest to locate these phenomena."
			);

			return new GeneralLoreEvent(
				name: "Radio - Mana Shards 2",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About Mana Shards (2)",
							description: msg,
							modOfOrigin: AMLMod.Instance,
							alertPlayer: MessagesAPI.IsUnread(id),
							isImportant: true,
							parentMessage: MessagesAPI.EventsCategoryMsg,
							id: id
						);
						return false;
					} );
				},
				isRepeatable: false
			);
		}
	}
}
