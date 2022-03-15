using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Objectives.Definitions;
using Messages.Definitions;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		private static void LoadScannable_StrongGates() {
			string msgId = "Scannable_StrongGates";
			string msgTitle = "About strong World Gates";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"That gate is too strong for you right now. You'll need to find a way to increase your P.B.G's"
				+" power, first. Since it works by way of its user's magical proficiency, you'll have to find"
				+" a way to increase yours."
			);

			//

			string objTitle = "Breach 3 Magical Barrier Gates";
			string objMsg = "Find and disable 3 magical barriers throughout the world. You may need to increase"
				+ "\n"+"your magic to do so. Your binoculars may give you some hints for this.";
			int units = 3;

			float objectiveCondition( Objective objective ) {
				int downGates = WorldGates.WorldGatesAPI.GetGateBarriers()
					.Where( b => !b.IsActive )
					.Count();
				return (float)downGates / (float)units;
			}

			//

			var scannable = new PKEScannable(
				canScan: (x, y) => NPC.downedGoblins && !NPC.savedGoblin && Main.LocalPlayer.ZoneUndergroundDesert,
				onScanCompleteAction: () => {
					Scannables.CreateMessage( msgId, msgTitle, msg );
					Scannables.CreatePercentObjective( objTitle, objMsg, 3, objectiveCondition );
				}
			);

			PKEMeterAPI.SetScannable( msgId, scannable, false, true );
		}
	}
}
