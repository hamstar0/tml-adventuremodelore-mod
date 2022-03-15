using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_Orbs() {
			if( ModLoader.GetMod("Orbs") != null ) {
				Scannables.LoadScannable_Orbs_WeakRef();
			}
		}

		private static void LoadScannable_Orbs_WeakRef() {
			string msgId = "Scannable_Orbs";
			string msgTitle = "About Orbs usage";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I see you've found an Orb. You can use those to open passages to underground areas you'd not normally"
				+" be able to reach. Simply holding the orb will reveal any nearby chunks of terrain that can be removed by"
				+" orbs of its given color. Special seeing instruments can also reveal more terrain color details from"
				+" afar."
				+"\n \n"
				+"In technical terms, we call these 'Geo-Resonant Orbs' because they resonate with the ambient composition of"
				+" soil-borne psychomagnotheric materials of a matching spiritual attenuation frequency. Upon contact, the"
				+" resulting frequency harmonization causes solid matter extrusion and displacement from the occupying spiritual"
				+" media, which then immediately disperses into the surroundings. In short, this creates voids or passages, which"
				+" then permit access."
			);
			/*string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I have news that might help your progress! In case you didn't already know, your P.B.G device isn't strong enough"
				+" to create access to certain areas of this island. This is because your magical proficiency isn't high enough to"
				+" give it the [c/88FF88:power it needs]."
				+"\n \n"
				+"Fret not! If the ancient civilizations of this land were able to discover and master magic, you may be able to as"
				+" well. The secret to doing so must be somewhere in this land... or maybe even the land itself. You might try"
				+" seaching for [c/88FF88:hidden magical phenomena] within caves and grottos. Keep your eyes open for [c/88FF88:ways"
				+" to detect such things]."
			);*/

			int[] anyOfItemTypes = new int[] {
				ModContent.ItemType<Orbs.Items.BlueOrbItem>(),
				ModContent.ItemType<Orbs.Items.BrownOrbItem>(),
				ModContent.ItemType<Orbs.Items.CyanOrbItem>(),
				ModContent.ItemType<Orbs.Items.GreenOrbItem>(),
				ModContent.ItemType<Orbs.Items.PinkOrbItem>(),
				ModContent.ItemType<Orbs.Items.PurpleOrbItem>(),
				ModContent.ItemType<Orbs.Items.RedOrbItem>(),
				ModContent.ItemType<Orbs.Items.WhiteOrbItem>(),
				ModContent.ItemType<Orbs.Items.YellowOrbItem>()
			};

			//

			var scannable = new PKEScannable(
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg ),
				anyOfItemTypes: anyOfItemTypes
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
