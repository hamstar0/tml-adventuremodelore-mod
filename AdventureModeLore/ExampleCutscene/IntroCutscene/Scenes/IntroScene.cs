using System;
using Microsoft.Xna.Framework;
using Terraria;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscene.Intro.Scenes {
	class IntroScene : Scene {
		protected Vector2 CameraEnd;

		protected int CameraMoveDuration;

		protected int CameraLingerDuration;



		////////////////
		
		public IntroScene( Vector2 camBegin, Vector2 camEnd, int camMoveDuration, int camLingerDuration ) 
					: base( false, camBegin ) {
			this.CameraEnd = camEnd;
			this.CameraMoveDuration = camMoveDuration;
			this.CameraLingerDuration = camLingerDuration;
		}


		////////////////

		protected override bool UpdateOnLocal() {
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

		protected override bool UpdateOnWorld() {
			return false;
		}
	}
}
