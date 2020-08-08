using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.Camera;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public abstract partial class Scene {
		public abstract bool MustSync { get; }

		public abstract string SequenceName { get; }



		////////////////

		internal void BeginOnPlayer_Internal( Cutscene parent, Player player ) {
			if( Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer ) {
				this.BeginOnLocal( parent );
			}

			this.OnBeginOnPlayer( parent, player );
		}

		internal void BeginOnWorld_Internal( Cutscene parent ) {
			this.OnBeginOnWorld( parent );
		}

		////
		
		private void BeginOnLocal( Cutscene parent ) {
			(Vector2 beg, Vector2 end, int time, int linger) camera = this.GetCameraData( parent );

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

		////

		protected virtual void OnBeginOnPlayer( Cutscene parent, Player player ) { }

		protected virtual void OnBeginOnWorld( Cutscene parent ) { }

		////////////////

		internal void EndForWorld_Private() {
			this.OnEndOnWorld();
		}
		
		internal void EndForPlayer_Private( Player player ) {
			this.OnEndOnPlayer( player );
		}

		////

		protected virtual void OnEndOnPlayer( Player player ) { }

		protected virtual void OnEndOnWorld() { }


		////////////////

		protected abstract (Vector2 cameraBegin, Vector2 cameraEnd, int cameraMoveDuration, int cameraLingerDuration)
				GetCameraData( Cutscene parent );
	}
}
