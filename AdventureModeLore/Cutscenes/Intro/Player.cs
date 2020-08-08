using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool CanBeginForPlayer( Player player ) {
			return base.CanBeginForPlayer( player );
		}


		////////////////

		public override bool IsSiezingControls() => true;
	}
}
