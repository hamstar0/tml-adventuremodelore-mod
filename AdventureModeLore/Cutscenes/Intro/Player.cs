using System;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using HamstarHelpers.Helpers.Players;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool CanBeginForPlayer( Player player ) {
			return base.CanBeginForPlayer( player );
		}


		public override void BeginForPlayer( Player player ) {
		}


		////

		internal override void UpdateForPlayer( AMLPlayer myplayer ) {
			PlayerHelpers.LockdownPlayerPerTick( myplayer.player );
		}
	}
}
