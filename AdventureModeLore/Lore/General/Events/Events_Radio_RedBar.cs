using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_RedBar_PreReq() {
			int pkeType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();

			Item heldItem = Main.LocalPlayer.HeldItem;
			if( heldItem?.active != true || heldItem.type != pkeType ) {
				return false;
			}

			PKEMeter.Logic.PKEGaugesGetter gauges = PKEMeter.PKEMeterAPI.GetGauge();
			PKEMeter.Logic.PKEGaugeValues values = gauges.Invoke( Main.LocalPlayer, Main.LocalPlayer.MountedCenter );

			return values.RedPercent >= 1f;
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_RedBar() {
			bool PreReq() {
				return ModLoader.GetMod("PKEMeter") == null
						//|| ModLoader.GetMod("BossReigns") == null
					? false
					: GeneralLoreEventDefinitions.Event_Radio_RedBar_PreReq();
			}

			//
			
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I have some bad news! Your orbs seem to be non-function, currently."
				+"\n \n"
				+"To summarize: The land itself bears a curse that acts to repel intrusions, and"
				+" your presence here is activating it. I can't tell you what it's exact effects are,"
				+" but with orbs out of commission, they probably won't be helpful for us..."
				+ "\n \n"
				+"After analyzing your PKE meter's calibrations, I've arrived at"
				+" at an important conclusion. It's as I feared. The red gauge in particular corresponds"
				+" to a dangerous environmental buildup of negative spiritual energy. We've seen this"
				+" before. I didn't know the cyborg's technologies were this far ahead to actually"
				+" gauge this particular thing..."
				+"\n \n"
				+"The only way I know of to dispel this curse is to find the nexus point of these"
				+" gathering spiritual energies, and disperse it. Typically, there will be a powerful"
				+" physical manifestation of some sort at this nexus point... usually a dangerous beast"
				+" or other unpleasant phenomena. You'll know it when you see it. You may even need to"
				+" summon it explicitly. Destroy that manifestation, and the curse should lift... for a"
				+" time."
			);
			return new GeneralLoreEvent(
				name: "Radio - Red Bar",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "Uh oh!",
						description: msg,
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_RedBar"
					);
				},
				isRepeatable: false
			);
		}
	}
}
