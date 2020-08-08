using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Services.Camera;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public abstract partial class Scene {
		public abstract bool MustSync { get; }

		public abstract string SequenceName { get; }

		public int PlayingForWhom { get; protected set; }



		////////////////

		internal void BeginOnLocal_Internal( Cutscene parent, int playerWho ) {
			this.PlayingForWhom = playerWho;
			(Vector2 beg, Vector2 end, int time, int linger) camera = this.OnBeginOnLocal( parent );

			AnimatedCamera.BeginMoveSequence(
				this.SequenceName,
				(int)camera.beg.X,
				(int)camera.beg.Y,
				(int)camera.end.X,
				(int)camera.end.Y,
				camera.time,
				camera.linger
			);
		}

		internal void BeginOnServer_Internal( int playerWho ) {
			this.PlayingForWhom = playerWho;

			this.OnBeginOnServer();
		}

		////

		protected abstract (Vector2 cameraBegin, Vector2 cameraEnd, int cameraMoveDuration, int cameraLingerDuration)
				OnBeginOnLocal( Cutscene parent );

		protected virtual void OnBeginOnServer() { }
	}
}
