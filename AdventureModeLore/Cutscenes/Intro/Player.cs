using HamstarHelpers.Helpers.Players;
using System;
using Terraria;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool HasValidPlayerConditions( Player player ) {
			return true;
		}


		public override void BeginForPlayer( Player player ) {
		}


		public override void UpdateForPlayer( Player player ) {
			PlayerHelpers.LockdownPlayerPerTick( player );
		}
	}
}
