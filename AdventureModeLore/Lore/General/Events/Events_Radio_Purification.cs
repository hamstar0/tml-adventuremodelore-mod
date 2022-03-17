using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static GeneralLoreEvent GetEvent_Radio_Purification() {
			bool PreReq() {
				return NPC.AnyNPCs( NPCID.Dryad );
			}

			//

			string evil = WorldGen.crimson ? "crimson" : "corruption";

			string msgId = "AML_Radio_Purification";
			string msg = Message.RenderFormattedDescription( NPCID.Dryad,
				"Are you trying to help save this world? You'll need my help! You cannot hope to penetrate the nasty"
				+" "+evil+" growth of this land by the usual means. You'll need my purification powder to have a"
				+" chance to break through. Use it."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Dryad,
				"Are you trying to help save this world? You'll need my help! You cannot hope to penetrate the nasty"
				+" "+evil+" growth of this land by the usual means. You'll need my [c/88FF88:purification powder] to have a"
				+" chance to break through. Use it."
			);*/

			return new GeneralLoreEvent(
				name: "Radio - Purification",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 25, false, () => {
						MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
							MessagesAPI.AddMessage(
								title: "About "+(WorldGen.crimson ? "crimson" : "corruption")+" purification",
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
