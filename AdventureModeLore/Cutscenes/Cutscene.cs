using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Cutscenes.Intro {
	public enum CutsceneID {
		Intro = 1
	}




	public abstract partial class Cutscene {
		public abstract CutsceneID UniqueId { get; }

		public abstract Scene[] Scenes { get; }

		public int CurrentScene { get; protected set; } = 0;

		////

		public Vector2 StartPosition { get; protected set; }



		////////////////

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

		public abstract bool IsSiezingControls();

		public virtual void SiezeControl( string control, ref bool state ) {
			if( control == "Inventory" ) { return; }
			state = false;
		}
		
		////////////////

		public virtual bool AllowInterfaceLayer( GameInterfaceLayer layer ) {
			return false;
		}
		
		////////////////

		public virtual bool AllowNPC( NPC npc ) {
			return npc.friendly;
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


		////////////////

		protected virtual void UpdateForWorld() { }

		protected virtual void UpdateForPlayer( AMLPlayer myplayer ) { }

		////

		internal void UpdateForWorld_Internal() {
			this.UpdateForWorld();

			if( !this.Scenes[this.CurrentScene].UpdateOnWorld_Internal() ) {
				return;
			}

			this.CurrentScene++;
			if( this.CurrentScene < (this.Scenes.Length - 1) ) {
				this.CurrentScene++;
				AMLCutsceneNetData.SendToClients( -1, this, this.CurrentScene );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}

		internal void UpdateForPlayer_Internal( AMLPlayer myplayer ) {
			this.UpdateForPlayer( myplayer );

			if( Main.netMode != NetmodeID.Server ) {
				if( myplayer.player.whoAmI == Main.myPlayer ) {
					this.UpdateForLocal();
				}
			}
		}

		////

		internal void UpdateForLocal() {
			Scene scene = this.Scenes[ this.CurrentScene ];
			if( !scene.MustSync ) {
				return;
			}

			// Has scene ended?
			if( !scene.UpdateOnLocal_Internal() ) {
				return;
			}

			if( this.CurrentScene < (this.Scenes.Length - 1) ) {
				this.CurrentScene++;
				AMLCutsceneNetData.Broadcast( this, this.CurrentScene );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}
	}
}
