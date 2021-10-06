using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_AnimaEmpty_PreReq() {
			return Necrotis.NecrotisAPI.GetAnimaPercentOfPlayer( Main.LocalPlayer ) <= 0f;
		}


		////////////////
		
		private static GeneralLoreEvent GetEvent_Radio_AnimaEmpty() {
			bool PreReq() {
				if( ModLoader.GetMod("Necrotis") == null ) {
					return false;
				}
				return GeneralLoreEventDefinitions.Event_Radio_AnimaEmpty_PreReq();
			}

			//

			string msgId = "AML_Radio_AnimaEmpty";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Hey be careful! If you spend too much time in dark and dingey places in this land, it'll have a way of getting"
				+" to you. You look like you're a bit down in the dumps, presently. That's called Necrotis: The symptom most"
				+" commonly associated with the undeath plague. Get clear of the area you're in when it starts getting"
				+" too severe. Your anima meter shows how close you are to becoming afflicted."
				+"\n \n"
				+"You might be able to recover your status if you can extract from a pure spiritual energy source"
				+" somewhere in your vicinity. You'll commonly find this on this island in the form of ectoplasmic"
				+" residue located within burial containers. Though grave robbing is usually frowned upon, I'm sure"
				+" the dead won't mind. Not like they need it anymore... except to become UNdead, if left unchecked!"
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Hey be careful! If you spend too much time in dark and dingey places in this land, it'll have a way of getting"
				+" to you. You look like you're a bit down in the dumps, presently. That's called Necrotis: The symptom most"
				+" commonly associated with the undeath plague. [c/88FF88:Get clear of the area you're in when it starts getting"
				+" too severe]. Your anima meter shows how close you are to becoming afflicted."
				+"\n \n"
				+"You might be able to [c/88FF88:recover your status] if you can extract from a pure spiritual energy source"
				+" somewhere in your vicinity. You'll commonly find this on this island in the form of [c/88FF88:ectoplasmic"
				+" residue located within burial containers]. Though grave robbing is usually frowned upon, I'm sure"
				+" the dead won't mind. Not like they need it anymore... except to become UNdead, if left unchecked!"
			);*/

			return new GeneralLoreEvent(
				name: "Radio - Anima Empty",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						MessagesAPI.AddMessage(
							title: "About your Anima",
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
