using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Services.Messages.Simple;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Intro() {
			return new GeneralLoreEvent(
				name: "Intro",
				prereqs: new Func<bool>[] { () => true },
				myevent: () => {
					SimpleMessage.PostMessage(
						msg: "Terraria, The Land of the Undead",
						submsg: "Eliminate the source of undeath!",
						duration: 60 * 10,
						isBordered: true,
						color: new Color(255, 224, 64)
					);
				},
				isRepeatable: false
			);
		}
	}
}
