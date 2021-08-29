using System;
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
				var myworld = ModContent.GetInstance<AMLWorld>();
				return myworld.LostExpeditions.Count(kv => kv.Value) >= 3;
			}

			//

			string id = "AML_Radio_ManaShards1";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I have news that might help your progress! In case you didn't"
				+" already know, your P.B.G device isn't strong enough to create access to certain"
				+" areas of this island. But I have found the solution! You will need to increase your"
				+" magic wielding proficiency. Allow me to explain:"
				+"\n \n"
				+"Hidden around the island are pieces of a crystaline substance known as Mana"
				+" Shards. They are not visible to the naked eye, but can occasionally be found on"
				+" floors, walls, and ceilings. You should be able to be extracted them with basic"
				+" mining tools. I can't tell you yet how to go about detecting these reliably, but"
				+" keep your eyes peeled for clues!"
			);

			return new GeneralLoreEvent(
				name: "Radio - Mana Shards 1",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About Mana Shards (1)",
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
