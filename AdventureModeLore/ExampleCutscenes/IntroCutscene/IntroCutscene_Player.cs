using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Info;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool IsSiezingControls() => true;



		////////////////

		public override bool CanBegin( Player playsFor ) {
			if( this.Data == null ) {
				LogHelpers.Warn( "No data given." );
				return false;
			}
			if( GameInfoHelpers.GetVanillaProgressList().Count > 0 ) {
				return false;
			}
			if( NPC.AnyNPCs(NPCID.Merchant) ) {
				return false;
			}

			return base.CanBegin( playsFor );
		}
	}
}
