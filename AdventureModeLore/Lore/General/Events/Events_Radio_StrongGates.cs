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
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_StrongGates_PreReq() {
			var rect = new Rectangle(
				x: (int)Main.LocalPlayer.MountedCenter.X,
				y: (int)Main.LocalPlayer.MountedCenter.Y,
				width: 0,
				height: 0
			);
			rect.X -= 16 * 16;
			rect.Y -= 16 * 16;
			rect.Width += 32 * 16;
			rect.Height += 32 * 16;

			return WorldGates.WorldGatesAPI.GetGateBarriers()
				.Where( b => b.Strength > Main.LocalPlayer.statManaMax2 )
				.Any( b => rect.Intersects( b.TileArea ) );
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
				"That gate is too strong for you right now. You'll need to find a way to increase your P.B.G's power,"
				+" first. Since it works by way of its user's magical proficiency, you'll have to find a way to increase yours."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"That gate is too strong for you right now. You'll need to find a way to [c/88FF88:increase your P.B.G's power],"
				+" first."
			);*/

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

					var objective = new PercentObjective(
						title: "Breach 3 Magical Barrier Gates",
						description: "Find and disable 3 magical barriers throughout the world. You may need to increase"
							+ "\n"+"your magic to do so. Your binoculars may give you some hints for this.",
						units: 3,
						condition: ( obj ) => {
							int downGates = WorldGates.WorldGatesAPI.GetGateBarriers()
								.Where( b => !b.IsActive )
								.Count();
							return (float)downGates / 3f;
						}
					);

					ObjectivesAPI.AddObjective( objective, 0, true, out _ );
				},
				isRepeatable: false
			);
		}
	}
}
