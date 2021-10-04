using System;
using Terraria;
using Terraria.ID;
using Messages;
using Messages.Definitions;
using ModLibsCore.Services.Timers;
using Objectives;
using AdventureModeLore.Lore.Dialogues.Events;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_MagicSecrets() {
			bool PreReq() {
				return ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(
					DialogueLoreEventDefinitions.ObjectiveTitle_FindJungle
				);
			}
			
			//

			string msgId = "AML_Radio_MagicSecrets";
			string msg = Message.RenderFormattedDescription( NPCID.OldMan,
				"Is this thing what you kids are using to talk with? I don't know what sort of quest you're on exactly, but I do"
				+" know this: This island is full of buried secrets. Someone clearly thought they should stay buried. How do you"
				+" think I get to be stuck here like I am, for example? Just a fair warning."
				+"\n \n"
				+"Anyway, if you're intent on exploring the depths, I can suggest one thing: [c/88FF88:Magic powers can reveal"
				+" magic] secrets! Don't believe me? Try using magic wherever you can underground, and see what turns up!"
				+"\n \n"
				+" Just don't forget: The pursuit of magic is what the ancient peoples of this land were also doing. We all"
				+" know how that turned out..."
			);

			return new GeneralLoreEvent(
				name: "Radio - Magic Secrets",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About Magic Secrets",
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
