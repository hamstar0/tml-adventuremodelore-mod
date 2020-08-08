using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Cutscenes.Intro {
	public abstract partial class Cutscene {
		public virtual bool CanBeginForWorld() {
			var myworld = ModContent.GetInstance<AMLWorld>();
			var cutsceneMngr = CutsceneManager.Instance;

			if( myworld.CurrentPlayingCutsceneForWorld != 0 ) {
LogHelpers.LogOnce("Fail 1a");
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivatedForWorld(this.UniqueId) ) {
LogHelpers.LogOnce("Fail 2a");
				return false;
			}
			
			return true;
		}

		public virtual bool CanBeginForPlayer( Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			var cutsceneMngr = CutsceneManager.Instance;

			if( myplayer.CurrentPlayingCutsceneForPlayer != 0 ) {
LogHelpers.LogOnce("Fail 1b");
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivatedForPlayer(this.UniqueId, player) ) {
LogHelpers.LogOnce("Fail 2b");
				return false;
			}
			
			return true;
		}


		////////////////

		/// <summary></summary>
		/// <returns>Start position of the cutscene (for players).</returns>
		protected abstract Vector2 OnBeginForWorld();

		/// <summary></summary>
		/// <param name="player"></param>
		/// <param name="sceneIdx"></param>
		protected virtual void OnBeginForPlayer( Player player, int sceneIdx ) { }

		////

		internal void BeginForWorld_Internal( Player player, int sceneIdx ) {
			failsafe against running cutscenes

			this.CurrentScene = sceneIdx;

			Scene scene = this.Scenes[sceneIdx];
			scene.BeginOnWorld_Internal( this );

			this.StartPosition = this.OnBeginForWorld();
		}

		internal void BeginForPlayer_Internal( Player player, int sceneIdx ) {
			failsafe against running cutscenes

			this.CurrentScene = sceneIdx;

			Scene scene = this.Scenes[sceneIdx];
			scene.BeginOnPlayer_Internal( this, player );

			this.OnBeginForPlayer( player, sceneIdx );
		}

		////////////////

		protected virtual void OnEndForWorld() { }

		protected virtual void OnEndForPlayer( Player player ) { }

		////

		internal void OnEndForWorld_Internal() {
			this.CurrentScene = 0;
			this.OnEndForWorld();
		}

		internal void OnEndForPlayer_Internal( Player player ) {
			this.CurrentScene = 0;
			this.OnEndForPlayer( player );
		}


		////////////////

		internal void SetCurrentSceneForWorld( int sceneIdx ) {
			Scene prevScene = this.Scenes[this.CurrentScene];
			prevScene.EndForWorld_Private();

			this.CurrentScene = sceneIdx;

			Scene currScene = this.Scenes[this.CurrentScene];
			currScene.BeginOnWorld_Internal( this );
		}
		
		internal void SetCurrentSceneForPlayer( Player player, int sceneIdx ) {
			Scene prevScene = this.Scenes[this.CurrentScene];
			prevScene.EndForPlayer_Private( player );

			this.CurrentScene = sceneIdx;

			Scene currScene = this.Scenes[this.CurrentScene];
			currScene.BeginOnPlayer_Internal( this, player );
		}
	}
}
