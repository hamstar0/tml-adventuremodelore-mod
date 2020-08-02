using System;
using Terraria;
using HamstarHelpers.Helpers.Players;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool HasValidPlayerConditions( Player player ) {
			return true;
		}


		public override void BeginForPlayer( Player player ) {
		}


		////

		internal override void UpdateForPlayer( AMLPlayer myplayer ) {
			PlayerHelpers.LockdownPlayerPerTick( myplayer.player );
		}
	}
}
