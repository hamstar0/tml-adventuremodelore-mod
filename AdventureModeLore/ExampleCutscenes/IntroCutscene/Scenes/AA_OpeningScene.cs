using System;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	class AA_OpeningScene : Scene<IntroCutscene> {
		public AA_OpeningScene()  : base( false, false ) { }


		////////////////

		protected override void OnBegin( IntroCutscene parent, Player playsFor ) {
			CameraMover cam0 = null, cam1 = null, cam2 = null, cam3 = null, cam4 = null;

			parent.GetData( playsFor, out Vector2 exteriorViewPos, out Vector2 interiorViewPos );

			bool isShipOnLeft = (int)exteriorViewPos.X < ((16 * Main.maxTilesX) / 2);

			Vector2 dungeonViewPos = new Vector2( Main.dungeonX * 16, Main.dungeonY * 16 );
			dungeonViewPos.X += isShipOnLeft ? (-32 * 16) : (32 * 16);
			dungeonViewPos.Y += -32 * 16;

			int extShipViewY = (int)exteriorViewPos.Y - (6 * 16);
			int intShipViewY = (int)interiorViewPos.Y - (12 * 16);

			cam0 = new CameraMover(
				name: "AdventureModeIntro_Title",
				moveXFrom: (Main.maxTilesX - 40) - (Main.screenWidth / 16),
				moveYFrom: 40,
				moveXTo: (Main.maxTilesX - 40) - (Main.screenWidth / 16),
				moveYTo: 40,
				toDuration: 0,
				lingerDuration: 60 * 5,
				froDuration: 0,
				onStop: () => CameraMover.Current = cam1
			);
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
				moveYFrom: extShipViewY,
				moveXTo: (int)exteriorViewPos.X,
				moveYTo: extShipViewY,
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

			CameraMover.Current = cam0;
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
			var animCam = CameraMover.Current;
			switch( animCam.Name ) {
			case "AdventureModeIntro_Title":
				string titleText = "Test Title";
				Vector2 titleDim = Main.fontMouseText.MeasureString( titleText );
				var pos = new Vector2( (Main.screenWidth / 2) - titleDim.X, (Main.screenHeight / 2) - titleDim.Y );

				Utils.DrawBorderStringFourWay(
					sb: Main.spriteBatch,
					font: Main.fontMouseText,
					text: titleText,
					x: pos.X,
					y: pos.Y,
					textColor: Color.White,
					borderColor: Color.Black,
					origin: default(Vector2),
					scale: 2f
				);
				break;
			}
		}
	}
}
