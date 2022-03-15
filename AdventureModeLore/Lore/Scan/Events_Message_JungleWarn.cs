using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Message_JungleWarn() {
			bool PreReq() {
				return Main.LocalPlayer.ZoneJungle;
			}
			
			//

			string msgId = "AML_Message_Jungle";

			return new GeneralLoreEvent(
				name: "Message - Jungle PGB Warning",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "Jungle warning: Your PBG decays quickly!",
						description: "",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: MessagesAPI.IsUnread( msgId ),
						isImportant: false,
						parentMessage: MessagesAPI.GameInfoCategoryMsg,
						id: msgId
					);
				},
				isRepeatable: false
			);
		}
	}
}
