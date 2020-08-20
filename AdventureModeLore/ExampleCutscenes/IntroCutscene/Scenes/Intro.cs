using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene> {
		public IntroCutsceneScene_00()  : base( false, false ) { }


		////////////////

		protected override void OnBegin( IntroCutscene parent, Player playsFor ) {
			var cams = new List<CameraMover>();

			parent.GetData( playsFor, out Vector2 exteriorShipView, out Vector2 interiorShipView );

			bool isShipOnLeft = (int)exteriorShipView.X < ((16 * Main.maxTilesX) / 2);

			Vector2 dungeonView = new Vector2( Main.dungeonX * 16, Main.dungeonY * 16 );
			dungeonView.X += isShipOnLeft ? (-32 * 16) : (32 * 16);
			dungeonView.Y += -32 * 16;

			int extShipViewScrollY = (int)exteriorShipView.Y - (6 * 16);
			interiorShipView.Y = interiorShipView.Y - (12f * 16f);

			this.GetCam00_Title( cams );
			this.GetCam01_ExteriorChat( cams, exteriorShipView );
			this.GetCam02_Dungeon( cams, dungeonView );
			this.GetCam03_ExteriorAttack( cams, dungeonView, extShipViewScrollY );
			this.GetCam04_InteriorChat( cams, interiorShipView );

			CameraMover.Current = cams[0];
		}


		////////////////

		protected override bool Update( IntroCutscene parent, Player playsFor ) {
			var animCam = CameraMover.Current;
			if( animCam == null || !animCam.Name.StartsWith("AdventureModeIntro") || !animCam.IsAnimating() ) {
				return true;
			}

			return false;
		}


		////////////////

		public override void DrawInterface() {
			switch( CameraMover.Current.Name ) {
			case "AdventureModeIntro_Title":
				this.DrawInterface00_Title();
				break;
			}
		}
	}
}
