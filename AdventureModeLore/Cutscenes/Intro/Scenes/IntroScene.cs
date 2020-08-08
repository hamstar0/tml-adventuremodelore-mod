﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Services.Camera;


namespace AdventureModeLore.Cutscenes.Intro.Scenes {
	class IntroScene : Scene {
		public override bool MustSync => true;

		public override string SequenceName => "Intro";



		////////////////

		protected override (Vector2, Vector2, int, int) BeginOnLocal( Cutscene parent ) {
			Vector2 startPos = parent.StartPosition;
			Vector2 endPos = startPos + new Vector2(0f, -4f);
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

		public override bool UpdateOnServer() {
			return false;
		}
	}
}
