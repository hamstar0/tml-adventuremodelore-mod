using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	class AA_OpeningScene : Scene<IntroCutscene> {
		public AA_OpeningScene()  : base( false, false ) { }


		////////////////

		protected override void OnBegin_Any( IntroCutscene parent, Player player ) {
			CameraMover cam1 = null, cam2 = null, cam3 = null, cam4 = null;

			Vector2 interiorViewPos = parent.Data.InteriorShipViewPosition;
			Vector2 exteriorViewPos = parent.Data.ExteriorShipViewPosition;

			bool isShipOnLeft = (int)exteriorViewPos.X < ((16 * Main.maxTilesX) / 2);

			Vector2 dungeonViewPos = new Vector2( Main.dungeonX * 16, Main.dungeonY * 16 );
			dungeonViewPos.X += isShipOnLeft ? (-32 * 16) : (32 * 16);
			dungeonViewPos.Y += -40 * 16;

			cam1 = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)exteriorViewPos.X,
				moveYFrom: (int)exteriorViewPos.Y,
				moveXTo: (int)exteriorViewPos.X,
				moveYTo: (int)exteriorViewPos.Y - (12 * 16),
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
				froDuration: 0,
				onStop: () => CameraMover.Current = cam3
			);
			cam3 = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)exteriorViewPos.X,
				moveYFrom: (int)exteriorViewPos.Y,
				moveXTo: (int)exteriorViewPos.X,
				moveYTo: (int)exteriorViewPos.Y,
				toDuration: 0,
				lingerDuration: 60 * 5,
				froDuration: 0,
				onStop: () => CameraMover.Current = cam4
			);
			cam4 = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)interiorViewPos.X,
				moveYFrom: (int)interiorViewPos.Y,
				moveXTo: (int)interiorViewPos.X,
				moveYTo: (int)interiorViewPos.Y,
				toDuration: 0,
				lingerDuration: 60 * 5,
				froDuration: 0
				//onStop: () => CameraMover.Current = cam5
			);

			CameraMover.Current = cam1;
		}

		////////////////

		protected override bool Update_Local( IntroCutscene parent ) {
			var animCam = CameraMover.Current;
			if( animCam?.Name != "AdventureModeIntro" || !animCam.IsAnimating() ) {
				return true;
			}

			return false;
		}

		protected override bool Update_World( IntroCutscene parent ) {
			return false;
		}
	}
}
