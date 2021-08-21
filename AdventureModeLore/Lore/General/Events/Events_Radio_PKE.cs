﻿using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_PKE_PreReq() {
			int pkeType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();

			return Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == pkeType );
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_PKE() {
			bool PreReq() {
				return ModLoader.GetMod("PKEMeter") == null 
					? false
					: GeneralLoreEventDefinitions.Event_Radio_PKE_PreReq();
			}

			//

			return new GeneralLoreEvent(
				name: "Radio - PKE",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About the PKE Meter",
						description: "Guide: \"That's some serious tech to be hauling out here. I don't know much"
						+" about these cyborgs, but they must also be looking for something on this island."
						+" We can only hope our goals overlap... It looks like things aren't going so smoothly"
						+" for them, at the moment. I suppose they won't mind if we borrow some of their toys?\""
						+"\n \n\"In any event, we can't ignore aid like this... witting or no. What you've found"
						+" there is a Psychokinetic Energy detection device, AKA a PKE Meter. It comes"
						+" pre-programmed with a few specific frequencies. You'll have to try to figure out"
						+" what they're attuned to. This model only detects frequencies by proximity, so you can"
						+" tell how close you are by the intensity of the reading. Find out what they were after.\"",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_PKE"
					);
				},
				isRepeatable: false
			);
		}
	}
}