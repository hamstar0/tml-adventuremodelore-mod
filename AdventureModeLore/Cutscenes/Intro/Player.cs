using System;
using Terraria;
using Terraria.Graphics.Capture;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override bool CanBeginForPlayer( Player player ) {
			return base.CanBeginForPlayer( player );
		}


		public override void BeginForPlayer( Player player ) {
		}


		////////////////

		public override bool IsSiezingControls() => true;


		////////////////

		internal override void UpdateForPlayer( AMLPlayer myplayer ) {
			myplayer.player.immune = true;
			myplayer.player.immuneTime = 2;

			//Main.mapFullscreen = false;
			//Main.mapEnabled = false;
			Main.mapStyle = 0;
			CaptureManager.Instance.Active = false;
		}
	}
}
