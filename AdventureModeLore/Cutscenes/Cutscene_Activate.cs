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

		internal void BeginForWorld_Internal( int sceneIdx ) {
			this.CurrentScene = sceneIdx;
			this.StartPosition = this.OnBeginForWorld();
		}

		internal void BeginForPlayer_Internal( Player player, int sceneIdx ) {
			this.CurrentScene = sceneIdx;
			this.OnBeginForPlayer( player, sceneIdx );
		}

		////////////////

		protected virtual void OnEndForWorld() { }

		protected virtual void OnEndForPlayer( AMLPlayer myplayer ) { }

		////

		internal void OnEndForWorld_Internal() {
			this.CurrentScene = 0;
			this.OnEndForWorld();
		}

		internal void OnEndForPlayer_Internal( AMLPlayer myplayer ) {
			this.CurrentScene = 0;
			this.OnEndForPlayer( myplayer );
		}
	}
}
