﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Messages;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_Dungeon_If() {
			string msgId = "Scannable_Dungeon";
			string msgTitle = "About the Dungeon";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"I see you've obtained access to that gigantic dungeon building. And I thought it looked big"
				+" on the outside! Who knows what the ancient civilizations used this monstrous building for..."
				+"\n \n"
				+"YOU'RE here to use this building for getting answers to our plague business."
				+" Having confirmed a massive PKE reading from this structure, it's safe to assume there's"
				+" going to be something to learn from here... and to fear! This place is absolutely"
				+" teaming with dark energies, and I swear there's noises coming from within, as well."
				+" The powerful spiritual presence of this place may even adversely affect your own"
				+" spiritual state. Do not linger longer than needed!"
			);

			//
			
			if( !MessagesAPI.IsUnread(msgId) ) {
				return;
			}

			//

			bool CanScan( int scrX, int scrY ) {
				return Main.LocalPlayer.ZoneDungeon && NPC.downedBoss3;
			}

			//

			var scannable = new PKEScannable(
				canScan: CanScan,
				onScanCompleteAction: () => Scannables.CreateMessage( msgId, msgTitle, msg )
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
