using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Message_UnderworldWarn() {
			bool PreReq() {
				return Main.LocalPlayer.ZoneUnderworldHeight;
			}

			//

			string msgId = "AML_Message_Underworld";

			return new GeneralLoreEvent(
				name: "Message - Underworld PGB Warning",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "Underworld warning: Your PBG decays quickly!",
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
