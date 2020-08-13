using System;
using Terraria;
using HamstarHelpers.Services.Camera;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscene.Intro.Scenes {
	class IntroScene : Scene<IntroCutscene> {
		public IntroScene()  : base( false ) {
		}


		////////////////

		protected override void OnBeginOnPlayer( IntroCutscene parent, Player player ) {
			CameraMover.Current = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)parent.StartPosition.X,
				moveYFrom: (int)parent.StartPosition.Y,
				moveXTo:
		}

		////////////////

		protected override bool UpdateOnLocal( IntroCutscene parent ) {
			//var animCam = CameraMover.Current;
			//if( animCam?.Name != this.UniqueId.Name || !animCam.IsAnimating() || animCam.IsPaused ) {
			//	return false;
			//}

			//CameraMover.Current = new CameraMover(
			//	name: "AdventureModeIntro",
			//	moveXFrom: (int)this.CameraStart.X,
			//	moveYFrom: (int)this.CameraStart.Y,
			//	moveXTo:

			return false;
		}

		protected override bool UpdateOnWorld( IntroCutscene parent ) {
			return false;
		}
	}
}
