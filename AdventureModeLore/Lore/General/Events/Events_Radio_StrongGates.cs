using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_StrongGates_PreReq() {
			var rect = new Rectangle(
				x: (int)Main.LocalPlayer.Center.X,
				y: (int)Main.LocalPlayer.Center.Y,
				width: 0,
				height: 0
			);
			rect.X -= 16 * 16;
			rect.Y -= 16 * 16;
			rect.Width += 32 * 16;
			rect.Height += 32 * 16;

			return WorldGates.WorldGatesAPI.GetGateBarriers()
				.Where( b => b.Strength > Main.LocalPlayer.statManaMax2 )
				.Any( b => rect.Intersects( b.WorldArea ) );
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_StrongGates() {
			bool PreReq() {
				if( ModLoader.GetMod("WorldGates") == null ) {
					return false;
				}
				return GeneralLoreEventDefinitions.Event_Radio_StrongGates_PreReq();
			}

			//

			string msgId = "AML_Radio_StrongGates";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"That gate is too strong for you right now. You'll need to find a way to [c/88FF88:increase your P.B.G's power],"
				+" first."
			);

			return new GeneralLoreEvent(
				name: "Radio - Strong Gates",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About strong world gates",
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
