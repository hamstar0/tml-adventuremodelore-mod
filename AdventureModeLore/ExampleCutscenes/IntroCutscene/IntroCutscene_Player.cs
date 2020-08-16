using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool IsSiezingControls() => true;



		////////////////

		public override bool CanBegin_Player( Player player ) {
			if( this.Data == null ) {
				LogHelpers.Warn( "No data given." );
				return false;
			}
			return base.CanBegin_Player( player );
		}
	}
}
