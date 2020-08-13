﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public virtual bool CanBeginForWorld() {
			var myworld = ModContent.GetInstance<AMLWorld>();
			var cutsceneMngr = CutsceneManager.Instance;

			if( myworld.CurrentPlayingCutsceneForWorld != null ) {
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

			if( myplayer.CurrentPlayingCutsceneForPlayer != null ) {
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

		internal bool BeginForWorld_Internal( int sceneIdx, out Vector2 startPosition ) {
			var myworld = ModContent.GetInstance<AMLWorld>();
			if( myworld.CurrentPlayingCutsceneForWorld != null ) {
				LogHelpers.Warn( "Cannot begin cutscene " + this.UniqueId + " (scene " + sceneIdx + ") while "
					+ myworld.CurrentPlayingCutsceneForWorld + " is active." );
				startPosition = default( Vector2 );
				return false;
			}

			this.CurrentSceneIdx = sceneIdx;

			Scene scene = this.Scenes[sceneIdx];
			scene.BeginOnWorld_Internal( this );

			startPosition = this.OnBeginForWorld();
			return true;
		}

		internal void BeginForPlayer_Internal( Player player, int sceneIdx ) {
			this.CurrentSceneIdx = sceneIdx;

			Scene scene = this.Scenes[sceneIdx];
			scene.BeginOnPlayer_Internal( this, player );

			this.OnBeginForPlayer( player, sceneIdx );
		}

		////

		/// <summary></summary>
		/// <returns>Start position of the cutscene (for players).</returns>
		protected abstract Vector2 OnBeginForWorld();

		/// <summary></summary>
		/// <param name="player"></param>
		/// <param name="sceneIdx"></param>
		protected virtual void OnBeginForPlayer( Player player, int sceneIdx ) { }


		////////////////

		internal void OnEndForWorld_Internal() {
			this.CurrentSceneIdx = 0;
			this.OnEndForWorld();
		}

		internal void OnEndForPlayer_Internal( Player player ) {
			this.CurrentSceneIdx = 0;
			this.OnEndForPlayer( player );
		}

		////

		protected virtual void OnEndForWorld() { }

		protected virtual void OnEndForPlayer( Player player ) { }
	}
}
