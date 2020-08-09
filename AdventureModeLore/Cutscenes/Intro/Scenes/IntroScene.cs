using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Services.Camera;


namespace AdventureModeLore.Cutscenes.Intro.Scenes {
	class IntroScene : Scene {
		private Vector2 CameraStart;

		private Vector2 CameraEnd;

		private int CameraMoveDuration;

		private int CameraLingerDuration;


		////////////////

		public override bool MustSync => false;

		public override string SequenceName => "Intro";



		////////////////
		
		public IntroScene( Vector2 cameraBegin, Vector2 cameraEnd, int cameraMoveDuration, int cameraLingerDuration ) {
			this.CameraStart = cameraBegin;
			this.CameraEnd = cameraEnd;
			this.CameraMoveDuration = cameraMoveDuration;
			this.CameraLingerDuration = cameraLingerDuration;
		}


		////////////////

		protected override (Vector2, Vector2, int, int) GetCameraData( Cutscene parent ) {
			return (this.CameraStart, this.CameraEnd, this.CameraMoveDuration, this.CameraLingerDuration);
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
