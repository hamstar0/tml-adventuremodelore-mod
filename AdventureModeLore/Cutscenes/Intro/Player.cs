using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool CanBeginForPlayer( Player player ) {
			return base.CanBeginForPlayer( player );
		}


		protected override void BeginForPlayer( Player player ) {
		}


		////////////////

		public override bool IsSiezingControls() => true;


		////////////////

		protected override void UpdateForPlayer( AMLPlayer myplayer ) {
		}
	}
}
