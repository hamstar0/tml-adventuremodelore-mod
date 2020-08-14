using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	class AA_OpeningScene : Scene<IntroCutscene> {
		public AA_OpeningScene()  : base( false ) { }


		////////////////

		protected override void OnBeginOnPlayer( IntroCutscene parent, Player player ) {
			CameraMover cam1 = null, cam2 = null, cam3 = null;

			Vector2 exteriorShipStartPos = parent.CurrentPosition;

			bool isShipOnLeft = (int)exteriorShipStartPos.X < ((16 * Main.maxTilesX) / 2);

			Vector2 dungeonViewPos = new Vector2( Main.dungeonX * 16, Main.dungeonY * 16 );
			dungeonViewPos.X += isShipOnLeft ? (-32 * 16) : (32 * 16);
			dungeonViewPos.Y += -40 * 16;

			cam1 = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)exteriorShipStartPos.X,
				moveYFrom: (int)exteriorShipStartPos.Y,
				moveXTo: (int)exteriorShipStartPos.X,
				moveYTo: (int)exteriorShipStartPos.Y - (12 * 16),
				toDuration: 60 * 3,
				lingerDuration: 60 * 3,
				froDuration: 0,
				onStop: () => CameraMover.Current = cam2
			);
			cam2 = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)dungeonViewPos.X,
				moveYFrom: (int)dungeonViewPos.Y,
				moveXTo: (int)dungeonViewPos.X,
				moveYTo: (int)dungeonViewPos.Y - (4 * 16),
				toDuration: 60 * 5,
				lingerDuration: 0,
				froDuration: 0
				//onStop: () => CameraMover.Current = cam3
			);

			CameraMover.Current = cam1;
		}

		////////////////

		protected override bool UpdateOnLocal( IntroCutscene parent ) {
			var animCam = CameraMover.Current;
			if( animCam?.Name != "AdventureModeIntro" || !animCam.IsAnimating() ) {
				return true;
			}

			return false;
		}

		protected override bool UpdateOnWorld( IntroCutscene parent ) {
			return false;
		}
	}
}
