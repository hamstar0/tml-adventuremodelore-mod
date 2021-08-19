using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Messages;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_Orbs_PreReq( ref ISet<int> orbTypes ) {
			if( orbTypes == null ) {
				orbTypes = new HashSet<int> {
						ModContent.ItemType<Orbs.Items.BlueOrbItem>(),
						ModContent.ItemType<Orbs.Items.BrownOrbItem>(),
						ModContent.ItemType<Orbs.Items.CyanOrbItem>(),
						ModContent.ItemType<Orbs.Items.GreenOrbItem>(),
						ModContent.ItemType<Orbs.Items.PinkOrbItem>(),
						ModContent.ItemType<Orbs.Items.PurpleOrbItem>(),
						ModContent.ItemType<Orbs.Items.RedOrbItem>(),
						ModContent.ItemType<Orbs.Items.WhiteOrbItem>(),
						ModContent.ItemType<Orbs.Items.YellowOrbItem>(),
					};
			}

			var ot = orbTypes;
			return Main.LocalPlayer.inventory
				.Any( i => i?.active == true && ot.Contains( i.type ) );
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_Orbs() {
			ISet<int> orbTypes = null;

			//
			
			bool PreReq() {
				if( ModLoader.GetMod( "Orbs" ) == null ) {
					return false;
				}
				return GeneralLoreEventDefinitions.Event_Radio_Orbs_PreReq( ref orbTypes );
			}

			//

			return new GeneralLoreEvent(
				name: "Radio - Orbs",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					MessagesAPI.AddMessage(
						title: "About Orbs usage",
						description: "Guide: \"I see you've found an Orb. You can use those to open passages to"
							+" underground areas you'd not normally be able to reach. Simply holding the orb will"
							+" reveal any nearby chunks of terrain that can be removed by any orb of its given"
							+" color. Special seeing instruments can also reveal more terrain color details from"
							+" afar.\""
							+"\n \n"
							+"\"In technical terms, we call these 'Geo-Resonant Orbs' because they resonate"
							+" with the ambient composition of soil-borne psychomagnotheric materials of a matching"
							+" spiritual attenuation frequency. Upon contact, the resulting frequency"
							+" harmonization causes solid matter extrusion and displacement from the occupying"
							+" spiritual media, which then immediately disperses into the surroundings.\"",
						modOfOrigin: AMLMod.Instance,
						alertPlayer: true,
						isImportant: true,
						parentMessage: MessagesAPI.EventsCategoryMsg,
						id: "AML_Radio_Orbs"
					);
				},
				isRepeatable: false
			);
		}
	}
}
