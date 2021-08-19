using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_StrongGates() {
			bool PreReq() {
				Rectangle rect = Main.LocalPlayer.getRect();
				rect.X -= 16 * 16;
				rect.Y -= 16 * 16;
				rect.Width += 32 * 16;
				rect.Height += 32 * 16;

				return WorldGates.WorldGatesAPI.GetGateBarriers()
					.Where( b => b.Strength > Main.LocalPlayer.statManaMax2 )
					.Any( b => rect.Intersects(b.WorldArea) );
			}

			//

			return new GeneralLoreEvent(
				name: "Radio - Strong Gates",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About strong world gates",
						description: "Guide: That gate is too strong for you right now. You'll need to find a way to"
							+" increase your P.B.G's power, first.",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_StrongGates"
					);
				},
				isRepeatable: false
			);
		}
	}
}
