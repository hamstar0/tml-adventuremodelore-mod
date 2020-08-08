using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Services.Camera;


namespace AdventureModeLore.Cutscenes.Intro.Scenes {
	class IntroScene : Scene {
		public override bool MustSync => false;

		public override string SequenceName => "Intro";



		////////////////

		protected override (Vector2 cameraBegin, Vector2 cameraEnd, int cameraMoveDuration, int cameraLingerDuration)
					GetCameraData( Cutscene parent ) {
			Vector2 startPos = parent.StartPosition;
			Vector2 endPos = startPos + new Vector2( 0f, -4f );
			int duration = 60 * 5;

			return (startPos, endPos, duration, 0);
		}


		////////////////

		protected override bool UpdateOnLocal() {
			var animCam = AnimatedCamera.Instance;

			if( animCam.CurrentMoveSequence != this.SequenceName ) {
				return true;
			}
			if( animCam.MoveTicksLingerElapsed >= animCam.MoveTicksLingerDuration ) {
				return true;
			}

			return false;
		}

		protected override bool UpdateOnWorld() {
			return false;
		}
	}
}
